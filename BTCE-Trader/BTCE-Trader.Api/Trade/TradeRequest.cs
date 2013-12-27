namespace BTCE_Trader.Api.Trade
{
    public class TradeRequest : ITradeRequest
    {
        public BtcePairEnum Pair { get; set; }
        public TradeTypeEnum TradeType { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
    }
}