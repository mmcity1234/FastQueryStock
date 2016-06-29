using FastQueryStock.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FastQueryStock.ViewModels.Controls
{
    public class BuySellVolumeItem : BaseViewModel
    {
        public string CurrentPrice { get; set; }
        public string Volumes { get; set; }
        public Brush VolumesColor { get; set; }
        public Brush CurrentPriceColor { get; set; }
        public Brush CurrentPriceBackgroundColor { get; set; }

        public string Time { get; set; }

        public BuySellVolumeItem() { }

        public BuySellVolumeItem(RealTimeStockItem item)
        {            
            CurrentPrice = item.CurrentPrice;
            Volumes = item.CurrentTimeVolumes;
            CurrentPriceColor = item.CurrentPriceValueColor;
            CurrentPriceBackgroundColor = item.CurrentPriceValueBackgroundColor;
            VolumesColor = ValueColorHelper.GetVolumeColor(string.Empty, string.Empty, item.BuyPriceList, item.SellPriceList, item.CurrentPrice);

            Time = item.LatestTime;
        }
    }
}
