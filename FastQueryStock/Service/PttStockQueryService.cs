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

            while (true)
            {
                if (articleList.Count < count)
                {
                    resultArticles.AddRange(articleList);

                    string prePageUrl = await client.GetPreviousPageUrl();
                    articleList = await client.GetPageArticles(prePageUrl);
                }
                else
                {
                    int skipCount = articleList.Count - count;
                    resultArticles.AddRange(articleList.Skip(skipCount));
                    break;
                }
            }
            return resultArticles;
        }
    }
}
