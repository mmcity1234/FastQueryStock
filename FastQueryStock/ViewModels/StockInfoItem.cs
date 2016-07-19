using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.ViewModels
{
    /// <summary>
    /// The basic information of stock
    /// </summary>
    public class StockInfoItem
    {
        public string Name { get; set; }

        public string Id { get; set; }


        /// <summary>
        /// 使用者自訂的分類頁籤索引
        /// </summary>
        public int CustomCategory { get; set; }

        /// <summary>
        /// 產業類別
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 上市櫃
        /// </summary>
        public string MarketType { get; set; }

        /// <summary>
        /// Record the stock order number
        /// </summary>
        public int Order { get; internal set; }
    }
}
