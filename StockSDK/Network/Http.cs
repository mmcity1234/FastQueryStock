using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Network
{
    public static class Http
    {
        public async static Task<string> GetAsync(string uri)
        {
            HttpWebRequest req = WebRequest.CreateHttp(uri);
            req.ContentType = "application/x-www-form-urlencoded";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.80 Safari/537.36";
            req.Method = "GET";
            req.Timeout = 10000;

            HttpWebResponse response = (HttpWebResponse)await req.GetResponseAsync();
            Encoding encoding = Encoding.UTF8;
            if (response != null && !string.IsNullOrEmpty(response.CharacterSet))
            {
                if (response.CharacterSet == "MS950")
                    encoding = Encoding.GetEncoding(950);
                else
                    encoding = Encoding.GetEncoding(response.CharacterSet);
            }

            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);
            string htmlContext = sr.ReadToEnd().Trim();
            response.Close();
            sr.Close();
            return htmlContext;
        }

        public async static Task<string> GetAsync(string URI, bool isKeepAlive, CookieContainer cookies)
        {
            HttpWebRequest req = WebRequest.CreateHttp(URI);
            req.ContentType = "application/x-www-form-urlencoded";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.80 Safari/537.36";
            req.CookieContainer = cookies;
            req.KeepAlive = isKeepAlive;

            req.Method = "GET";

            WebResponse resp = await req.GetResponseAsync();
            if (resp == null)
                return string.Empty;
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string result = sr.ReadToEnd().Trim();
            resp.Close();
            sr.Close();
            return result;
        }
    }

}
