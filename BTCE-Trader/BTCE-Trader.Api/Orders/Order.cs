using System;

namespace BTCE_Trader.Api.Orders
{
    public class Order : IOrder
    {
        public string Id { get; set; }
        public BtcePairEnum Pair { get; set; }
        public TradeTypeEnum Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }

    }
}
