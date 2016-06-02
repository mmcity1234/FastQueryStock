using FastQueryStock.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FastQueryStock.ViewModels.Controls
{
    public class BuySellPriceItem : BaseViewModel
    {
        private String _buyPrice;
        private String _buyQuantity;
        private String _sellPrice;
        private String _sellQuantity;
        private RealTimeStockItem _stockItem;

        public BuySellPriceItem(RealTimeStockItem stockItem)
        {
            _stockItem = stockItem;
        }

        public String BuyPrice
        {
            get { return _buyPrice; }
            set
            {
                _buyPrice = value;
                NotifyPropertyChanged("BuyPrice");
            }

        }
        public String BuyQuantity
        {
            get { return _buyQuantity; }
            set
            {
                _buyQuantity = value;
                NotifyPropertyChanged("BuyQuantity");
            }

        }
        public String SellPrice
        {
            get { return _sellPrice; }
            set
            {
                _sellPrice = value;
                NotifyPropertyChanged("SellPrice");
            }
        }

        public String SellQuantity
        {
            get { return _sellQuantity; }
            set
            {
                _sellQuantity = value;
                NotifyPropertyChanged("SellQuantity");
            }
        }

        public Brush SellPriceColor
        {
            get { return ValueColorHelper.GetValueForegroundColor(_stockItem.OpenPrice, _stockItem.YesterdayPrice, _stockItem.LimitDown, _stockItem.LimitUp); }
        }

        public Brush SellPriceBackgroundColor
        {
            get { return ValueColorHelper.GetValueBackgroundColor(_stockItem.HighestPrice, _stockItem.LimitDown, _stockItem.LimitUp); }
        }

        public Brush BuyPriceColor
        {
            get { return ValueColorHelper.GetValueForegroundColor(_stockItem.OpenPrice, _stockItem.YesterdayPrice, _stockItem.LimitDown, _stockItem.LimitUp); }
}

        public Brush BuyPriceBackgroundColor
{
            get { return ValueColorHelper.GetValueBackgroundColor(_stockItem.HighestPrice, _stockItem.LimitDown, _stockItem.LimitUp); }
}
    }
}
