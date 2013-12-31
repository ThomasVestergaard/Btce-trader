using System.Collections.Generic;
using BTCE_Trader.Api;

namespace BTCE_Trader.UI.UpdateAgents.Depth
{
    public interface IDepthAgent
    {
        void Start(int updateInterval);
        void Stop();
    }
}
