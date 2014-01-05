using System;

namespace BTCE_Trader.Api.MarketTrades
{
    public interface IMarketTrade
    {
        int TradeId { get; set; }
        BtcePairEnum Pair { get; set; }
        decimal Amount { get; set; }
        decimal Rate { get; set; }
        DateTime Timestamp { get; set; }
    }
}
