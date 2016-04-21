using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSDK.Ptt
{
    public class PttArticleData
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public string Date { get; set; }

        public string Url { get; set; }

        public bool IsDeleted { get; set; }
    }
}
