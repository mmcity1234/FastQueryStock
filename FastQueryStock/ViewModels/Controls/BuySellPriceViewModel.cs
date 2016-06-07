using FastQueryStock.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.ViewModels.Controls
{
    public class BuySellPriceViewModel : BaseViewModel
    {
        private List<BuySellPriceItem> _buySellList;

        public List<BuySellPriceItem> BuySellList
        {
            get { return _buySellList; }
            set
            {
                _buySellList = value;
                NotifyPropertyChanged("BuySellList");
            }
        }


        public BuySellPriceViewModel()
        {            
            BuySellList = new List<BuySellPriceItem>();           
        }

        public void Load(RealTimeStockItem stockItem)
        {
            List<BuySellPriceItem> priceItemList = new List<BuySellPriceItem>();
            int priceCount = stockItem.BuyPriceList.Split('_').Length;
            int sellCount = stockItem.SellPriceList.Split('_').Length;
            int initSize = priceCount > sellCount ? priceCount : sellCount;
            for (int i = 0; i < initSize; i++)
                priceItemList.Add(new BuySellPriceItem(stockItem));

            FillValue(stockItem.BuyPriceList, priceItemList, (value, model) => model.BuyPrice = value);
            FillValue(stockItem.BuyQuantityList, priceItemList, (value, model) => model.BuyQuantity = value);
            FillValue(stockItem.SellPriceList, priceItemList, (value, model) => model.SellPrice = value);
            FillValue(stockItem.SellQuantityList, priceItemList, (value, model) => model.SellQuantity = value);

            // fill the quantity percentage
            double maxBuyQuantity = priceItemList.Max(x => Convert.ToDouble(x.BuyQuantity));
            double maxSellQuantity = priceItemList.Max(x => Convert.ToDouble(x.SellQuantity));
            double maxQuantity = maxBuyQuantity > maxSellQuantity ? maxBuyQuantity : maxSellQuantity;
            FillValue(stockItem.SellQuantityList, priceItemList, (value, model) => model.SellQuantityPercentage = Convert.ToDouble(model.SellQuantity) / maxQuantity * model.QuantityWidth);
            FillValue(stockItem.BuyQuantityList, priceItemList, (value, model) => model.BuyQuantityPercentage = Convert.ToDouble(model.BuyQuantity) / maxQuantity * model.QuantityWidth);


            BuySellList = priceItemList;
        } 

        private void FillValue(string stockValueListStr, List<BuySellPriceItem> list, Action<string, BuySellPriceItem> action)
        {
            if (string.IsNullOrEmpty(stockValueListStr))
                return;

            string[] stockValueList = stockValueListStr.Split('_');
            for (int i = 0; i < stockValueList.Length; i++)
            {
                action(stockValueList[i], list[i]);
            }
        }
    }
}
