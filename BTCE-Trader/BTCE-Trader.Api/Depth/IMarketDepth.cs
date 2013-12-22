using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Api.Depth
{
    public interface IMarketDepth
    {
        List<IDepthOrderInfo> Bids { get; set; }
        List<IDepthOrderInfo> Asks { get; set; }
    }
}
