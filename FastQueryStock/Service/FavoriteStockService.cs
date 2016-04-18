using FastQueryStock.Entity.Context;
using FastQueryStock.Entity.Entity;
using FastQueryStock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Service
{
    public class FavoriteStockService : IFavoriteStockService
    {
        public void Add(StockInfoItem item)
        {
            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                // check if the stock is exist in favorite table
                if (db.FavoriteStock.GetById(item.Id) != null)
                    throw new FavoriteStockExistException(item.Id, item.Name);

                StockEntity stockEntity = db.Stock.GetById(item.Id);
                var favoriteStockEntity = new FavoriteStockEntity
                {
                    Id = item.Id,
                    ParentStock = stockEntity,
                    // TODO : it must rewrite the category source from category table
                    //CustomCategory = item.Category,
                    Order = item.Order

                };
                db.FavoriteStock.Add(favoriteStockEntity);
                db.SaveChanges();

            }
        }
        public List<StockInfoItem> GetAll()
        {
            List<StockInfoItem> allResult = new List<StockInfoItem>();
            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                List<FavoriteStockEntity> allEntity = db.FavoriteStock.GetAll();

                foreach (var item in allEntity)
                {
                    allResult.Add(new StockInfoItem
                    {
                        Id = item.Id,
                        // TODO : it must rewrite the category source from category table
                        Category = item.ParentStock.Category,
                        MarketType = item.ParentStock.MarketType,
                        Name = item.ParentStock.Name,
                        Order = item.Order
                    });
                }
            }
            return allResult;
        }

        /// <summary>
        /// Get the last order number of favorite stock by stock category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int GetLastOrder(int CustomCategoryId)
        {
            int lastOrder = 0;
            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                var favoriteStocks = db.FavoriteStock.GetAll();
                if (favoriteStocks.Count != 0)
                {
                    lastOrder = favoriteStocks.Where(x => x.CustomCategory == CustomCategoryId).
                        Max(x => x.Order);
                    lastOrder++;
                }
            }
            return lastOrder;
        }


        public void Delete(string Id)
        {
            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                db.FavoriteStock.Delete(Id);
                db.SaveChanges();
            }
        }
    }
}
