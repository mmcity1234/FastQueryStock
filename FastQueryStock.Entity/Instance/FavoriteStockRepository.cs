using FastQueryStock.Entity.Entity;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FastQueryStock.Entity.Context;

namespace FastQueryStock.Entity.Instance
{
    public class FavoriteStockRepository : BaseRepository<FavoriteStockEntity>, IFavoriteStockRepository
    {
        public FavoriteStockRepository(StockContext context) : base(context) { }

        public override List<FavoriteStockEntity> GetAll()
        {
            return Context.FavoriteStock.
                Include(entity => entity.ParentStock).
                ToList();
        }

        public override FavoriteStockEntity GetById(string id)
        {
            return Context.FavoriteStock.
                Include(entity => entity.ParentStock).
                FirstOrDefault(x => x.Id == id);
        }

        public override List<FavoriteStockEntity> GetAll(Expression<Func<FavoriteStockEntity, bool>> condition = null)
        {
            var dbSet = Context.Set<FavoriteStockEntity>();
            if(condition == null)
                return dbSet.Include(entity => entity.ParentStock).ToList();
            else
                return dbSet.Where(condition).Include(entity => entity.ParentStock).ToList();
        }
    }
}
