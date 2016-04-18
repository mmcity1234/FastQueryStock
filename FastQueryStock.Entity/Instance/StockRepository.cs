using FastQueryStock.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using FastQueryStock.Entity.Context;
using System.Reflection;

namespace FastQueryStock.Entity.Instance
{
    public class StockRepository : BaseRepository<StockEntity>, IStockRepository
    {
        public StockRepository(StockContext context) : base(context) { }

        public override List<StockEntity> GetAll(Expression<Func<StockEntity, bool>> condition = null)
        {
            return GetAllWithInclude(condition);
        }

        public override void Add(IList<StockEntity> list)
        {
            int count = 1;

            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            //Context.Configuration.ValidateOnSaveEnabled = false;
            foreach (var item in list)
            {
                // batch to update data entity
                Add(item);
                if (count++ % 1000 == 0)
                {
                    Context.SaveChanges();
                    // Context.Dispose();
                    // Context = new StockContext();
                    //  Context.Configuration.AutoDetectChangesEnabled = false;
                }
            }
            Context.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public StockEntity GetByName(string name)
        {
            var baseQuery = (IQueryable<StockEntity>)Context.Stocks; ;
            var result = baseQuery.Where(t => t.Name == name);

            return result.FirstOrDefault();         

        }
    }
}
