using FastQueryStock.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Entity
{
    public interface IStockRepository : IBaseRepository<StockEntity>
    {
        StockEntity GetByName(string name);
    }
}
