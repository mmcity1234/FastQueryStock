using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Entity.Entity
{
    public class StockEntity : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// 產業類別
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 上市櫃
        /// </summary>
        public string MarketType { get; set; }
    }
}
