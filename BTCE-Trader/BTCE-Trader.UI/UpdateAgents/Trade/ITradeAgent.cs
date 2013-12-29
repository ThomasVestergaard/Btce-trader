using BTCE_Trader.Api.Trade;

namespace BTCE_Trader.UI.UpdateAgents.Trade
{
    public interface ITradeAgent
    {
        void MakeTrade(ITradeRequest tradeRequest);
    }
}
