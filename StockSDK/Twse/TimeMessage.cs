using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Twse
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TimeMessage
    {
        [JsonProperty(PropertyName = "sysTime")]
        public string SysTime { get; set; }
        [JsonProperty(PropertyName = "sysDate")]
        public string SysDate { get; set; }
       
    }
}
