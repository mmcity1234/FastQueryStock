using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockSDK.Google;
using StockSDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Google.Tests
{
    [TestClass()]
    public class GoogleStockClientTests
    {
        [TestMethod()]
        public void QueryStockTest()
        {
            GoogleStockClient query = new GoogleStockClient();
            RealTimeStockModel item = query.QueryStockAsync("2115").GetAwaiter().GetResult();
            Assert.IsNotNull(item.CurrentPrice);
        }
    }
}