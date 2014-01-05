using System;
using System.Threading;
using BTCE_Trader.Api;

namespace BTCE_Trader.UI.UpdateAgents.MarketTrades
{
    class MarketTradesAgent : IMarketTradesAgent
    {
        private readonly IBtceTradeApi btceTradeApi;
        private readonly IBtceModels btceModels;

        private Thread workerThread { get; set; }
        private int updateInterval { get; set; }
        private bool isRunning { get; set; }
        private bool hasReceivedLastRequst { get; set; }
        private DateTime lastRequestTime { get; set; }

        public MarketTradesAgent(IBtceTradeApi btceTradeApi, IBtceModels btceModels)
        {
            lastRequestTime = DateTime.Now.AddSeconds(-10);
            hasReceivedLastRequst = true;
            this.btceTradeApi = btceTradeApi;
            this.btceModels = btceModels;
            this.btceModels.MarketTradesUpdated += btceModels_MarketTradesUpdated;
        }

        void btceModels_MarketTradesUpdated(object sender, EventArgs e)
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
                    Console.WriteLine("Sending market trades request");
                    btceTradeApi.UpdateMarketTrades();
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