using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK
{
    public class JsonConverter
    {
        public static T DeserializeFrom<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
