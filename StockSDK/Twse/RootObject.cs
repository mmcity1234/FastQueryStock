using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Twse
{
    [JsonObject(MemberSerialization.OptOut)]
    public class RootObject<TStock>
    {
        [JsonProperty(PropertyName = "msgArray")]
        public List<TStock> StockMessage { get; set; }

        [JsonProperty(PropertyName = "userDelay")]
        public int Delay { get; set; }   

        [JsonProperty(PropertyName = "queryTime")]
        public TimeMessage Time { get; set; }

        /// <summary>
        /// if success return "OK"
        /// </summary>
        [JsonProperty(PropertyName = "rtmessage")]
        public string Result { get; set; }
       
    }
}
