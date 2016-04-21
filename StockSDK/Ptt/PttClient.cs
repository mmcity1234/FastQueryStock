using HtmlAgilityPack;
using StockSDK.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Ptt
{
    public class PttClient
    {
        public const string PTT_URL = "https://www.ptt.cc";
        public const string PTT_STOCK_LAST_PAGE_URL = "https://www.ptt.cc/bbs/Stock/index.html";

        public async Task<List<PttArticleData>> GetPageArticles(string pageUrl)
        {
            if (string.IsNullOrEmpty(pageUrl))
                throw new Exception("Ptt的股票網址參數錯誤");
            string html = await Http.GetAsync(pageUrl);
            var articles = ParsePttPage(html);

            return articles;
        }

        public async Task<string> GetPreviousPageUrl()
        {
            return await GetPreviousPageUrl(PTT_STOCK_LAST_PAGE_URL);
        }

        /// <summary>
        /// Get previous page url by current page's url
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetPreviousPageUrl(string currentPageUrl)
        {
            string html = await Http.GetAsync(PTT_STOCK_LAST_PAGE_URL);
            return RetirvePreviousPageUrl(html);

        }

        private string RetirvePreviousPageUrl(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var node = doc.DocumentNode.Descendants("a").
                Where(x => x.Attributes["class"].Value == "btn wide" && x.InnerText.Contains("上頁")).
                FirstOrDefault();

            if (node == null)
                throw new Exception("無法取得上一頁的網址，目前可能已經回到第一頁");

            return node.Attributes["href"].Value;
        }

        private List<PttArticleData> ParsePttPage(string html)
        {
            List<PttArticleData> articles = new List<PttArticleData>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var htmlNodes = doc.DocumentNode.Descendants("div").Where(x =>
            {
                if (!x.Attributes.Contains("class"))
                    return false;

                string classValue = x.Attributes["class"].Value;
                return classValue == "r-ent";


            });
            foreach (var node in htmlNodes)
            {
                PttArticleData data = new PttArticleData();
                if (node.Descendants("a").FirstOrDefault() != null)
                {
                    data.Title = node.Descendants("a").Where(x => x.Attributes["class"].Value == "title").First().InnerText;
                    data.Url = PTT_URL + node.Descendants("a").First().Attributes["href"].Value;
                }
                else
                    data.IsDeleted = true;

                data.Date = node.Descendants("div").Where(x => x.Attributes["class"].Value == "date").First().InnerText;
                data.Author = node.Descendants("div").Where(x => x.Attributes["class"].Value == "author").First().InnerText;

                articles.Add(data);
            }

            return articles;
        }

    }
}
