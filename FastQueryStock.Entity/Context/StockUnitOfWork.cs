using FastQueryStock.Entity.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Entity.Context
{
    public class StockUnitOfWork :IStockUnitOfWork, IDisposable
    {
        private bool disposed = false;
        private StockContext _context = new StockContext();
        public IStockRepository Stock { get; set; }
        public IFavoriteStockRepository FavoriteStock { get; set; }

        public StockUnitOfWork()
        {          
            Stock = new StockRepository(_context);
            FavoriteStock = new FavoriteStockRepository(_context);
        }

        /// <summary>
        /// Save all change to database
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Create the database, if exist it will not create again
        /// </summary>
        public static void CreateDatabase()
        {
            using (var db = new StockContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
