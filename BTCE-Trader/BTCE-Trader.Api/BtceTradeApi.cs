using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTCE_Trader.Api.Orders;
using BTCE_Trader.Api.Time;
using BTCE_Trader.Api.Web;
using Newtonsoft.Json.Linq;

namespace BTCE_Trader.Api
{
    public class BtceTradeApi : BaseApi
    {
        public BtceTradeApi(IWebRequestWrapper webRequest) : base(webRequest)
        {

        }

        public List<IOrder> GetActiveOrders()
        {
            var toReturn = new List<IOrder>();

            var webResult = JObject.Parse(Query("ActiveOrders"));
            
            if (webResult["success"].Value<int>() == 0)
                return toReturn;

            foreach (var orderItem in webResult["return"].Value<JObject>())
            {
                var newOrder = new Order()
                {
                    Pair = BtcePairHelper.FromString(orderItem.Value["pair"].Value<string>()),
                    Type = TradeTypeHelper.FromString(orderItem.Value["type"].Value<string>()),
                    Amount = orderItem.Value["amount"].Value<decimal>(),
                    Rate = orderItem.Value["rate"].Value<decimal>(),
                    CreateDate = UnixTimeHelper.UnixTimeToDateTime(orderItem.Value["timestamp_created"].Value<UInt32>()),
                    Status = orderItem.Value["status"].Value<int>()
                };

                toReturn.Add(newOrder);
            }


            return toReturn;
        }
    }
}
