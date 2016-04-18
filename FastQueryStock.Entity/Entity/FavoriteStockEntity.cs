using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Entity.Entity
{
    public class FavoriteStockEntity : BaseEntity
    {
        public string StockId { get; set; }
        [ForeignKey("StockId")]
        public StockEntity ParentStock { get; set; }

        /// <summary>
        /// record the sequence of the stock in this category
        /// </summary>
        public int Order { get; set; }

        public int CustomCategory { get; set; }
    }
}
