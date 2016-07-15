using FastQueryStock.Common;
using FastQueryStock.Event;
using FastQueryStock.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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

        private static object _buySellCollectionLockObj = new object();

        public ObservableCollection<BuySellVolumeItem> BuySellVolumeList { get; set; }

        public BuySellPriceViewModel PriceListViewModel { get; set; }

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

     

        public BuySellPricePanelViewModel()
        {
            PriceListViewModel = new BuySellPriceViewModel();
            BuySellVolumeList = new ObservableCollection<BuySellVolumeItem>();
            BindingOperations.EnableCollectionSynchronization(BuySellVolumeList, _buySellCollectionLockObj);

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
                VoluemsColor = ValueColorHelper.GetVolumeColor(string.Empty, string.Empty, stockItem.BuyPriceList, stockItem.SellPriceList, stockItem.CurrentPrice);
            else
                VoluemsColor = ValueColorHelper.GetVolumeColor(_currentStockItem.BuyPriceList, _currentStockItem.SellPriceList, stockItem.BuyPriceList, stockItem.SellPriceList, stockItem.CurrentPrice);

            PriceListViewModel.Load(stockItem);

            // 分時明細
            if(BuySellVolumeList.FirstOrDefault(x => x.Time == stockItem.LatestTime) == null && stockItem.CurrentTimeVolumes != "0")
                BuySellVolumeList.Insert(0, new BuySellVolumeItem(stockItem, VoluemsColor));

            _currentStockItem = stockItem;
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
