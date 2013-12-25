using System.Collections.Generic;
using System.Threading;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Depth;

namespace BTCE_Trader.Core.Depth
{
    public class DepthAgent : IDepthAgent
    {
        private readonly IBtceTradeApi btceTradeApi;

        public delegate void DepthUpdatedDelegate(Dictionary<BtcePairEnum, MarketDepth> pairDepthPairs);
        public event DepthUpdatedDelegate DepthUpdated;

        private Thread workerThread { get; set; }
        private int updateInterval { get; set; }
        private List<BtcePairEnum> pairs { get; set; }
        private bool isRunning { get; set; }

        public DepthAgent(IBtceTradeApi btceTradeApi)
        {
            this.btceTradeApi = btceTradeApi;
        }

        public void Start(int updateInterval, List<BtcePairEnum> pairs)
        {
            this.updateInterval = updateInterval;
            this.pairs = pairs;
            isRunning = true;
            workerThread = new Thread(DoWork);
            workerThread.Start();
        }

        private void DoWork()
        {
            while (isRunning)
            {
                var depth = btceTradeApi.GetMarketDepths(pairs);
                if (depth != null)
                    RaiseDepthUpdatedEvent(depth);

                Thread.Sleep(updateInterval);
            }
        }

        public void Stop()
        {
            isRunning = false;
            workerThread.Join();
        }

        private void RaiseDepthUpdatedEvent(Dictionary<BtcePairEnum, MarketDepth> pairDepthPairs)
        {
            if (DepthUpdated != null)
                DepthUpdated(pairDepthPairs);
        }

    }
}
