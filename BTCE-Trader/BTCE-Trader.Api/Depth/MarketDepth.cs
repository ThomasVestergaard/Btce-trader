using System.Collections.Generic;

namespace BTCE_Trader.Api.Depth
{
    public class MarketDepth : IMarketDepth
    {
        public List<IDepthOrderInfo> Bids { get; set; }
        public List<IDepthOrderInfo> Asks { get; set; }

        public MarketDepth()
        {
            Bids = new List<IDepthOrderInfo>();
            Asks = new List<IDepthOrderInfo>();
        }
    }
}
