using System.Collections.Generic;
using System.Threading;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Orders;

namespace BTCE_Trader.Core.Orders
{
    public class ActiveOrderAgent : IActiveOrderAgent
    {
        public delegate void ActiveOrdersUpdatedDelegate(List<IOrder> activeOrders);
        public event ActiveOrdersUpdatedDelegate ActiveOrdersUpdated;

        private Thread workerThread { get; set; }
        private int updateInterval { get; set; }
        private IBtceTradeApi btceApi { get; set; }
        private bool isRunning { get; set; }

        public ActiveOrderAgent(IBtceTradeApi btceApi)
        {
            this.btceApi = btceApi;
        }

        public void Start(int updateInterval)
        {
            this.updateInterval = updateInterval;
            isRunning = true;
            workerThread = new Thread(DoWork);
            workerThread.Start();
        }

        private void DoWork()
        {
            
            while (isRunning)
            {
                var activeOrderList = btceApi.GetActiveOrders();
                RaiseActiveUpdatedEvent(activeOrderList);
                
                Thread.Sleep(updateInterval);
            }
        }

        public void Stop()
        {
            isRunning = false;
            workerThread.Join();
        }

        private void RaiseActiveUpdatedEvent(List<IOrder> activeOrders)
        {
            if (ActiveOrdersUpdated != null)
                ActiveOrdersUpdated(activeOrders);
        }
    }
}
