
using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FastQueryStock.Model.Charts;
using OxyPlot;

namespace FastQueryStock.Views.Chart
{
    /// <summary>
    /// StockValueChartControl.xaml 的互動邏輯
    /// </summary>
    public partial class StockValueChartControl : UserControl, IStockValueChartControlView
    {
        public StockValueChartControl()
        {
            InitializeComponent();
        }

        public void DrawExtraLine(List<ExtraLine> lines)
        {            
            double[] lineValues = new double[lines.Count];
            for (int i = 0; i < lines.Count; i++)
            {
                lineValues[i] = lines[i].Value;
            }

            valueLinearAxis.ExtraGridlines = lineValues;
            valueLinearAxis.ExtraGridlineColor = Colors.Blue;
             
        }
    }
}
