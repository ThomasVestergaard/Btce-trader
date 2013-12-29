using System;
using System.Collections.Generic;
using System.Threading;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Orders;

namespace BTCE_Trader.UI.UpdateAgents.Orders
{
    public class ActiveOrderAgent : IActiveOrderAgent
    {
        private readonly IBtceModels btceModels;

        public delegate void ActiveOrdersUpdatedDelegate(List<IOrder> activeOrders);
        public event ActiveOrdersUpdatedDelegate ActiveOrdersUpdated;

        private Thread workerThread { get; set; }
        private int updateInterval { get; set; }
        private IBtceTradeApi btceApi { get; set; }
        private bool isRunning { get; set; }
        private bool hasReceivedLastRequst { get; set; }
        private DateTime lastRequestTime { get; set; }

        public ActiveOrderAgent(IBtceTradeApi btceApi, IBtceModels btceModels)
        {
            lastRequestTime = DateTime.Now.AddSeconds(-10);
            hasReceivedLastRequst = true;
            btceModels.ActiveOrdersUpdated += btceModels_ActiveOrdersUpdated;
            this.btceModels = btceModels;
            this.btceApi = btceApi;
        }

        void btceModels_ActiveOrdersUpdated(object sender, EventArgs e)
        {
            hasReceivedLastRequst = true;
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
                var timeCheck = (DateTime.Now - lastRequestTime);
                if (hasReceivedLastRequst && timeCheck.TotalMilliseconds >= updateInterval)
                {
                    Console.WriteLine("Sending active orders request");
                    btceApi.GetActiveOrders();
                    lastRequestTime = DateTime.Now;
                    hasReceivedLastRequst = false;
                }

                Thread.Sleep(10);
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
