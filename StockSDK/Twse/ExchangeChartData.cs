using Newtonsoft.Json;
using StockSDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Twse
{
    /// <summary>
    /// The class is used to define the json data from TWSE and it is for drawing the real-time chart of the stock
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ExchangeChartData
    {

        [JsonProperty(PropertyName = "ex")]
        public string ExchangeType { get; set; }

        [JsonProperty(PropertyName = "ohlcArray")]
        public List<ChartPointData> ChartData { get; set; }

        [JsonProperty(PropertyName = "rtmessage")]
        public string Result { get; set; }

        [JsonProperty(PropertyName = "ch")]
        public string StockId { get; set; }

        [JsonProperty(PropertyName = "infoArray")]
        public List<StockData> StockMessage { get; set; }

        [JsonProperty(PropertyName = "rtcode")]
        public string ResultCode { get; set; }

        internal StockChartModel ConvertToModel()
        {
            StockChartModel chartModel = new StockChartModel();
            chartModel.StockId = StockId;

            if (StockMessage.Count > 0)
                chartModel.StockMessage = StockMessage[0].Convert();

            chartModel.ChartData = new List<ChartPointModel>();
            foreach (var point in ChartData)
                chartModel.ChartData.Add(point.Convert());


            return chartModel;
        }
    }
}
