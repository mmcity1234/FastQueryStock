using FastQueryStock.Event;
using FastQueryStock.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastQueryStock.ViewModels.Controls
{
    public class StockListControlViewModel
    {
        private IFavoriteStockService _favoriteStockService;
        private IStockQueryService _stockQueryService;

        public int UpdateInterval { get; set; }
        public ObservableCollection<RealTimeStockItem> StockList { get; set; }

        public StockListControlViewModel(IStockQueryService stockQueryService, IFavoriteStockService favoriteService)
        {
            StockList = new ObservableCollection<RealTimeStockItem>();
            _stockQueryService = stockQueryService;
            _favoriteStockService = favoriteService;
        }

        /// <summary>
        /// Add the stock item to the list box
        /// </summary>
        /// <param name="list"></param>
        public void AddStock(List<RealTimeStockItem> list)
        {
            foreach (var item in list)
            {
                AddStock(item);
            }
        }
        public void AddStock(RealTimeStockItem item)
        {
            // check if the stock is exist in the list
            if (StockList.FirstOrDefault(x => x.Id == item.Id) == null)
            {
                StockList.Add(item);
            }
        }
        public void RemoveStock(RealTimeStockItem item)
        {
            StockList.Remove(item);
        }


        #region Polling
        private CancellationTokenSource cancelToken;
        /// <summary>
        /// Seconds
        /// </summary>

        public void StartPollingUpdate(int interval)
        {
            UpdateInterval = interval;
            cancelToken = new CancellationTokenSource();

            // 存放目前執行續的CancelTokenSource
            var localCancelToken = cancelToken;
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        await Task.Delay(UpdateInterval * 1000);
                        if (localCancelToken.IsCancellationRequested)
                            break;

                        var allFavorite = _favoriteStockService.GetAll();
                        if (allFavorite.Count > 0)
                        {
                            List<RealTimeStockItem> realTimeStockList = await _stockQueryService.GetMultipleRealTimeStockAsync(allFavorite);

                            foreach (var viewItem in StockList)
                            {
                                var newStockValue = realTimeStockList.FirstOrDefault(x => x.Id == viewItem.Id);
                                if (newStockValue != null)
                                {
                                    viewItem.Update(newStockValue);
                                    NotificationCenter.Instance.NotifyEvent(EventType.RealTimeStockValue, newStockValue);
                                }
#if DEBUG 
                                //else //for test
                                //    Dialog.ShowWarning("找不到符合的資料進行更新，股票名稱[" + viewItem.Name + "]");
#endif
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Dialog.ShowError(e.Message);
                    }

                }
            }, localCancelToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
        public void StopUpdate()
        {
            if (cancelToken != null)
                cancelToken.Cancel();
        }
        #endregion



    }
}
