using System.Collections.Generic;
using BTCE_Trader.Api.Depth;
using BTCE_Trader.Api.Info;
using BTCE_Trader.Api.Orders;
using BTCE_Trader.Api.Trade;

namespace BTCE_Trader.Api
{
    public interface IBtceTradeApi
    {
        List<IOrder> GetActiveOrders();
        Dictionary<BtcePairEnum, IMarketDepth> GetMarketDepths(List<BtcePairEnum> pairs);
        void CancelOrder(string orderId);
        IAccountInfo GetAccountInfo();
        ITradeResult MakeTrade(ITradeRequest tradeRequest);
    }
}
