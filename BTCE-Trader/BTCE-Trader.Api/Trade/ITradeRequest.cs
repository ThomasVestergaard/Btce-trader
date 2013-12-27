namespace BTCE_Trader.Api.Trade
{
    public interface ITradeRequest
    {
        BtcePairEnum Pair { get; set; }
        TradeTypeEnum TradeType { get; set; }
        decimal Amount { get; set; }
        decimal Rate { get; set; }
    }
}
