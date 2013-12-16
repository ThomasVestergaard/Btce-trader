using System.Collections.Generic;
using System.Threading;
using BtcE;
using System;

namespace BTCE_Trader.Core.Depth
{
    public class DepthUpdater : IDepthUpdater
    {
        public delegate void DepthUpdatedDelegate(Dictionary<BtcePair, BtcE.Depth> pairDepthPairs);
        public event DepthUpdatedDelegate DepthUpdated;

        private Thread workerThread { get; set; }
        private int updateInterval { get; set; }
        private List<BtcePair> pairs { get; set; }
        private bool isRunning { get; set; }

        public void Start(int updateInterval, List<BtcePair> pairs)
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
                var depth = BtceApiV3.GetDepth(pairs.ToArray());
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

        private void RaiseDepthUpdatedEvent(Dictionary<BtcePair, BtcE.Depth> pairDepthPairs)
        {
            if (DepthUpdated != null)
                DepthUpdated(pairDepthPairs);
        }

    }
}
