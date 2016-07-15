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

        public BuySellVolumeItem(RealTimeStockItem currentItem, Brush volumeColor)
        {            
            CurrentPrice = currentItem.CurrentPrice;
            Volumes = currentItem.CurrentTimeVolumes;
            CurrentPriceColor = currentItem.CurrentPriceValueColor;
            CurrentPriceBackgroundColor = currentItem.CurrentPriceValueBackgroundColor;
            VolumesColor = volumeColor;
            Time = currentItem.LatestTime;
        }
    }
}
