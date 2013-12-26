using System.Collections.Generic;

namespace BTCE_Trader.Api.Depth
{
    public interface IMarketDepth
    {
        List<IDepthOrderInfo> Bids { get; set; }
        List<IDepthOrderInfo> Asks { get; set; }
    }
}
