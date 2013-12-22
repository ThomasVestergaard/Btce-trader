using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Api.Orders
{
    public interface IOrder
    {
        BtcePairEnum Pair { get; set; }
        TradeTypeEnum Type { get; set; }
        decimal Amount { get; set; }
        decimal Rate { get; set; }
        DateTime CreateDate { get; set; }
        int Status { get; set; }

    }
}
