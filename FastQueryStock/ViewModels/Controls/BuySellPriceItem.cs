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
        private double _buyQuantityPercentage;
        private double _sellQuantityPercentage;

        private const double QUANTITY_WIDTH = 50;

        public BuySellPriceItem(RealTimeStockItem stockItem)
        {
            _stockItem = stockItem;
        }

        /// <summary>
        /// Get the stock quantity width that show on the grid
        /// </summary>
        public double QuantityWidth
        {
            get { return QUANTITY_WIDTH; }
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

        public double BuyQuantityPercentage
        {
            get { return _buyQuantityPercentage; }
            set
            {
                _buyQuantityPercentage = value;
                NotifyPropertyChanged("BuyQuantityPercentage");
            }
        }
        public double SellQuantityPercentage
        {
            get { return _sellQuantityPercentage; }
            set
            {
                _sellQuantityPercentage = value;
                NotifyPropertyChanged("SellQuantityPercentage");
            }
        }

        public Brush SellPriceColor
        {
            get { return ValueColorHelper.GetValueForegroundColor(SellPrice, _stockItem.YesterdayPrice, _stockItem.LimitDown, _stockItem.LimitUp); }
        }

        public Brush SellPriceBackgroundColor
        {
            get { return ValueColorHelper.GetValueBackgroundColor(SellPrice, _stockItem.LimitDown, _stockItem.LimitUp); }
        }

        public Brush BuyPriceColor
        {
            get { return ValueColorHelper.GetValueForegroundColor(BuyPrice, _stockItem.YesterdayPrice, _stockItem.LimitDown, _stockItem.LimitUp); }
        }

        public Brush BuyPriceBackgroundColor
        {
            get { return ValueColorHelper.GetValueBackgroundColor(BuyPrice, _stockItem.LimitDown, _stockItem.LimitUp); }
        }
    }
}
