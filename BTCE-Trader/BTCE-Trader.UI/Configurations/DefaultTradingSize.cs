using BTCE_Trader.Api;

namespace BTCE_Trader.UI.Configurations
{
    public class DefaultTradingSize
    {
        public BtcePairEnum Pair { get; set; }
        public TradeTypeEnum TradeType { get; set; }
        public DefaultTradingSizeType TradeSizeType { get; set; }
        public decimal FixedAmount { get; set; }
        public decimal PercentageOfCurrentHoldings { get; set; }
    }

    public enum DefaultTradingSizeType
    {
        FixedAmount,
        PercentageOfCurrentHoldings
    }
}
