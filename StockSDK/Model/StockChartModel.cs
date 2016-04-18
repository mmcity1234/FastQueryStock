using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class StockChartModel
    {
        public List<ChartPointModel> ChartData { get; set; }

        public string StockId { get; set; }

        public RealTimeStockModel StockMessage { get; set; }
    }
}
