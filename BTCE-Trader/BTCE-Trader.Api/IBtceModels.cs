using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BTCE_Trader.Api.Depth;
using BTCE_Trader.Api.Info;
using BTCE_Trader.Api.MarketTrades;
using BTCE_Trader.Api.Orders;
using BTCE_Trader.Api.RequestQueue;
using Disruptor;

namespace BTCE_Trader.Api
{
    public interface IBtceModels : IEventHandler<OutputQueueItem>
    {
        event EventHandler AccountInfoUpdated;
        event EventHandler ActiveOrdersUpdated;
        event EventHandler DepthUpdated;
        event EventHandler MarketTradesUpdated;
        ObservableCollection<ApiMessage> ApiMessages { get; set; }
        Dictionary<BtcePairEnum, IMarketDepth> MarketDepths { get; }
        List<IOrder> ActiveOrders { get; }
        IAccountInfo AccountInfo { get; }
        Dictionary<BtcePairEnum, List<IMarketTrade>> PairTrades { get; set; }
    }

    public class ApiMessage
    {
        public string MessageType { get; set; }
        public string Message { get; set; }
    }

}
