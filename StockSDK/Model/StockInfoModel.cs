using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Model
{
    /// <summary>
    /// Record the basic information of the stock
    /// </summary>
    public class StockInfoModel : BaseModel
    {
        private const string TW_TSE = "tse";
        private const string TW_OTC = "otc";
        private const string TSE_CONST = "上市";
        private const string OTC_CONST = "上櫃";

        /// <summary>
        /// 產業類別
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 取得上市櫃中文
        /// </summary>
        public string MarketType { get; set; }


        /// <summary>
        /// 上市上櫃代碼
        /// </summary>
        public string ExchangeTpeyKey
        {
            get
            {
                if (MarketType == TSE_CONST)
                    return TW_TSE;
                else if (MarketType == OTC_CONST)
                    return TW_OTC;
                else
                    return MarketType;
            }
        }


    }
}
