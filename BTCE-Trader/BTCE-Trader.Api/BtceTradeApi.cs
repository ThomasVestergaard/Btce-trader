using System.Collections.Generic;
using System.Globalization;
using BTCE_Trader.Api.Depth;
using BTCE_Trader.Api.RequestQueue;
using BTCE_Trader.Api.Trade;
using BTCE_Trader.Api.Web;
using Newtonsoft.Json.Linq;

namespace BTCE_Trader.Api
{
    public class BtceTradeApi : IBtceTradeApi
    {
        public const string BtceCommandCancelOrder = "CancelOrder";
        public const string BtceCommandTrade = "Trade";
        public const string BtceCommandActiveOrders = "ActiveOrders";
        public const string BtceCommandAccountInfo = "getInfo";
        public const string BtceCommandUpdateDepth = "UpdateDepth";

        private readonly IRequestInputQueue requestInputQueue;
        private IWebRequestWrapper webRequest { get; set; }


        public BtceTradeApi(IWebRequestWrapper webRequest, IRequestInputQueue requestInputQueue)
        {
            this.webRequest = webRequest;
            this.requestInputQueue = requestInputQueue;
        }

        public void GetActiveOrders()
        {   
            requestInputQueue.AddItemToQueue(new InputQueueItem
            {
                MethodName = BtceCommandActiveOrders,
                MethodParameters = new Dictionary<string, string>()
            });
        }
        public Dictionary<BtcePairEnum, IMarketDepth> GetMarketDepths(List<BtcePairEnum> pairs)
        {
            var toReturn = new Dictionary<BtcePairEnum, IMarketDepth>();

            string parameters = "";
            int c = 0;
            foreach (var pair in pairs)
            {
                parameters += pair.ToString();
                if (c < pairs.Count - 1)
                    parameters += "-";

                c++;
            }

            var urlData = webRequest.RequestV3("depth", parameters);
            if (urlData == null)
                return null;

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
        public void CancelOrder(string orderId)
        {
            var cancelOrderParams = new Dictionary<string, string>();
            cancelOrderParams.Add("order_id", orderId);

            requestInputQueue.AddItemToQueue(new InputQueueItem
            {
                MethodName = BtceCommandCancelOrder,
                MethodParameters = cancelOrderParams
            });
        }
        public void GetAccountInfo()
        {
            requestInputQueue.AddItemToQueue(new InputQueueItem
            {
                MethodName = BtceCommandAccountInfo,
                MethodParameters = new Dictionary<string, string>()
            });
        }
        public void UpdateDepth()
        {
            requestInputQueue.AddItemToQueue(new InputQueueItem
            {
                MethodName = BtceCommandUpdateDepth,
                MethodParameters = new Dictionary<string, string>()
            });
        }
        public void MakeTrade(ITradeRequest tradeRequest)
        {
            var p = new Dictionary<string, string>();
            p.Add("pair", BtcePairHelper.ToString(tradeRequest.Pair));
            p.Add("type", TradeTypeHelper.ToString(tradeRequest.TradeType));
            p.Add("rate", tradeRequest.Rate.ToString(CultureInfo.InvariantCulture));
            p.Add("amount", tradeRequest.Amount.ToString(CultureInfo.InvariantCulture));

            requestInputQueue.AddItemToQueue(new InputQueueItem
                {
                    MethodName = BtceCommandTrade,
                    MethodParameters = p
                });

            
            /*
            string tradeResult = Query("Trade", p);
            var tradeResponse = JObject.Parse(tradeResult);

            if (tradeResponse["success"].Value<int>() == 0)
            {
                toReturn.IsSuccess = false;
                toReturn.ErrorMessage = tradeResponse["error"].Value<string>();
            }
            else
            {
                toReturn.IsSuccess = true;
                toReturn.ErrorMessage = string.Empty;
            }

            return toReturn;*/
        }

        

    }
}
