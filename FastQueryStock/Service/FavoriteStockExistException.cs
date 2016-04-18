using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Service
{
    public class FavoriteStockExistException : Exception
    {
        public string StockId { get; set; }
        public string StockName { get; set; }
        public FavoriteStockExistException(string id, string name) :
            base("Teh stock " + name + "(" + id + ")" + "is already exist in favorite table")
        { }

        public FavoriteStockExistException(string message) : base(message) { }

    }
}
