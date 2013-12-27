namespace BTCE_Trader.Api.Trade
{
    public interface ITradeResult
    {
        bool IsSuccess { get; set; }
        string ErrorMessage { get; set; }
    }
}
