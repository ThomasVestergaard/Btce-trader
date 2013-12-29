using BTCE_Trader.Api;
using BTCE_Trader.Api.Trade;

namespace BTCE_Trader.UI.UpdateAgents.Trade
{
    public class TradeAgent : ITradeAgent
    {
        private readonly IBtceTradeApi btceTradeApi;

        public TradeAgent(IBtceTradeApi btceTradeApi)
        {
            this.btceTradeApi = btceTradeApi;
        }


        public void MakeTrade(ITradeRequest tradeRequest)
        {
            btceTradeApi.MakeTrade(tradeRequest);
        }

        
    }
}