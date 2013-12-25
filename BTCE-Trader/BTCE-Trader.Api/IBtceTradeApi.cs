using System.Collections.Generic;
using BTCE_Trader.Api.Depth;
using BTCE_Trader.Api.Orders;

namespace BTCE_Trader.Api
{
    public interface IBtceTradeApi
    {
        List<IOrder> GetActiveOrders();
        Dictionary<BtcePairEnum, MarketDepth> GetMarketDepths(List<BtcePairEnum> pairs);
    }
}
