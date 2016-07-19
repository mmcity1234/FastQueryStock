using FastQueryStock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Service
{
    /// <summary>
    /// 提供自選清單管理的服務
    /// </summary>
    public interface IFavoriteStockService
    {
        void Add(StockInfoItem item);

        List<StockInfoItem> GetAll();

        StockInfoItem GetById(string id);

        void Delete(string Id);

        /// <summary>
        /// Get the last order number of favorite stock by stock category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        int GetLastOrder(int CustomCategoryId);

        /// <summary>
        /// Change the stock sequence in this category
        /// </summary>
        /// <param name="changeItem"></param>
        /// <param name="newIndex"></param>
        void ChnageOrder(StockInfoItem originalItem, StockInfoItem targetItem);
    }
}
