using FastQueryStock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Service
{
    public interface ILocalStockService
    {
        /// <summary>
        /// Get stock data by id or name
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        StockInfoItem Get(string key);
        /// <summary>
        /// Get all of the stock datat from local db
        /// </summary>
        /// <returns></returns>
        List<StockInfoItem> GetAll();
        
        void Add(StockInfoItem item);
        void Add(List<StockInfoItem> item);

        void DeleteAll();

        /// <summary>
        /// Inititalize the data For the first time
        /// </summary>
        void InitializeData();

    }
}
