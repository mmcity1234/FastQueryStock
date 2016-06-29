using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FastQueryStock.Common
{
    public class ValueColorHelper
    {
        /// <summary>
        /// 取得是否為漲跌停板的背景色
        /// </summary>
        /// <param name="compareValue">compare value</param>
        /// <returns></returns>
        public static Brush GetValueBackgroundColor(string compareValue, string limitDown, string limitUp)
        {
            Brush result = null;
            if (!string.IsNullOrEmpty(compareValue) && !string.IsNullOrEmpty(limitDown) && !string.IsNullOrEmpty(limitUp))
            {
                decimal limitUpValue = Convert.ToDecimal(limitUp);
                decimal limitDownValue = Convert.ToDecimal(limitDown);
                decimal value = Convert.ToDecimal(compareValue);
                if (value >= limitUpValue)
                    result = Brushes.Red;
                else if (value <= limitDownValue)
                    result = Brushes.Green;
            }
            return result;
        }

        public static Brush GetValueForegroundColor(string compareValue, string baseValue, string limitDown, string limitUp)
        {
            Brush result = Brushes.Black;
            if (!string.IsNullOrEmpty(compareValue) && !string.IsNullOrEmpty(baseValue))
            {
                // if the stock value is limit up or limit down, return the wihte color
                Brush backgroundColor = GetValueBackgroundColor(compareValue, limitDown, limitUp);
                if (backgroundColor == Brushes.Red || backgroundColor == Brushes.Green)
                    return Brushes.White;

                return GetValueForegroundColor(compareValue, baseValue);
            }
            return result;
        }

        public static Brush GetValueForegroundColor(string compareValue, string baseValue)
        {
            Brush result = Brushes.Black;
            if (!string.IsNullOrEmpty(compareValue) && !string.IsNullOrEmpty(baseValue))
            {              
                decimal valueDiff = Convert.ToDecimal(compareValue) - Convert.ToDecimal(baseValue);

                if (valueDiff > 0)
                    result = Brushes.Red;
                else if (valueDiff < 0)
                    result = Brushes.Green;
            }
            return result;
        }

        public static Brush GetVolumeColor(string currentBuyPriceStr, string currentSellPriceStr, string newBuyPriceStr, string newSellPriceStr, string currentPrice)
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
    }
}
