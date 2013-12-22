using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Api.Depth
{
    public class MarketDepth
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
