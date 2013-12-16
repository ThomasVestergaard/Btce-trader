using System.Collections.Generic;
using BtcE;

namespace BTCE_Trader.Core.Depth
{
    public interface IDepthUpdater
    {
        void Start(int updateInterval, List<BtcePair> pairs);
        void Stop();
        event DepthUpdater.DepthUpdatedDelegate DepthUpdated;
    }
}
