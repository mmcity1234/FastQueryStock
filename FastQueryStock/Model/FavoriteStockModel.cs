using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Model
{
    public class FavoriteStockModel
    {
        public string id { get; set; }

        /// <summary>
        /// record the sequence of the stock in this category
        /// </summary>
        public int Order { get; set; }

        public int CustomCategory { get; set; }

    }
}
