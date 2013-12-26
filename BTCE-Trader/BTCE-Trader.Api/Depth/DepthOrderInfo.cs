namespace BTCE_Trader.Api.Depth
{
    public class DepthOrderInfo : IDepthOrderInfo
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
