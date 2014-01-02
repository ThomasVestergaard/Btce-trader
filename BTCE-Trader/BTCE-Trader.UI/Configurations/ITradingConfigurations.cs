using System.Collections.Generic;
using BTCE_Trader.Api;

namespace BTCE_Trader.UI.Configurations
{
    public interface ITradingConfigurations
    {
        List<DefaultTradingSize> DefaultTradingSizes { get; }
        decimal CalculateAmount(BtcePairEnum pair, TradeTypeEnum tradeType, decimal CurrentHoldings);
    }
}
