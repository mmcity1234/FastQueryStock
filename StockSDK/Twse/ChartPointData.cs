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
    public class ChartPointData
    {
        [JsonProperty(PropertyName = "t")]
        public string Timestemp { get; set; }

        [JsonProperty(PropertyName = "s")]
        public string ExchangeVolume { get; set; }

        [JsonProperty(PropertyName = "c")]
        public string Value { get; set; }

        internal ChartPointModel Convert()
        {
            return new ChartPointModel
            {
                ExchangeVolume = this.ExchangeVolume,
                Timestemp = this.Timestemp + "00000",
                Value = this.Value
            };
        }
    }
}
