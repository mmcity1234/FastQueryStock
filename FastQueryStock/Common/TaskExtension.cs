using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastQueryStock.Common
{
    public static class TaskExtension
    {
        public static CancellationTokenSource Polling(int interval, Action pollingAction)
        {
            CancellationTokenSource localCancelToken = new CancellationTokenSource();

            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        await Task.Delay(interval * 1000);
                        if (localCancelToken.IsCancellationRequested)
                            break;
                        // polling function
                        pollingAction();
                    }
                    catch (Exception e)
                    {
                       
                    }

                }
            }, localCancelToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            return localCancelToken;
        }
    }
}
