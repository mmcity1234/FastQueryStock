using FastQueryStock.Common;
using FastQueryStock.Event;
using FastQueryStock.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FastQueryStock.ViewModels
{
    public class BuySellPricePanelViewModel : BaseViewModel
    {
        private string _title;
        private string _currentVolumes;
        private string _StockId;
        private Brush _voluemsColor;
        private RealTimeStockItem _currentStockItem;
        private string _currentPrice;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        public string CurrentPrice
        {
            get { return _currentPrice; }
            set
            {
                _currentPrice = value;
                NotifyPropertyChanged("CurrentPrice");
            }
        }
        /// <summary>
        /// 目前當筆成交數
        /// </summary>
        public string CurrentVolumes
        {
            get { return _currentVolumes; }
            set
            {
                _currentVolumes = value;
                NotifyPropertyChanged("CurrentVolumes");
            }
        }

        public Brush VoluemsColor
        {
            get { return _voluemsColor; }
            set
            {
                _voluemsColor = value;
                NotifyPropertyChanged("VoluemsColor");
            }
        }

        public BuySellPriceViewModel PriceListViewModel { get; set; }


        public BuySellPricePanelViewModel()
        {
            PriceListViewModel = new BuySellPriceViewModel();
            NotificationCenter.Instance.RegisterEvent<RealTimeStockItem>(EventType.RealTimeStockValue, Update_EventHandler);
        }

        public void Load(RealTimeStockItem stockItem)
        {
            _StockId = stockItem.Id;
            Title = string.Format("{0} ({1}) 五檔掛單", stockItem.Name, stockItem.Id);
            CurrentVolumes = stockItem.CurrentTimeVolumes;
            CurrentPrice = stockItem.CurrentPrice;

            // set the color of current stock volumes
            if (_currentStockItem == null)
                VoluemsColor = GetVolumeColor(string.Empty, string.Empty, stockItem.BuyPriceList, stockItem.SellPriceList, stockItem.CurrentPrice);
            else
                VoluemsColor = GetVolumeColor(_currentStockItem.BuyPriceList, _currentStockItem.SellPriceList, stockItem.BuyPriceList, stockItem.SellPriceList, stockItem.CurrentPrice);

            PriceListViewModel.Load(stockItem);
            _currentStockItem = stockItem;
        }

        private SolidColorBrush GetVolumeColor(string currentBuyPriceStr, string currentSellPriceStr, string newBuyPriceStr, string newSellPriceStr, string currentPrice)
        {
            var currentBuyPriceList = currentBuyPriceStr.Split('_');
            var currentSellPriceList = currentSellPriceStr.Split('_');
            var newBuyPriceList = newBuyPriceStr.Split('_');
            var newSellPriceList = newSellPriceStr.Split('_');
            if (string.IsNullOrEmpty(currentBuyPriceStr) || string.IsNullOrEmpty(currentSellPriceStr))
            {
                if (newBuyPriceList.Contains(currentPrice))
                    return Brushes.Green;
                else if (newSellPriceList.Contains(currentPrice))
                    return Brushes.Red;
            }
            // 持續賣
            else if (currentBuyPriceList.Contains(currentPrice) && newBuyPriceList.Contains(currentPrice))
                return Brushes.Green;
            // 持續買
            else if (currentSellPriceList.Contains(currentPrice) && newSellPriceList.Contains(currentPrice))
                return Brushes.Red;
            // 單檔掛價買完
            else if (currentSellPriceList.Contains(currentPrice) && newBuyPriceList.Contains(currentPrice))
                return Brushes.Red;
            // 單檔掛價賣完
            else if (currentBuyPriceList.Contains(currentPrice) && newSellPriceList.Contains(currentPrice))
                return Brushes.Green;
            return Brushes.Black;
        }

        public void Close()
        {
            NotificationCenter.Instance.RemoveEvent<RealTimeStockItem>(EventType.RealTimeStockValue, Update_EventHandler);
        }
        private void Update_EventHandler(RealTimeStockItem item)
        {
            if (item.Id == _StockId)
            {
                Load(item);
            }
        }
    }
}
