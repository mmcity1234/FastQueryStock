using FastQueryStock.ViewModels;
using StockSDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Model
{
    public static class ModelConverter
    {
        /// <summary>
        /// Convert all of stock info item to StockInfoModel
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public static List<StockInfoModel>  Convert(this List<StockInfoItem> itemList)
        {
            List<StockInfoModel> resultList = new List<StockInfoModel>();
            foreach (var stockInfoItem in itemList)
            {
                resultList.Add(stockInfoItem.Convert());
            }
            return resultList;
        }
        public static StockInfoModel Convert(this StockInfoItem stockItem)
        {
            return new StockInfoModel
            {
                Id = stockItem.Id,
                Category = stockItem.Category,
                MarketType = stockItem.MarketType,
                Name = stockItem.Name
            };
        }

        public static List<RealTimeStockItem> Convert(this List<RealTimeStockModel> realTimeList)
        {
            List<RealTimeStockItem> resultList = new List<RealTimeStockItem>();
            foreach (var model in realTimeList)
            {
                resultList.Add(RealTimeStockItem.ConvertFrom(model));
            }
            return resultList;
        }
    }
}
