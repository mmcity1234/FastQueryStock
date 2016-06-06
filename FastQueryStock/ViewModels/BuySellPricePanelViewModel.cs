using FastQueryStock.Common;
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

      

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        public BuySellPriceViewModel PriceListViewModel { get; set; }

        public BuySellPricePanelViewModel(RealTimeStockItem stockItem)
        {
            PriceListViewModel = new BuySellPriceViewModel(stockItem);         
            Title = string.Format("{0} ({1}) 五檔掛單",stockItem.Name, stockItem.Id);
        }

        public void Load()
        {
            PriceListViewModel.Load();
        }

        public void Close()
        {
            PriceListViewModel.Release();
        }
    }
}
