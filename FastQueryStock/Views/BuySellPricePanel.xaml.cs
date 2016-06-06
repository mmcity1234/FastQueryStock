using FastQueryStock.ViewModels;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace FastQueryStock.Views
{
    /// <summary>
    /// BuySellPricePanel.xaml 的互動邏輯
    /// </summary>
    public partial class BuySellPricePanel : MetroWindow
    {

        public BuySellPricePanel()
        {
            InitializeComponent();
        }

        private void BuySellPricePanel_Closed(object sender, EventArgs e)
        {
            BuySellPricePanelViewModel viewModel = DataContext as BuySellPricePanelViewModel;
            if(viewModel != null)
            {
                viewModel.Close();
            }
        }
    }
}
