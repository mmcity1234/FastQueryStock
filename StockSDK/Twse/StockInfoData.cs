using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Twse
{
    /// <summary>
    /// Record the basic information of stock
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class StockInfoData
    {

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// 上市或上櫃的類型
        /// </summary>
        [JsonProperty(PropertyName = "ex")]
        public string ExchangeType { get; set; }


    }
}
