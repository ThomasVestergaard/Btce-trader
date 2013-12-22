using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Api.Depth
{
    public class DepthOrderInfo : IDepthOrderInfo
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
