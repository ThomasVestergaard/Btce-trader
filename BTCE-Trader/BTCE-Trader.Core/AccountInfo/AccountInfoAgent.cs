using System.Threading;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Info;

namespace BTCE_Trader.Core.AccountInfo
{
    public class AccountInfoAgent : IAccountInfoAgent
    {
        public delegate void AccountInfoUpdatedDelegate(IAccountInfo accountInfo);
        public event AccountInfoUpdatedDelegate AccountUpdated;

        private readonly IBtceTradeApi btceTradeApi;
        private Thread workerThread { get; set; }
        private int updateInterval { get; set; }
        private bool isRunning { get; set; }

        public AccountInfoAgent(IBtceTradeApi btceTradeApi)
        {
            this.btceTradeApi = btceTradeApi;
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
                var accountInfo = btceTradeApi.GetAccountInfo();
                if (accountInfo != null)
                    RaiseAccountUpdatedEvent(accountInfo);

                Thread.Sleep(updateInterval);
            }
        }

        public void Stop()
        {
            isRunning = false;
            workerThread.Join();
        }

        private void RaiseAccountUpdatedEvent(IAccountInfo accountInfo)
        {
            if (AccountUpdated != null)
                AccountUpdated(accountInfo);
        }

    }
}
