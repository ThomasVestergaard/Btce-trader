using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BtcE;

namespace BTCE_Trader.Core.Orders
{
    public class ActiveOrderAgent : IActiveOrderAgent
    {
        public delegate void ActiveOrdersUpdatedDelegate(List<Order> activeOrders);
        public event ActiveOrdersUpdatedDelegate ActiveOrdersUpdated;

        private Thread workerThread { get; set; }
        private int updateInterval { get; set; }
        private BtceApi btceApi { get; set; }
        private bool isRunning { get; set; }

        public ActiveOrderAgent(BtceApi btceApi)
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
            return;
            while (isRunning)
            {
                var aorders = btceApi.GetActiveOrderList();
                
                /*var activeOrder = btceApi.GetOrderList(null, null, null, null, null, null, null, null, true);
                var activeOrderList = activeOrder.List.Select(a => a.Value).ToList();
                RaiseActiveUpdatedEvent(activeOrderList);
                */
                Thread.Sleep(updateInterval);
            }
        }

        public void Stop()
        {
            isRunning = false;
            workerThread.Join();
        }

        private void RaiseActiveUpdatedEvent(List<Order> activeOrders)
        {
            if (ActiveOrdersUpdated != null)
                ActiveOrdersUpdated(activeOrders);
        }
    }
}
