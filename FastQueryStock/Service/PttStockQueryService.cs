using StockSDK.Ptt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Service
{
    public class PttStockQueryService : IPttStockQueryService
    {
        public async Task<List<PttArticleData>> GetLastArticle(int count)
        {
            List<PttArticleData> resultArticles = new List<PttArticleData>();
            PttClient client = new PttClient();
            List<PttArticleData> articleList = await client.GetPageArticles(PttClient.PTT_STOCK_LAST_PAGE_URL);

            resultArticles.AddRange(articleList);
            string currentPageUrl = PttClient.PTT_STOCK_LAST_PAGE_URL;
            while (resultArticles.Count < count)
            {
                string prePageUrl = await client.GetPreviousPageUrl(currentPageUrl);
                articleList = await client.GetPageArticles(prePageUrl);
                // Check if add all article to list in this page or add the partial articles
                if (count - resultArticles.Count > articleList.Count)
                {
                    resultArticles.InsertRange(0, articleList);
                    currentPageUrl = prePageUrl;
                }
                else
                {
                    int skipCount = articleList.Count - (count - resultArticles.Count);
                    resultArticles.InsertRange(0, articleList.Skip(skipCount));
                    break;
                }
            }
            return resultArticles;
        }
    }
}
