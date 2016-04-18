using FastQueryStock.Common;
using FastQueryStock.Model;
using FastQueryStock.Model.Charts;
using FastQueryStock.Service;
using FastQueryStock.Views;
using OxyPlot;
using OxyPlot.Axes;
using StockSDK.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastQueryStock.ViewModels.Controls
{
    public class StockValueChartViewModel : BaseViewModel
    {
        private IStockQueryService _queryService;

        private double _middleLineValue;
        private double _topLimitValue = Double.NaN;
        private double _downLimitValue = Double.NaN;
        private double _priceStep = Double.NaN;
        private string _title;
        private double _openTimeAxis;
        private double _closingTimeAxis;
        private IStockValueChartControlView _uiView;
        private double _volumeTopLimitValue;
        private double _volumeDownLimitValue;


        #region UI Properties

        public RealTimeStockItem StockValueContext { get; set; }
        /// <summary>
        /// Line Chart data
        /// </summary>
        public ObservableCollection<DataPoint> ChartDataCollection { get; set; }

        public ObservableCollection<DataPoint> VolumeDataCollection { get; set; }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Gets or sets the maximun value of the chrat to define the up limit
        /// </summary>
        public double TopLimitValue
        {
            get { return _topLimitValue; }
            set
            {
                _topLimitValue = value;
                NotifyPropertyChanged("TopLimitValue");
            }
        }
        /// <summary>
        /// Gets or sets the minimun value of the chrat to define the down limit
        /// </summary>
        public double DownLimitValue
        {
            get { return _downLimitValue; }
            set
            {
                _downLimitValue = value;
                NotifyPropertyChanged("DownLimitValue");
            }
        }

        public double VolumeTopLimitValue
        {
            get { return _volumeTopLimitValue; }
            set
            {
                _volumeTopLimitValue = value;
                NotifyPropertyChanged("VolumeTopLimitValue");
            }
        }
        public double VolumeDownLimitValue
        {
            get { return _volumeDownLimitValue; }
            set
            {
                _volumeDownLimitValue = value;
                NotifyPropertyChanged("VolumeDownLimitValue");
            }
        }

        /// <summary>
        /// 昨收價 中間分隔線
        /// </summary>
        public double MiddleLineValue
        {
            get { return _middleLineValue; }
            set
            {
                _middleLineValue = value;
                NotifyPropertyChanged("MiddleLineValue");
            }
        }
        /// <summary>
        /// Gets or sets opening of a stock exchange
        /// </summary>
        public double OpeningTimeAxis
        {
            get { return _openTimeAxis; }
            set
            {
                _openTimeAxis = value;
                NotifyPropertyChanged("OpeningTimeAxis");
            }
        }
        /// <summary>
        /// Gets or sets closing of a stock exchange
        /// </summary>
        public double ClosingTimeAxis
        {
            get { return _closingTimeAxis; }
            set
            {
                _closingTimeAxis = value;
                NotifyPropertyChanged("ClosingTimeAxis");
            }
        }

        public double PriceStep
        {
            get { return _priceStep; }
            set
            {
                _priceStep = value;
                NotifyPropertyChanged("PriceStep");
            }
        }



        #endregion


        public StockValueChartViewModel(IStockValueChartControlView view, IStockQueryService queryService, RealTimeStockItem realtimeStock)
        {
            _uiView = view;
            _queryService = queryService;

            ChartDataCollection = new ObservableCollection<DataPoint>();
            VolumeDataCollection = new ObservableCollection<DataPoint>();
            StockValueContext = realtimeStock;
        }

        public async Task LoadChart()
        {
            var stockInfo = new StockInfoItem
            {
                Id = StockValueContext.Id,
                MarketType = StockValueContext.ExchangeTypeKey,
                Name = StockValueContext.Name
            };
            StockChartModel chartModel = await _queryService.GetRealTimeTradeChartAsync(stockInfo);

            // Set the start and end value to y axis
            SetOpenCloseTimeToCharAxis(chartModel);

            // Set the top and bottom value for the chart
            SetUpDownLimitLineToChart(chartModel.StockMessage);

            // set the value of top and bottom volume for the line bar chart 
            SetVolumeUpDownLimitToChart(chartModel.ChartData);

            // Draw the middle, up and down extra line for the chart
            DrawExtraLines(chartModel.StockMessage);

            // 設定股價座標顯示的區間次數
            SetPriceTickInterval(Convert.ToDouble(chartModel.StockMessage.LimitUp), Convert.ToDouble(chartModel.StockMessage.LimitDown), 5);

            // Add stock point to chart
            foreach (var point in chartModel.ChartData)
            {
                DateTime xDateTime = DateTimeUtils.UnixTimeStampToDateTime(Convert.ToDouble(point.Timestemp));
                var axisPoint = DateTimeAxis.CreateDataPoint(xDateTime, Convert.ToDouble(point.Value));
                var volumePoint = DateTimeAxis.CreateDataPoint(xDateTime, Convert.ToDouble(point.ExchangeVolume));
                ChartDataCollection.Add(axisPoint);
                VolumeDataCollection.Add(volumePoint);
            }
        }

        private void SetPriceTickInterval(double limitUp, double limitDown, int interval)
        {
            double step = (limitUp - limitDown) / 5;
            PriceStep = step;
        }

        private void SetUpDownLimitLineToChart(RealTimeStockModel stockMessage)
        {
            // Set the top and bottom value for the chart
            // *It Must set value before drawing the chart
            MiddleLineValue = Convert.ToDouble(stockMessage.YesterdayPrice);
            if (stockMessage.LimitUp != null && stockMessage.LimitDown != null)
            {
                TopLimitValue = Convert.ToDouble(stockMessage.LimitUp);
                DownLimitValue = Convert.ToDouble(stockMessage.LimitDown);
            }
        }

        private void SetVolumeUpDownLimitToChart(List<ChartPointModel> dataPoints)
        {
            VolumeTopLimitValue =  dataPoints.Max(x => Convert.ToDouble(x.ExchangeVolume));
            VolumeDownLimitValue = dataPoints.Min(x => Convert.ToDouble(x.ExchangeVolume));
        }

        private void SetOpenCloseTimeToCharAxis(StockChartModel chartModel)
        {
            // set the start and end y axis
            if (chartModel.ChartData.Count > 0)
            {
                DateTime op = DateTimeUtils.UnixTimeStampToDateTime(Convert.ToDouble(chartModel.ChartData[0].Timestemp));
                DateTime clo = new DateTime(op.Year, op.Month, op.Day, 13, 30, 0);
                OpeningTimeAxis = DateTimeAxis.ToDouble(op);
                ClosingTimeAxis = DateTimeAxis.ToDouble(clo);
            }
        }

        private void DrawExtraLines(RealTimeStockModel stockMessage)
        {
            List<ExtraLine> extraLines = new List<ExtraLine>
            {
                new ExtraLine { Value = Convert.ToDouble(stockMessage.LimitUp) },
                new ExtraLine { Value = Convert.ToDouble(stockMessage.YesterdayPrice) },
                new ExtraLine { Value = Convert.ToDouble(stockMessage.LimitDown) }
            };
            _uiView.DrawExtraLine(extraLines);

        }
    }
}
