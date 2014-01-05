using System;

namespace BTCE_Trader.Api.MarketTrades
{
    class MarketTrade : IMarketTrade
    {
        public int TradeId { get; set; }
        public BtcePairEnum Pair { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}