using System.Collections.Generic;
using BTCE_Trader.Api.Depth;
using BTCE_Trader.Api.Trade;

namespace BTCE_Trader.Api
{
    public interface IBtceTradeApi
    {
        void GetActiveOrders();
        Dictionary<BtcePairEnum, IMarketDepth> GetMarketDepths(List<BtcePairEnum> pairs);
        void CancelOrder(string orderId);
        void GetAccountInfo();
        void MakeTrade(ITradeRequest tradeRequest);
    }
}
