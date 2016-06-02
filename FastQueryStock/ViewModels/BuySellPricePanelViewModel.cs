using FastQueryStock.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.ViewModels
{
    public class BuySellPricePanelViewModel : BaseViewModel
    {
        private string _title;

        private string Title
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
            Title = stockItem.Name;
        }

        public void Load()
        {
            PriceListViewModel.Load();
        }
    }
}
