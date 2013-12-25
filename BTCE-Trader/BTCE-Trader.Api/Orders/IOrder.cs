using System;

namespace BTCE_Trader.Api.Orders
{
    public interface IOrder
    {
        string Id { get; set; }
        BtcePairEnum Pair { get; set; }
        TradeTypeEnum Type { get; set; }
        decimal Amount { get; set; }
        decimal Rate { get; set; }
        DateTime CreateDate { get; set; }
        int Status { get; set; }

    }
}
