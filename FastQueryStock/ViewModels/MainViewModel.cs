using FastQueryStock.Common;
using FastQueryStock.Service;
using FastQueryStock.ViewModels.Controls;
using MaterialMenu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastQueryStock.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private IStockQueryService _queryService;
        private string _stockNumber;
        private int _timeInterval;
        private bool _isAutoRefresh;
        private string _backgroundColor = "#FFFFFFFF";
        private IFavoriteStockService _favoriteStockService;
        private ILocalStockService _localStockService;
        private RealTimeStockItem _selectedStockItem;
        private bool _enableDeleteButton;
        private bool _isSafeMode;
        private bool _isUpdateDbLoading;
        private bool _enableUpdateDbButton = true;

        #region UI Property


        public StockListControlViewModel StockListViewModel { get; set; }

        /// <summary>
        /// The main windows background color
        /// </summary>
        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                NotifyPropertyChanged("BackgroundColor");
            }
        }

        /// <summary>
        /// Input the number to add the stock
        /// </summary>
        public string StockNumber
        {
            get { return _stockNumber; }
            set
            {
                _stockNumber = value;
                NotifyPropertyChanged("StockNumber");
            }
        }

        public int TimeInterval
        {
            get { return _timeInterval; }
            set
            {
                _timeInterval = value;
                NotifyPropertyChanged("TimeInterval");
            }
        }
        /// <summary>
        /// Is auto refresh all of the stock data
        /// </summary>
        public bool IsAutoRefresh
        {
            get { return _isAutoRefresh; }
            set
            {

                _isAutoRefresh = value;
                NotifyPropertyChanged("IsAutoRefresh");
            }
        }
        public bool IsSafeMode
        {
            get { return _isSafeMode; }
            set
            {
                _isSafeMode = value;
                NotifyPropertyChanged("IsSafeMode");
            }
        }

        public RealTimeStockItem SelectedStockItem
        {
            get { return _selectedStockItem; }
            set
            {
                _selectedStockItem = value;
                EnableDeleteButton = _selectedStockItem != null;
            }
        }
        public bool EnableDeleteButton
        {
            get { return _enableDeleteButton; }
            set
            {
                _enableDeleteButton = value;
                NotifyPropertyChanged("EnableDeleteButton");
            }
        }

        public bool IsUpdateDbLoading
        {
            get { return _isUpdateDbLoading; }
            set
            {
                _isUpdateDbLoading = value;
                NotifyPropertyChanged("IsUpdateDbLoading");
            }
        }

        public bool EnableUpdateDbButton
        {
            get { return _enableUpdateDbButton; }
            set
            {
                _enableUpdateDbButton = value;
                NotifyPropertyChanged("EnableUpdateDbButton");
            }
        }


        #endregion

        #region Command
        public ICommand AddStockCommand { get; set; }
        public ICommand DeleteStockCommand { get; set; }
        public ICommand AutoRefreshCheckedCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand SafeModeCheckedCommand { get; set; }
        public ICommand UpdateAllStockCommand { get; set; }

        public ICommand StockItemDoubleClickCommand { get; set; }
        #endregion

        public MainViewModel(IStockQueryService queryService, ILocalStockService localStockService, IFavoriteStockService favoriteService)
        {
            _queryService = queryService;
            _localStockService = localStockService;
            _favoriteStockService = favoriteService;
            StockListViewModel = new StockListControlViewModel(queryService, favoriteService);            
            AddStockCommand = new DelegateCommand(AddStock_Click);
            DeleteStockCommand = new DelegateCommand(DeleteStock_Click);
            AutoRefreshCheckedCommand = new DelegateCommand(AutoRefresh_Checked);
            SafeModeCheckedCommand = new DelegateCommand(SafeMode_Checked);
            UpdateAllStockCommand = new DelegateCommand(UpdateAllStock_Click);
            StockItemDoubleClickCommand = new DelegateCommand<RealTimeStockItem>(StockItem_DoubleClick);

            TimeInterval = 10;
            IsAutoRefresh = true;
#if DEBUG
            IsSafeMode = true;
#endif
            _localStockService.InitializeData();
        }

     

        /// <summary>
        /// if comfirm to the safe mode, stock panel would change the background to Transparency
        /// </summary>
        private void SafeMode_Checked()
        {
            if (!IsSafeMode)
                BackgroundColor = "#FFFFFFFF";
            else
                BackgroundColor = "#01000000";
        }


        /// <summary>
        /// Start to load data
        /// </summary>
        public async Task LoadData()
        {           
            StringBuilder str = new StringBuilder();
            try
            {
                var allFavorite = _favoriteStockService.GetAll();
              
                if (allFavorite.Count > 0)
                {
                    allFavorite = allFavorite.OrderBy(x => x.Order).ToList();
                    
                    List<RealTimeStockItem> realTimeStockList = await _queryService.GetMultipleRealTimeStockAsync(allFavorite);
                    StockListViewModel.AddStock(realTimeStockList);
                }
            }
            catch (Exception e)
            {
                str.AppendLine(e.Message);
            }

            if (str.Length != 0) 
                Dialog.ShowError(str.ToString());

        }
      

