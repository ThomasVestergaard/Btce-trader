using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Api.Orders
{
    public class Order : IOrder
    {
        public BtcePairEnum Pair { get; set; }
        public TradeTypeEnum Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }

    }
}
