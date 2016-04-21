using StockSDK.Ptt;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastQueryStock.Service
{
    public interface IPttStockQueryService
    {
        /// <summary>
        /// According to the count number to get the articles from last page
        /// </summary>
        /// <param name="count">number of article want to get</param>
        /// <returns></returns>
        Task<List<PttArticleData>> GetLastArticle(int count);
    }
}