#region Event Handler
        private async void AddStock_Click()
        {
            
            StockInfoItem stockItem = null;
            try
            {
                string localStockNum = StockNumber;

                stockItem = _localStockService.Get(localStockNum);
                var stockData = await _queryService.GetMultipleRealTimeStockAsync(new List<StockInfoItem> { stockItem });
                // check if the stock is exist in the list
                if (stockData.Count > 0)
                    StockListViewModel.AddStock(stockData);

                stockItem.Order = _favoriteStockService.GetLastOrder(0);
                _favoriteStockService.Add(stockItem);
            }
            catch(FavoriteStockExistException e)
            {
                Dialog.ShowError(string.Format("目前 {0}({1}) 已經存在於清單內!", stockItem.Name, stockItem.Id));
            }
            catch (Exception e)
            {
                Dialog.ShowError(e.Message + 
                    (e.InnerException == null ? string.Empty :  " (detail : " +  e.InnerException + ")"));
            }
            StockNumber = string.Empty;
        }

        private void DeleteStock_Click()
        {
            try
            {
                if (SelectedStockItem != null)
                {
                    _favoriteStockService.Delete(SelectedStockItem.Id);
                    StockListViewModel.RemoveStock(SelectedStockItem);
                }
            }
            catch (Exception e)
            {
                Dialog.ShowError(e.Message);
            }
        }


        /// <summary>
        /// When change the time interval value, the method would called by changed value event of time interval UpDownNumeric
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void TimeIntervalChanged(int oldValue, int newValue)
        {
            StockListViewModel.UpdateInterval = newValue;
        }

        public void AutoRefresh_Checked()
        {
            if (IsAutoRefresh)
                StockListViewModel.StartPollingUpdate(TimeInterval);
            else
                StockListViewModel.StopUpdate();
        }

        /// <summary>
        /// Update all of the stock data from TWSE web site, and store in the database
        /// </summary>
        private async void UpdateAllStock_Click()
        {
            try
            {
                await Task.Run(async () =>
                {
                    IsUpdateDbLoading = true;
                    EnableUpdateDbButton = false;

                    List<StockInfoItem> allStockItems = await _queryService.GetAllStockInfoAsync();
                    _localStockService.Add(allStockItems);

                    IsUpdateDbLoading = false;
                    EnableUpdateDbButton = true;
                });
            }
            catch (Exception e)
            {
                IsUpdateDbLoading = false;
                EnableUpdateDbButton = true;
                Dialog.ShowError(e.Message);
            }
        }

        private void StockItem_DoubleClick(RealTimeStockItem realTimeStock)
        {
            //StockInfoItem stockItemInfo = new StockInfoItem
            //{
            //    Id = realTimeStock.Id,
            //    MarketType = realTimeStock.ExchangeTypeKey,
            //    Name = realTimeStock.Name                
            //};

            Dialog.ShowStockChartDialog(_queryService, realTimeStock);
        }
        #endregion


    }
}
