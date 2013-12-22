using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Api.Depth
{
    public interface IDepthOrderInfo
    {
        decimal Price { get; set; }
        decimal Amount { get; set; }
    }
}
