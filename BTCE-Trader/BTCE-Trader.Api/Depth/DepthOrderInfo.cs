namespace BTCE_Trader.Api.Depth
{
    public class DepthOrderInfo : IDepthOrderInfo
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal AccumulatedAmount { get; set; }
        public string ActiveOrder { get; set; }

    }
}
