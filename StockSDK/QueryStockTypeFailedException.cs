using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryStockTypeFailedException : Exception
    {
        public QueryStockTypeFailedException() { }
        
        public QueryStockTypeFailedException(string message) : base(message) { }

    }
}
