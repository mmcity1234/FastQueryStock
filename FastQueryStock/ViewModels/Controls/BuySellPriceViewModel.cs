using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.ViewModels.Controls
{
    public class BuySellPriceViewModel
    {
        private RealTimeStockItem _stockItem;

        public ObservableCollection<BuySellPriceItem> BuySellList { get; set; }


        public BuySellPriceViewModel(RealTimeStockItem stockItem)
        {
            _stockItem = stockItem;
            BuySellList = new ObservableCollection<BuySellPriceItem>();
        }
        public void Load()
        {
            int priceCount = _stockItem.BuyPriceList.Split('_').Length;
            int sellCount = _stockItem.SellPriceList.Split('_').Length;
            int initSize = priceCount > sellCount ? priceCount : sellCount;
            for (int i = 0; i < initSize; i++)
                BuySellList.Add(new BuySellPriceItem(_stockItem));

            FillValue(_stockItem.BuyPriceList, BuySellList, (value, model) => model.BuyPrice = value);
            FillValue(_stockItem.BuyQuantityList, BuySellList, (value, model) => model.BuyQuantity = value);
            FillValue(_stockItem.SellPriceList, BuySellList, (value, model) => model.SellPrice = value);
            FillValue(_stockItem.SellQuantityList, BuySellList, (value, model) => model.SellQuantity = value);


        }
        private void FillValue(string stockValueListStr, ObservableCollection<BuySellPriceItem> list, Action<string, BuySellPriceItem> action)
        {
            string[] stockValueList = stockValueListStr.Split('_');
            for (int i = 0; i < stockValueList.Length; i++)
            {
                action(stockValueList[i], list[i]);
            }
        }
}
}
