using FastQueryStock.Model;
using FastQueryStock.Service;
using FastQueryStock.ViewModels;
using FastQueryStock.ViewModels.Controls;
using FastQueryStock.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.ViewModel
{
    /// <summary>
    /// The stock chart which is display multiple style chart
    /// </summary>
    public class StockMainChartViewModel : BaseViewModel
    {
        private string _title;
        private RealTimeStockItem _realtimeStockModel;

        #region UI Properties
        public string Title
        {
            get
            { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        #endregion

        /// <summary>
        /// Line Chart data
        /// </summary>
        public StockValueChartViewModel StockValueChart  { get; set; }

        public StockMainChartViewModel(IStockChartPanelView chartView, IStockQueryService queryService, RealTimeStockItem realtimeStock)
        {
            StockValueChart = new StockValueChartViewModel(chartView.StockValueChartView, queryService, realtimeStock);
            _realtimeStockModel = realtimeStock;
        }

        public async void Load()
        {
            Title = _realtimeStockModel.Name + " (" + _realtimeStockModel.Id + ")";

            await StockValueChart.LoadChart();
        }
    }
}
