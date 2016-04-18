using FastQueryStock.Model.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.Views
{
    /// <summary>
    /// Define the stock value char operation for view model
    /// </summary>
    public interface IStockValueChartControlView
    {
        void DrawExtraLine(List<ExtraLine> lines);
    }
}
