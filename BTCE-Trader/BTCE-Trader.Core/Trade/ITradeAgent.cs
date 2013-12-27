using BTCE_Trader.Api.Trade;

namespace BTCE_Trader.Core.Trade
{
    public interface ITradeAgent
    {
        void MakeTrade(ITradeRequest tradeRequest);
    }
}
