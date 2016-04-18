using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Model
{
    public class ChartPointModel
    {        
        /// <summary>
        /// Utc time with million seconds
        /// </summary>
        public string Timestemp { get; set; }
                
        public string ExchangeVolume { get; set; }
                
        public string Value { get; set; }
    }
}
