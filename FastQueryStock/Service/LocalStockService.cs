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
    /// <summary>
    /// Load data from local db
    /// </summary>
    public class LocalStockService : ILocalStockService
    {

        public StockInfoItem Get(string key)
        {
            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                StockEntity stock = db.Stock.GetById(key);
                if (stock == null)
                {
                    stock = db.Stock.GetByName(key);
                    if (stock == null)
                        throw new Exception("本機資料庫查無此筆資料，請確認此股票代號是否正確，並更新上市櫃股票資料");
                }

                return new StockInfoItem()
                {
                    Id = stock.Id,
                    Name = stock.Name,
                    MarketType = stock.MarketType,
                    Category = stock.Category
                };
            }
        }
        public List<StockInfoItem> GetAll()
        {
            List<StockInfoItem> results = new List<StockInfoItem>();

            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                List<StockEntity> stocks = db.Stock.GetAll();
                foreach (var item in stocks)
                {
                    results.Add(new StockInfoItem()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        MarketType = item.MarketType,
                        Category = item.Category
                    });
                }
            }
            return results;
        }

        public void Add(StockInfoItem item)
        {
            var stockEntity = new StockEntity
            {
                Id = item.Id,
                Name = item.Name,
                MarketType = item.MarketType,
                Category = item.Category
            };

            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                db.Stock.Add(stockEntity);
                db.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            //using (StockUnitOfWork db = new StockUnitOfWork())
            //{
            //    db.Stock.Delete(Id);
            //    db.SaveChanges();
            //}
        }

        public void Add(List<StockInfoItem> stockInfoList)
        {
            using (StockUnitOfWork db = new StockUnitOfWork())
            {
                var existStockInfoList = db.Stock.GetAll();
                List<StockEntity> stockEntityList = new List<StockEntity>();
                foreach (var item in stockInfoList)
                {
                    // 不存在才更新
                    if (existStockInfoList.FirstOrDefault(x => x.Id == item.Id) == null)
                    {
                        stockEntityList.Add(new StockEntity
                        {
                            Id = item.Id,
                            Name = item.Name,
                            MarketType = item.MarketType,
                            Category = item.Category
                        });

                    }
                }
                db.Stock.Add(stockEntityList);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Inititalize the data For the first time
        /// </summary>
        public void InitializeData()
        {
            StockUnitOfWork.CreateDatabase();
        }
    }
}
