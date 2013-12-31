using System;
using System.Collections.Generic;
using System.Threading;
using BTCE_Trader.Api;

namespace BTCE_Trader.UI.UpdateAgents.Depth
{
    public class DepthAgent : IDepthAgent
    {
        private readonly IBtceTradeApi btceTradeApi;
        private readonly IBtceModels btceModels;

        private Thread workerThread { get; set; }
        private int updateInterval { get; set; }
        private bool isRunning { get; set; }
        private bool hasReceivedLastRequst { get; set; }
        private DateTime lastRequestTime { get; set; }

        public DepthAgent(IBtceTradeApi btceTradeApi, IBtceModels btceModels)
        {
            lastRequestTime = DateTime.Now.AddSeconds(-10);
            hasReceivedLastRequst = true;
            this.btceTradeApi = btceTradeApi;
            this.btceModels = btceModels;
            this.btceModels.DepthUpdated += btceModels_DepthUpdated;
        }

        void btceModels_DepthUpdated(object sender, EventArgs e)
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
                    Console.WriteLine("Sending update depth request");
                    btceTradeApi.UpdateDepth();
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


    }
}
