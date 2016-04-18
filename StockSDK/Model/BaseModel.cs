using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Model
{
    public class BaseModel
    {
        /// <summary>
        /// 股票代號
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 公司簡稱
        /// </summary>
        public string Name { get; set; }
    }
}
