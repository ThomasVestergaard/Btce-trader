using System;
using System.Collections.Generic;
using BTCE_Trader.Api.Depth;
using BTCE_Trader.Api.Orders;
using BTCE_Trader.Api.Time;
using BTCE_Trader.Api.Web;
using Newtonsoft.Json.Linq;

namespace BTCE_Trader.Api
{
    public class BtceTradeApi : BaseApi, IBtceTradeApi
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

        public Dictionary<BtcePairEnum, MarketDepth> GetMarketDepths(List<BtcePairEnum> pairs)
        {
            var toReturn = new Dictionary<BtcePairEnum, MarketDepth>();

            string parameters = "";
            int c = 0;
            foreach (var pair in pairs)
            {
                parameters += pair.ToString();
                if (c < pairs.Count - 1)
                    parameters += "-";

                c++;
            }

            var urlData = V3Query("depth", parameters);
            var webResult = JObject.Parse(urlData);

            foreach (var pair in pairs)
            {
                toReturn.Add(pair, new MarketDepth());

                foreach (var ask in webResult[BtcePairHelper.ToString(pairs[0])]["asks"])
                {
                    var price = ask[0].Value<decimal>();
                    var amount = ask[1].Value<decimal>();
                    toReturn[pair].Asks.Add(new DepthOrderInfo { Amount = amount, Price = price});
                }

                foreach (var bid in webResult[BtcePairHelper.ToString(pairs[0])]["bids"])
                {
                    var price = bid[0].Value<decimal>();
                    var amount = bid[1].Value<decimal>();
                    toReturn[pair].Bids.Add(new DepthOrderInfo { Amount = amount, Price = price });
                }
            }

            return toReturn;
        }
    }
}
