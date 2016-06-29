using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Model
{
    public class RealTimeStockModel : BaseModel
    {     
        /// <summary>
        /// 開盤
        /// </summary>
        public string OpenPrice { get; set; }

        /// <summary>
        /// 最低價
        /// </summary>
        public string LowestPrice { get; set; }

        /// <summary>
        /// 最高價
        /// </summary>
        public string HighestPrice { get; set; }

        /// <summary>
        /// 跌停價
        /// </summary>
        public string LimitDown { get; set; }

        /// <summary>
        /// 累計成交量
        /// </summary>
        public string Volumes { get; set; }

        /// <summary>
        /// 當筆成交量
        /// </summary>
        public string CurrentTimeVolumes { get; set; }

        /// <summary>
        /// 漲停價
        /// </summary>
        public string LimitUp { get; set; }

        /// <summary>
        /// 最新一筆成交時間
        /// </summary>
        public string LastTradeTime { get; set; }

        /// <summary>
        /// 漲跌值
        /// </summary>
        public string ChangePrice { get; set; }
        /// <summary>
        /// 漲跌百分比
        /// </summary>
        public string ChangePercentage { get; set; }

        /// <summary>
        /// 成交價
        /// </summary>
        public string CurrentPrice { get; set; }

        /// <summary>
        /// 昨收價格
        /// </summary>
        public string YesterdayPrice { get; set; }

        /// <summary>
        /// 揭示買價(從高到低，以_分隔資料), e.g : 27.40_27.35_27.30_27.25_27.20_
        /// </summary>
        public string BuyPriceList { get; set; }

        /// <summary>
        /// 揭示買量(配合b，以_分隔資料) e.g. : 4_23_79_20_48_
        /// </summary>
        public string BuyQuantityList { get; set; }

        /// <summary>
        /// 揭示賣價(從低到高，以_分隔資料) e.g : 27.45_27.50_27.55_27.60_27.65_
        /// </summary>
        public string SellPriceList { get; set; }

        /// <summary>
        /// 揭示賣量(配合a，以_分隔資料) e.g. : 12_96_61_120_62_
        /// </summary>
        public string SellQuantityList { get; set; }

        /// <summary>
        /// 系統查詢的時間
        /// </summary>
        //public string QuerySystemTime { get; set; }

        /// <summary>
        /// 取得或設定上市或上櫃代號
        /// </summary>
        public string ExchangeTypeKey { get; set; }
    }
}
