using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BTCE_Trader.Api.Annotations;
using BTCE_Trader.Api.Info;
using BTCE_Trader.Api.Orders;
using BTCE_Trader.Api.RequestQueue;
using BTCE_Trader.Api.Time;
using Newtonsoft.Json.Linq;

namespace BTCE_Trader.Api
{
    public class BtceModels : IBtceModels, INotifyPropertyChanged
    {
        public event EventHandler AccountInfoUpdated;
        public event EventHandler ActiveOrdersUpdated;

        private IAccountInfo accountInfo;
        public IAccountInfo AccountInfo
        {
            get { return accountInfo; }
        }
        
        public List<IOrder> ActiveOrders { get; set; }

        public ObservableCollection<ApiMessage> ApiMessages { get; set; }

        public BtceModels()
        {
            accountInfo = new AccountInfo();
            ApiMessages = new ObservableCollection<ApiMessage>();
            ActiveOrders = new List<IOrder>();
        }

        public void OnNext(OutputQueueItem data, long sequence, bool endOfBatch)
        {
            switch (data.InputQueueItem.MethodName)
            {
                case BtceTradeApi.BtceCommandAccountInfo :
                    HandleAccountInfoCallBack(data.WebResult);
                    break;

                case BtceTradeApi.BtceCommandActiveOrders:
                    HandleActiveOrdersCallBack(data.WebResult);
                    break;

                default:
                    break;
            }

            Console.WriteLine("Received output message from method: " + data.InputQueueItem.MethodName);
        }

        private void LogApiMessage(string messageType, string message)
        {
            ApiMessages.Add(new ApiMessage { Message = message, MessageType = messageType });
        }
        
        private void HandleAccountInfoCallBack(string result)
        {
            var webResult = JObject.Parse(result);
            bool handleMessage = true;
            if (webResult["success"].Value<int>() == 0)
            {
                LogApiMessage("Error", string.Format("Error while handling AccountInfo message. {0}", webResult["error"].Value<string>()));
                handleMessage = false;
            }

            if (handleMessage)
            {
                foreach (var fundItem in webResult["return"].Value<JObject>()["funds"].Value<JObject>())
                {
                    switch (fundItem.Key)
                    {
                        case "usd":
                            accountInfo.UsdAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "eur":
                            accountInfo.EurAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "rur":
                            accountInfo.RurAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "btc":
                            accountInfo.BtcAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "ltc":
                            accountInfo.LtcAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "nmc":
                            accountInfo.NmcAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "trc":
                            accountInfo.TrcAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "nvc":
                            accountInfo.NvcAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "ppc":
                            accountInfo.PpcAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "ftc":
                            accountInfo.FtcAmount = fundItem.Value.Value<decimal>();
                            break;

                        case "xpm":
                            accountInfo.XpmAmount = fundItem.Value.Value<decimal>();
                            break;
                    }
                }
            }

            OnPropertyChanged("AccountInfo");

            if (AccountInfoUpdated != null)
                AccountInfoUpdated(this, null);

            LogApiMessage("Api message processed", "Account info updated");
        }

        private void HandleActiveOrdersCallBack(string result)
        {
            var webResult = JObject.Parse(result);
            bool handleMessage = true;
            if (webResult["success"].Value<int>() == 0)
            {
                LogApiMessage("Error", string.Format("Error while handling AccountInfo message. {0}", webResult["error"].Value<string>()));
                handleMessage = false;
            }

            var newList = new List<IOrder>();
            if (handleMessage)
            {
                foreach (var orderItem in webResult["return"].Value<JObject>())
                {
                    var newOrder = new Order()
                        {
                            Id = orderItem.Key,
                            Pair = BtcePairHelper.FromString(orderItem.Value["pair"].Value<string>()),
                            Type = TradeTypeHelper.FromString(orderItem.Value["type"].Value<string>()),
                            Amount = orderItem.Value["amount"].Value<decimal>(),
                            Rate = orderItem.Value["rate"].Value<decimal>(),
                            CreateDate = UnixTimeHelper.UnixTimeToDateTime(orderItem.Value["timestamp_created"].Value<UInt32>()),
                            Status = orderItem.Value["status"].Value<int>()
                        };
                    newList.Add(newOrder);
                }
            }

            ActiveOrders = newList;
            OnPropertyChanged("ActiveOrders");

            if (ActiveOrdersUpdated != null)
                ActiveOrdersUpdated(this, null);

            LogApiMessage("Api message processed", "Active orders updated");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
