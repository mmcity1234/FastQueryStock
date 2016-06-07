using FastQueryStock.Common;
using FastQueryStock.Event;
using FastQueryStock.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastQueryStock.ViewModels
{
    public class BuySellPricePanelViewModel : BaseViewModel
    {
        private string _title;
        private string _currentVolumes;
        private string _StockId;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
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
            PriceListViewModel.Load(stockItem);
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
