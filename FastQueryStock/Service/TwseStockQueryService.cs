using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastQueryStock.ViewModels;
using StockSDK.Twse;
using StockSDK;
using StockSDK.Model;
using FastQueryStock.Common;
using System.Threading;
using FastQueryStock.Model;
using System.Diagnostics;

namespace FastQueryStock.Service
{
    public class TwseStockQueryService : IStockQueryService
    {
        private SessionData _clientSession = null;

        private SemaphoreSlim _syncLock = new SemaphoreSlim(1);

        public async Task<List<RealTimeStockItem>> GetMultipleRealTimeStockAsync(List<StockInfoItem> stockList)
        {
            // hard code to display taiwan tse stock
            stockList.Insert(0, new StockInfoItem { Id = "t00", MarketType = "上市" });


            SessionData session = await GetSession();
            List<StockInfoModel> queryStockList = stockList.Convert();
            
            List<RealTimeStockModel> results = new List<RealTimeStockModel>();
            try
            {
                TwseStockClient client = new TwseStockClient();
                results = await client.QueryStockAsync(queryStockList, session);
            }
            catch (Exception e)
            {
                Debug.WriteLine(DateTime.Now + " : " + "Cookie time out");
                ReleaseSession();
            }

            return results.Convert();
        }

        private async Task<SessionData> GetSession()
        {
            //SessionData clientSession;
            if (_clientSession == null)
            {
                await _syncLock.WaitAsync();
                if (_clientSession == null)
                {
                    _clientSession = await TwseStockClient.CreateSessionAsync();
                }
                _syncLock.Release();
            }
            return _clientSession;
        }
        private void ReleaseSession()
        {
            _clientSession = null;
        }

        public async Task<StockChartModel> GetRealTimeTradeChartAsync(StockInfoItem stockItem)
        {
            TwseStockClient client = new TwseStockClient();
            var resutl = await client.QueryRealTimeTradeChartAsync(stockItem.Convert());
            return resutl;
        }

        /// <summary>
        /// Get all of the stock information from TWSE web site
        /// </summary>
        /// <returns></returns>
        public async Task<List<StockInfoItem>> GetAllStockInfoAsync()
        {
            TwseStockClient client = new TwseStockClient();
            var stockList = await client.GetAllStockInfoAsync();
            List<StockInfoItem> resultList = new List<StockInfoItem>();
            foreach (var item in stockList)
            {
                resultList.Add(new StockInfoItem
                {
                    Id = item.Id,
                    Category = item.Category,
                    MarketType = item.MarketType,
                    Name = item.Name
                });
            }
            return resultList;
        }
    }
}
