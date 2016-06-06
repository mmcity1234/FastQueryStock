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
        private RealTimeStockItem _stockItem;
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


        public BuySellPriceViewModel(RealTimeStockItem stockItem)
        {
            _stockItem = stockItem;
            BuySellList = new List<BuySellPriceItem>();

            NotificationCenter.Instance.RegisterEvent<RealTimeStockItem>(EventType.RealTimeStockValue, Update_EventHandler);
        }

        /// <summary>
        /// Release the resouce
        /// </summary>
        public void Release()
        {
            NotificationCenter.Instance.RemoveEvent<RealTimeStockItem>(EventType.RealTimeStockValue, Update_EventHandler);
        }

        public void Load()
        {
            List<BuySellPriceItem> priceItemList = new List<BuySellPriceItem>();
            int priceCount = skipUnderLineWord(_stockItem.BuyPriceList).Split('_').Length;
            int sellCount = skipUnderLineWord(_stockItem.SellPriceList).Split('_').Length;
            int initSize = priceCount > sellCount ? priceCount : sellCount;
            for (int i = 0; i < initSize; i++)
                priceItemList.Add(new BuySellPriceItem(_stockItem));

            FillValue(skipUnderLineWord(_stockItem.BuyPriceList), priceItemList, (value, model) => model.BuyPrice = value);
            FillValue(skipUnderLineWord(_stockItem.BuyQuantityList), priceItemList, (value, model) => model.BuyQuantity = value);
            FillValue(skipUnderLineWord(_stockItem.SellPriceList), priceItemList, (value, model) => model.SellPrice = value);
            FillValue(skipUnderLineWord(_stockItem.SellQuantityList), priceItemList, (value, model) => model.SellQuantity = value);

            BuySellList = priceItemList;
        }

       

        private void FillValue(string stockValueListStr, List<BuySellPriceItem> list, Action<string, BuySellPriceItem> action)
        {
            if (string.IsNullOrEmpty(stockValueListStr))
                return;

            stockValueListStr = stockValueListStr.Substring(0, stockValueListStr.Length - 1);
            string[] stockValueList = stockValueListStr.Split('_');
            for (int i = 0; i < stockValueList.Length; i++)
            {
                action(stockValueList[i], list[i]);
            }
        }
        private string skipUnderLineWord(string priceString)
        {
            if (string.IsNullOrEmpty(priceString))
                return priceString;
            if (priceString[priceString.Length - 1] == '_')
                return priceString.Substring(0, priceString.Length - 1);
            else
                return priceString;
        }


        private void Update_EventHandler(RealTimeStockItem item)
        {
            if (item.Id == _stockItem.Id)
            {
                _stockItem = item;
                Load();
            }
        }
    }
}
