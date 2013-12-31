namespace BTCE_Trader.Api.Depth
{
    public interface IDepthOrderInfo
    {
        decimal Price { get; set; }
        decimal Amount { get; set; }
        decimal AccumulatedAmount { get; set; }
        string ActiveOrder { get; set; }
    }
}
