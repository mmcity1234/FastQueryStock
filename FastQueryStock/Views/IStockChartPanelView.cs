using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Views
{
    /// <summary>
    /// Define the main chart view operation for view model
    /// </summary>
    public interface IStockChartPanelView
    {
        IStockValueChartControlView StockValueChartView { get; } 
    }
}
