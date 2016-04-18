using Newtonsoft.Json;
using StockSDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Google
{
    [JsonObject(MemberSerialization.OptOut)]
    public class StockData
    {
        [JsonProperty(PropertyName = "t")]
        public string Id { get; set; }
        /// <summary>
        /// 國家
        /// </summary>
        [JsonProperty(PropertyName = "e")]
        public string Country { get; set; }

        /// <summary>
        /// 最後時間
        /// </summary>
        [JsonProperty(PropertyName = "lt")]
        public string LatestTime { get; set; }
        /// <summary>
        /// 漲跌值
        /// </summary>
        [JsonProperty(PropertyName = "c")]
        public string ChangePrice { get; set; }
        /// <summary>
        /// 漲跌百分比
        /// </summary>
        [JsonProperty(PropertyName = "cp")]
        public string ChangePercentage { get; set; }

        /// <summary>
        /// 目前價格
        /// </summary>
        [JsonProperty(PropertyName = "l")]
        public string CurrentPrice { get; set; }
        /// <summary>
        /// 開盤價
        /// </summary>
        [JsonProperty(PropertyName = "op")]
        public string OpenPrice { get; set; }

        /// <summary>
        /// 最高價
        /// </summary>
        [JsonProperty(PropertyName = "hi")]
        public string HighestPrice { get; set; }

        /// <summary>
        /// 最低價
        /// </summary>
        [JsonProperty(PropertyName = "lo")]
        public string LowestPrice { get; set; }

        /// <summary>
        /// 成交股數
        /// </summary>
        [JsonProperty(PropertyName = "vo")]
        public string Volumes { get; set; }

        /// <summary>
        /// 過去一年最高點
        /// </summary>
        [JsonProperty(PropertyName = "hi52")]
        public string Highest52 { get; set; }

        /// <summary>
        /// 過去一年最低點
        /// </summary>
        [JsonProperty(PropertyName = "lo52")]
        public string Lowest52 { get; set; }

        /// <summary>
        /// 市值
        /// </summary>
        [JsonProperty(PropertyName = "mc")]
        public string MarketCap { get; set; }
        /// <summary>
        /// 本益比
        /// </summary>
        [JsonProperty(PropertyName = "pe")]
        public string PE { get; set; }


        /// <summary>
        /// 每股盈餘
        /// </summary>
        [JsonProperty(PropertyName = "eps")]
        public string EPS { get; set; }

        [JsonProperty(PropertyName = "shares")]
        public string Shares { get; set; }



        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

     
        public static RealTimeStockModel CreateNon(string id)
        {
            return new RealTimeStockModel { Id = id };
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
                QuerySystemTime = this.LatestTime,
                LowestPrice = this.LowestPrice,
                Name = this.Name,
                OpenPrice = this.OpenPrice,
                Volumes = this.Volumes
            };
        }

    }
}

