using System.Collections.Generic;
using BtcE;

namespace BTCE_Trader.Core.Depth
{
    public interface IDepthAgent
    {
        void Start(int updateInterval, List<BtcePair> pairs);
        void Stop();
        event DepthAgent.DepthUpdatedDelegate DepthUpdated;
    }
}
