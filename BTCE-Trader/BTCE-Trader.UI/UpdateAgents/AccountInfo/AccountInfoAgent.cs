using System;
using System.Threading;
using BTCE_Trader.Api;

namespace BTCE_Trader.UI.UpdateAgents.AccountInfo
{
    public class AccountInfoAgent : IAccountInfoAgent
    {
        private readonly IBtceTradeApi btceTradeApi;
        private readonly IBtceModels btceModels;
        private Thread workerThread { get; set; }
        private int updateInterval { get; set; }
        private bool isRunning { get; set; }
        private bool hasReceivedLastRequst { get; set; }
        private DateTime lastRequestTime { get; set; }

        public AccountInfoAgent(IBtceTradeApi btceTradeApi, IBtceModels btceModels)
        {
            this.btceTradeApi = btceTradeApi;
            this.btceModels = btceModels;
        }

        public void Start(int updateInterval)
        {
            btceModels.AccountInfoUpdated += btceModels_AccountInfoUpdated;
            lastRequestTime = DateTime.Now.AddSeconds(-10);
            this.updateInterval = updateInterval;
            isRunning = true;
            hasReceivedLastRequst = true;
            workerThread = new Thread(DoWork);
            workerThread.Start();
        }

        void btceModels_AccountInfoUpdated(object sender, EventArgs e)
        {
            hasReceivedLastRequst = true;
        }

        private void DoWork()
        {
            while (isRunning)
            {
                var timeCheck = (DateTime.Now - lastRequestTime);
                if (hasReceivedLastRequst && timeCheck.TotalMilliseconds >= updateInterval)
                {
                    btceTradeApi.GetAccountInfo();
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
