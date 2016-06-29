using Newtonsoft.Json;
using StockSDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Twse
{
    [JsonObject(MemberSerialization.OptOut)]
    public class StockData
    {
        private string _currentPrice;

        /// <summary>
        /// 股票代號
        /// </summary>
        [JsonProperty(PropertyName = "c")]
        public string Id { get; set; }

        /// <summary>
        /// 上市上櫃代號
        /// </summary>
        [JsonProperty(PropertyName = "ex")]
        public string ExchangeTypeKey { get; set; }

        /// <summary>
        /// 公司簡稱
        /// </summary>
        [JsonProperty(PropertyName = "n")]
        public string Name { get; set; }

        /// <summary>
        /// 開盤
        /// </summary>
        [JsonProperty(PropertyName = "o")]
        public string OpenPrice { get; set; }

        /// <summary>
        /// 最低價
        /// </summary>
        [JsonProperty(PropertyName = "l")]
        public string LowestPrice { get; set; }

        /// <summary>
        /// 最高價
        /// </summary>
        [JsonProperty(PropertyName = "h")]
        public string HighestPrice { get; set; }

        /// <summary>
        /// 跌停價
        /// </summary>
        [JsonProperty(PropertyName = "w")]
        public string LimitDown { get; set; }

        /// <summary>
        /// 累計成交量
        /// </summary>
        [JsonProperty(PropertyName = "v")]
        public string Volumes { get; set; }

        /// <summary>
        /// 當筆成交量
        /// </summary>
        [JsonProperty(PropertyName = "tv")]
        public string CurrentTimeVolumes { get; set; }

        /// <summary>
        /// 漲停價
        /// </summary>
        [JsonProperty(PropertyName = "u")]
        public string LimitUp { get; set; }

        /// <summary>
        /// 最新一筆成交時間
        /// </summary>
        [JsonProperty(PropertyName = "t")]
        public string LastTradeTime { get; set; }

        /// <summary>
        /// 成交價
        /// </summary>
        [JsonProperty(PropertyName = "z")]
        public string CurrentPrice
        {
            get
            {
                return string.IsNullOrEmpty(_currentPrice) ?
                    MediatePrice :
                    _currentPrice;
            }
            set { _currentPrice = value; }
        }

        /// <summary>
        /// 試搓價格
        /// </summary>
        [JsonProperty(PropertyName = "pz")]
        public string MediatePrice { get; set; }

        /// <summary>
        /// 昨收價格
        /// </summary>
        [JsonProperty(PropertyName = "y")]
        public string YesterdayPrice { get; set; }

        /// <summary>
        /// 揭示買價(從高到低，以_分隔資料), e.g : 27.40_27.35_27.30_27.25_27.20_
        /// </summary>
        [JsonProperty(PropertyName = "b")]
        public string BuyPriceList { get; set; }

        /// <summary>
        /// 揭示買量(配合b，以_分隔資料) e.g. : 4_23_79_20_48_
        /// </summary>
        [JsonProperty(PropertyName = "g")]
        public string BuyQuantityList { get; set; }

        /// <summary>
        /// 揭示賣價(從低到高，以_分隔資料) e.g : 27.45_27.50_27.55_27.60_27.65_
        /// </summary>
        [JsonProperty(PropertyName = "a")]
        public string SellPriceList { get; set; }

        /// <summary>
        /// 揭示賣量(配合a，以_分隔資料) e.g. : 12_96_61_120_62_
        /// </summary>
        [JsonProperty(PropertyName = "f")]
        public string SellQuantityList { get; set; }


        /// <summary>
        /// 漲跌值
        /// </summary>
        public string ChangePrice
        {
            get
            {
                decimal yesterday = System.Convert.ToDecimal(YesterdayPrice);
                decimal current = System.Convert.ToDecimal(CurrentPrice);
                string changePrice = System.Convert.ToString(current - yesterday);
                if (current - yesterday > 0)
                    return "+" + changePrice;
                else
                    return changePrice;
            }
        }
        /// <summary>
        /// 漲跌百分比
        /// </summary>
        public string ChangePercentage
        {
            get
            {
                decimal yesterday = System.Convert.ToDecimal(YesterdayPrice);
                decimal current = System.Convert.ToDecimal(CurrentPrice);
                return System.Convert.ToString(Math.Round((current - yesterday) * 100 / yesterday, 2));
            }
        }

        public RealTimeStockModel Convert()
        {
            return new RealTimeStockModel
            {
                ChangePercentage = this.ChangePercentage,
                ChangePrice = this.ChangePrice,
                CurrentPrice = this.CurrentPrice,
                HighestPrice = this.HighestPrice,
                Id = this.Id,
                LastTradeTime = this.LastTradeTime,
                LowestPrice = this.LowestPrice,
                Name = this.Name,
                OpenPrice = this.OpenPrice,
                Volumes = this.Volumes,
                CurrentTimeVolumes = this.CurrentTimeVolumes,
                BuyPriceList = this.BuyPriceList,
                BuyQuantityList = this.BuyQuantityList,
                LimitDown = this.LimitDown,
                LimitUp = this.LimitUp,
                SellPriceList = this.SellPriceList,
                SellQuantityList = this.SellQuantityList,
                YesterdayPrice = this.YesterdayPrice,
                ExchangeTypeKey = this.ExchangeTypeKey
            };
        }
    }
}
