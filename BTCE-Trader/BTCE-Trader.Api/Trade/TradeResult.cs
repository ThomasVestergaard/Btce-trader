namespace BTCE_Trader.Api.Trade
{
    public class TradeResult : ITradeResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}