using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Network
{
    public class ParseHtmlException : Exception
    {
        public ParseHtmlException() { }
        public ParseHtmlException(string message) : base(message) { }
    }
}
