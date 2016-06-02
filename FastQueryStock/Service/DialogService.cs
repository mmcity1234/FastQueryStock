using FastQueryStock.Model;
using FastQueryStock.ViewModel;
using FastQueryStock.ViewModels;
using FastQueryStock.ViewModels.Controls;
using FastQueryStock.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FastQueryStock.Service
{
    public class Dialog
    {
        public static bool ShowWarning(string message)
        {
            return ShowWarning(message, "警告");
        }

        public static bool ShowWarning(string message, string caption)
        {
            var result = System.Windows.MessageBox.Show(message,
                caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            return result == MessageBoxResult.Yes;
        }

        public static bool ShowError(string message)
        {
            var result = System.Windows.MessageBox.Show(message,
                "錯誤",
                MessageBoxButton.OK,
                MessageBoxImage.Error,
                MessageBoxResult.OK);

            return result == MessageBoxResult.OK;
        }

        /// <summary>
        /// Show the chart windows from stock 
        /// </summary>
        public static void ShowStockChartDialog(IStockQueryService queryService, RealTimeStockItem stockItem)
        {
            StockMainChartPanel chartPanel = new StockMainChartPanel();
            StockMainChartViewModel chartMainViewModel = new StockMainChartViewModel(chartPanel, queryService, stockItem);
            chartPanel.DataContext = chartMainViewModel;
            chartPanel.Show();
            chartMainViewModel.Load();
        }

        public static void ShowBuySellPriceDialog(RealTimeStockItem stockItem)
        {
            BuySellPricePanel panel = new BuySellPricePanel();
            BuySellPricePanelViewModel viewModel = new BuySellPricePanelViewModel(stockItem);
            panel.DataContext = viewModel;
            panel.Show();
            viewModel.Load();
        }

    }
}
