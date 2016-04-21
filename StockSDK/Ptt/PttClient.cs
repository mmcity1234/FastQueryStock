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

        private const string SPLIT_TEXT = "[公告] 精華區導覽";

        public async Task<List<PttArticleData>> GetPageArticles(string pageUrl)
        {
            if (string.IsNullOrEmpty(pageUrl))
                throw new Exception("Ptt的股票網址參數錯誤");
            string html = await Http.GetAsync(pageUrl);
            var articles = ParsePttPage(html);

            return articles;
        }

        /// <summary>
        /// Get previous page url by current page's url
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetPreviousPageUrl(string currentPageUrl)
        {
            string html = await Http.GetAsync(currentPageUrl);
            return RetirvePreviousPageUrl(html);

        }

        private string RetirvePreviousPageUrl(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var node = doc.DocumentNode.Descendants("a").
                Where(x => x.GetAttributeValue("class", string.Empty) == "btn wide" && x.InnerText.Contains("上頁")).
                FirstOrDefault();

            if (node == null)
                throw new Exception("無法取得上一頁的網址，目前可能已經回到第一頁");

            return PTT_URL + node.Attributes["href"].Value;
        }

        private List<PttArticleData> ParsePttPage(string html)
        {
            List<PttArticleData> articles = new List<PttArticleData>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var htmlNodes = doc.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", string.Empty) == "r-ent");

            foreach (var node in htmlNodes)
            {
                try
                {
                    PttArticleData data = new PttArticleData();
                    // find the title and url
                    if (node.Descendants("a") != null && node.Descendants("a").FirstOrDefault() != null)
                    {
                        HtmlNode titleNode = node.Descendants("a").FirstOrDefault();
                        data.Title = titleNode.InnerText;
                        data.Url = PTT_URL + titleNode.GetAttributeValue("href", string.Empty);
                    }
                    else
                    {
                        data.IsDeleted = true;
                        data.Title = string.Empty;
                        var titleNodes = node.Descendants("div").Where(x => x.GetAttributeValue("class", string.Empty) == "title");
                        if (titleNodes != null)
                            data.Title = titleNodes.First().InnerHtml.Trim();
                    }

                    if (data.Title.StartsWith(SPLIT_TEXT))
                        break;

                    data.Date = node.Descendants("div").Where(x => x.Attributes["class"].Value == "date").First().InnerText;
                    data.Author = node.Descendants("div").Where(x => x.Attributes["class"].Value == "author").First().InnerText;

                    articles.Add(data);
                }
                catch (Exception e)
                {
                    //
                }
            }

            return articles;
        }

    }
}
