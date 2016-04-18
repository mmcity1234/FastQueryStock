using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockSDK.Model;
using StockSDK.Twse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Twse.Tests
{
    [TestClass()]
    public class TwseStockClientTests
    {
        [TestMethod()]
        public void QueryStockAsyncTest()
        {
            try
            {

            }
            catch (Exception e)
            {

            }

        }

        [TestMethod()]
        public void GetAllStocksAsyncTest()
        {

        }

        [TestMethod()]
        public void QueryExcehangeChartStockAsyncTest()
        {
            TwseStockClient client = new TwseStockClient();
            var stockInfo = new StockInfoModel { Id = "2345", MarketType = "上市", Name = "智邦" };
            var result = client.QueryRealTimeTradeChartAsync(stockInfo).GetAwaiter().GetResult();
        }
    }
}