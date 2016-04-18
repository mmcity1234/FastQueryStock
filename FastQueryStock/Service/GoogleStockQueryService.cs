using FastQueryStock.ViewModels;
using StockSDK;
using StockSDK.Google;
using StockSDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Service
{
    [Obsolete("The class doesn't implement complete")]
    public class GoogleStockQueryService //: IStockQueryService
    {
        public Task<List<StockInfoItem>> GetAllStockInfoAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RealTimeStockItem> GetRealTimeStockAsync(StockInfoItem stockItem)
        {
            GoogleStockClient client = new GoogleStockClient();
            RealTimeStockModel stockData = await client.QueryStockAsync(stockItem.Id);

            return RealTimeStockItem.ConvertFrom(stockData);
        }

        public Task<List<RealTimeStockItem>> GetMultipleRealTimeStockAsync(List<StockInfoItem> stockList)
        {
            throw new NotImplementedException();
        }
    }
}
