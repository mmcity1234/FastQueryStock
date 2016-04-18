using HtmlAgilityPack;
using StockSDK.Model;
using StockSDK.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Twse
{
    public class TwseStockClient
    {
        private const string SUCCESS = "OK";
        private const string SESSION_URI = "http://61.57.47.179/stock/fibest.jsp?lang=zh_tw";
        // Get the type of stock URI
        //private const string QUERY_STOCK_INFO_URI = "http://61.57.47.179/stock/api/getStock.jsp?ch={0}.tw&json=1&_={1}";
        private const string QUERY_URI = "http://61.57.47.179/stock/api/getStockInfo.jsp?ex_ch={0}&json=1&delay=0&_={1}";

        // Get the stock chart data
        private const string QUERY_REAL_TIME_TRADE_CHART_URI = "http://61.57.47.179/stock/api/getChartOhlc.jsp?ex={0}&ch={1}&fqy=0&delay=0&_={2}";


        //private const string SESSION_URI = "http://mis.twse.com.tw/stock/fibest.jsp?lang=zh_tw";
        //private const string QUERY_STOCK_INFO_URI = "http://mis.twse.com.tw/stock/api/getStock.jsp?ch={0}.tw&json=1&_={1}";
        //private const string QUERY_URI = "http://mis.twse.com.tw/stock/api/getStockInfo.jsp?ex_ch={0}&json=1&delay=0&_={1}";


        /// <summary>
        /// 所有上市資料
        /// </summary>
        private const string QUERY_MARKET_TSE_URI = "http://isin.twse.com.tw/isin/C_public.jsp?strMode=2";
        /// <summary>
        /// 所有上櫃資料
        /// </summary>
        private const string QUERY_MARKET_OCT_URI = "http://isin.twse.com.tw/isin/C_public.jsp?strMode=4";

        public TwseStockClient() { }

        public async Task<List<RealTimeStockModel>> QueryStockAsync(List<StockInfoModel> queryStockList, SessionData session)
        {
            List<RealTimeStockModel> allRealTimeResult = new List<RealTimeStockModel>();
            // Build all of the stock parameter 
            StringBuilder stockParm = new StringBuilder();
            foreach (var stock in queryStockList)
                stockParm.Append(stock.ExchangeTpeyKey).Append("_").Append(stock.Id).Append(".tw|");

            try
            {
                string queryStr = string.Format(QUERY_URI, stockParm, GetJavaScriptTimeTick());
                Debug.WriteLine(DateTime.Now + " : " + queryStr);
                string responseJson = string.Empty;

                responseJson = await Http.GetAsync(queryStr, true, session.Cookie);

                var results = JsonConverter.DeserializeFrom<RootObject<StockData>>(responseJson);
                if (results.Result == SUCCESS)
                {
                    foreach (var item in results.StockMessage)
                        allRealTimeResult.Add(item.Convert());
                    return allRealTimeResult;
                }
                else
                    throw new Exception("Stock Query fail, error result : " + results.Result);
            }
            catch (WebException e)
            {
                // retry to get session and try to query stock data
                throw new QueryDataTimeOutException(e.InnerException.Message);
            }
        }

        /// <summary>
        /// Get the real-time exchange chart by stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public async Task<StockChartModel> QueryRealTimeTradeChartAsync(StockInfoModel stock)
        {
            string url = string.Format(QUERY_REAL_TIME_TRADE_CHART_URI, stock.ExchangeTpeyKey, stock.Id + ".tw", GetJavaScriptTimeTick());

            try
            {
                string responseJson = await Http.GetAsync(url);
                var jsonModel = JsonConverter.DeserializeFrom<ExchangeChartData>(responseJson);
                if (jsonModel.Result == SUCCESS)
                {
                    return jsonModel.ConvertToModel();
                }
                else
                    throw new Exception("Stock Query fail, error result : " + jsonModel.Result);
            }
            catch(WebException e)
            {
                // TODO test
                // retry to get session and try to query stock data
                throw new QueryDataTimeOutException(e.InnerException.Message);
            }
        }

        /// <summary>
        /// Get the stock exchange type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public async Task<string> GetStockExchangeTypeAsync(string id, SessionData session)
        //{
        //    RootObject<StockInfoData> stockInfo = null;
        //    try
        //    {
        //        string queryStr = string.Format(QUERY_STOCK_INFO_URI, id);
        //        string responseJson = await Http.GetAsync(queryStr, true, session.Cookie);
        //        stockInfo = JsonConverter.DeserializeFrom<RootObject<StockInfoData>>(responseJson);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new SessionFailedException("Session may be disconnected, please try to create session again");
        //    }

        //    if (stockInfo.Result == SUCCESS)
        //        return stockInfo.StockMessage[0].ExchangeType;
        //    else
        //        throw new QueryStockTypeFailedException("Can't get the stock type, please try to query again or check the twse site is alive : " + QUERY_STOCK_INFO_URI);
        //}

        /// <summary>
        /// Create the session to connect the twse web site
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<SessionData> CreateSessionAsync()
        {
            string uri = string.Format(SESSION_URI);
            // create session and get the cookies data
            CookieContainer cookie = new CookieContainer();
            await Http.GetAsync(uri, true, cookie);
            return new SessionData(cookie);
        }

        private double GetJavaScriptTimeTick()
        {
            return Math.Round(DateTime.UtcNow
               .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
               .TotalMilliseconds);
        }


        /// <summary>
        /// Get all of the stock information from TWSE web, include of TSE and OCT stock
        /// </summary>
        /// <returns></returns>
        public async Task<List<StockInfoModel>> GetAllStockInfoAsync()
        {
            string responseTseHtml = await Http.GetAsync(QUERY_MARKET_TSE_URI);
            string responseOctHtml = await Http.GetAsync(QUERY_MARKET_OCT_URI);

            var tseStockList = RetrieveStockInfoFrom(responseTseHtml);
            var octStockList = RetrieveStockInfoFrom(responseOctHtml);

            List<StockInfoModel> results = new List<StockInfoModel>();
            results.AddRange(tseStockList);
            results.AddRange(octStockList);

            return results;
        }

        private List<StockInfoModel> RetrieveStockInfoFrom(string html)
        {
            const char stockSplitWord = '　';
            int count = 0;
            List<StockInfoModel> stockList = new List<StockInfoModel>(200);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var stockHtmlNodes = doc.DocumentNode.Descendants("tr");

            foreach (var item in stockHtmlNodes)
            {
                // the first two record is not stock data
                if (count++ < 2)
                    continue;
                var stockData = item.ChildNodes[0].InnerText.Trim().Split(stockSplitWord);
                if (stockData.Length >= 2)
                {
                    StockInfoModel model = new StockInfoModel
                    {
                        Id = stockData[0].Trim(),
                        Name = stockData[stockData.Length - 1].Trim(),
                        MarketType = item.ChildNodes[3].InnerText.Trim(),
                        Category = item.ChildNodes[4].InnerText.Trim()
                    };
                    stockList.Add(model);
                }
                else
                    count = 100;
            }
            return stockList;
        }
    }
}
