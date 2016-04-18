using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK
{
    /// <summary>
    /// The exception is when time out to get stock data from web api
    /// </summary>
    public class QueryDataTimeOutException : Exception
    {
        public QueryDataTimeOutException()
        {

        }

        public QueryDataTimeOutException(string message) : base(message) { }
    }
}
