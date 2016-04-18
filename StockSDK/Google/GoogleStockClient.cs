using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using StockSDK.Network;
using StockSDK.Model;

namespace StockSDK.Google
{
    public class GoogleStockClient
    {
        private const string QUERY_URI = "https://www.google.com.tw/finance/info?client=ob&infotype=infoonebox&hl=zh-TW&ei=NqJeVpjKBsOo0ATk1oLABQ&q=TPE%3A{0}";
        public async Task<RealTimeStockModel> QueryStockAsync(string id)
        {
            try
            {
                string queryStr = string.Format(QUERY_URI, id);
                string responseJson = await Http.GetAsync(queryStr);
                // remove the protection string
                responseJson = responseJson.Replace("/", string.Empty);

                var results = JsonConverter.DeserializeFrom<List<StockData>>(responseJson);
                if (results.Count != 0)
                    return results[0].Convert();
                else
                    return StockData.CreateNon(id);

            }
            catch (ParseHtmlException ex)
            {
                return new RealTimeStockModel { Name = "Not Found" };
            }
        }
    }
}
