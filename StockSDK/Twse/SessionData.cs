using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Twse
{
    public class SessionData
    {
        public SessionData() { }
        public SessionData(CookieContainer cookie)
        {
            Cookie = cookie;
        }
        public CookieContainer Cookie { get; set; }
    }
}
