using FastQueryStock.Service;
using FastQueryStock.ViewModels;
using MahApps.Metro.Controls;
using MaterialMenu;
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
using Xceed.Wpf.Toolkit;

namespace FastQueryStock
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.AllowsTransparency = true;
            
            //viewModel = new MainViewModel(new GoogleStockQueryService(), new LocalStockService());
            viewModel = new MainViewModel(new TwseStockQueryService(), new LocalStockService(), new FavoriteStockService(), new PttStockQueryService());
            
            DataContext = viewModel;
            viewModel.LoadData();

        }

        private void TimeInterval_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int oldValue = 0, newValue = 0;
            if (e.OldValue != null)
                oldValue = Convert.ToInt32(e.OldValue);
            if (e.NewValue != null)
                newValue = Convert.ToInt32(e.NewValue);

            viewModel.TimeIntervalChanged(oldValue, newValue);
        }

        private void SettingCommand_Click(object sender, RoutedEventArgs e)
        {
            if (SettingMenu.State == MenuState.Hidden)
                SettingMenu.State = MenuState.Visible;
            else
                SettingMenu.State = MenuState.Hidden;

        }
    }
}
