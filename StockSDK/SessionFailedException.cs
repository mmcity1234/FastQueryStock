using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK
{
    public class SessionFailedException : Exception
    {
        public SessionFailedException()
        {
        }

        public SessionFailedException(string message) : base(message) { }
    }
}
