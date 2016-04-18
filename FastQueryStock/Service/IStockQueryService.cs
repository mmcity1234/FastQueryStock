using FastQueryStock.ViewModels;
using StockSDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Service
{
    public interface IStockQueryService
    {
        /// <summary>
        /// Query the multiple of real time stock data by stock's id
        /// </summary>
        /// <param name="stockIds"></param>
        /// <returns></returns>
        Task<List<RealTimeStockItem>> GetMultipleRealTimeStockAsync(List<StockInfoItem> stockList);

      
        /// <summary>
        /// Get all of the stock information from TWSE web site
        /// </summary>
        /// <returns></returns>
        Task<List<StockInfoItem>> GetAllStockInfoAsync();

        /// <summary>
        /// Get the real-time trade chart of stock 
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        Task<StockChartModel> GetRealTimeTradeChartAsync(StockInfoItem stockItem);
    }
}
