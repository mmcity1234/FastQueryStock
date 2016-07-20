using FastQueryStock.Common.Exceptions;
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
                    CustomCategory = item.CustomCategory,
                    Order = item.Order

                };
                db.FavoriteStock.Add(favoriteStockEntity);
                db.SaveChanges();

            }
        }

        public StockInfoItem GetById(string id)
        {
            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                var entity = db.FavoriteStock.GetById(id);
                if (entity == null)
                    throw new DataNotFoundException("查無此股票代號 [" + id + "]", id);
                return new StockInfoItem
                {
                    Id = entity.Id,
                    Category = entity.ParentStock.Category,
                    CustomCategory = entity.CustomCategory,
                    MarketType = entity.ParentStock.MarketType,
                    Name = entity.ParentStock.Name,
                    Order = entity.Order
                };
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
                        CustomCategory = item.CustomCategory,
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


        public void ChnageOrder(StockInfoItem originalItem, StockInfoItem targetItem)
        {
            if (originalItem.Order == targetItem.Order)
                return;

            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                int changeStartInex = originalItem.Order < targetItem.Order ? originalItem.Order : targetItem.Order;
                int changeEndInex = originalItem.Order > targetItem.Order ? originalItem.Order : targetItem.Order;

                var changeList = db.FavoriteStock.GetAll(x => x.CustomCategory == originalItem.CustomCategory).
                    OrderBy(x => x.Order).
                    Where(x => x.Order >= changeStartInex && x.Order <= changeEndInex).
                    ToList();
                    
                // move the original item to upper
                if (originalItem.Order > targetItem.Order)
                {  
                    // 在異動的範圍內先全部累加排序的值，最後再將來源(最後一個)的排序值進行異動
                    changeList.ToList().ForEach(x => x.Order++);
                    changeList[changeList.Count - 1].Order = changeList[0].Order - 1;
                }
                else
                {
                    changeList.ToList().ForEach(x => x.Order--);
                    changeList[0].Order = changeList[changeList.Count - 1].Order + 1;
                }

                foreach (var item in changeList)
                    db.FavoriteStock.Update(item);
                db.SaveChanges();
            }
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
