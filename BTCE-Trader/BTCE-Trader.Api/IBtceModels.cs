using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BTCE_Trader.Api.Info;
using BTCE_Trader.Api.Orders;
using BTCE_Trader.Api.RequestQueue;
using Disruptor;

namespace BTCE_Trader.Api
{
    public interface IBtceModels : IEventHandler<OutputQueueItem>
    {
        event EventHandler AccountInfoUpdated;
        event EventHandler ActiveOrdersUpdated;
        ObservableCollection<ApiMessage> ApiMessages { get; set; }
        List<IOrder> ActiveOrders { get; }
        IAccountInfo AccountInfo { get; }
    }

    public class ApiMessage
    {
        public string MessageType { get; set; }
        public string Message { get; set; }
    }

}
