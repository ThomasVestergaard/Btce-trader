using System;
using System.Threading.Tasks;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Trade;

namespace BTCE_Trader.Core.Trade
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
            makeTrade(tradeRequest);
        }

        async void makeTrade(ITradeRequest tradeRequest)
        {
            var tradeResult = btceTradeApi.MakeTrade(tradeRequest);
            
            if (tradeResult.IsSuccess)
                Console.WriteLine("Trade taken");
            else
                Console.WriteLine("Error making trade: " + tradeResult.ErrorMessage);
        }
    }
}