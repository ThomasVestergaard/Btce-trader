using System;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.Api.RequestQueue;
using log4net;

namespace BTCE_Trader.Api.Web
{
    public class InputRequestHandler : IInputRequestHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(InputRequestHandler));
        private readonly IWebRequestWrapper webRequestWrapper;
        private readonly IRequestOutputQueue requestOutputQueue;
        private readonly IConfiguration configuration;

        public InputRequestHandler(IWebRequestWrapper webRequestWrapper, IRequestOutputQueue requestOutputQueue, IConfiguration configuration)
        {
            this.webRequestWrapper = webRequestWrapper;
            this.requestOutputQueue = requestOutputQueue;
            this.configuration = configuration;
        }

        public void OnNext(InputQueueItem data, long sequence, bool endOfBatch)
        {
            try
            {
                string webResult = string.Empty;
                string parameters = "";
                int counter = 0;
                switch (data.MethodName)
                {
                    case BtceTradeApi.BtceCommandUpdateDepth:

                        counter = 0;
                        foreach (var pair in configuration.Pairs)
                        {
                            parameters += pair.ToString();
                            if (counter < configuration.Pairs.Count - 1)
                                parameters += "-";

                            counter++;
                        }

                        webResult = webRequestWrapper.RequestV3("depth", parameters);
                        break;

                    case BtceTradeApi.BtceCommandUpdateMarketTrades:
                        
                        counter = 0;
                        foreach (var pair in configuration.Pairs)
                        {
                            parameters += pair.ToString();
                            if (counter < configuration.Pairs.Count - 1)
                                parameters += "-";

                            counter++;
                        }
                        webResult = webRequestWrapper.RequestV3("trades", parameters);
                        //webResult = webRequestWrapper.RequestData(data.MethodName, data.MethodParameters);
                        break;

                    default:
                        webResult = webRequestWrapper.RequestData(data.MethodName, data.MethodParameters);
                        break;
                }

                requestOutputQueue.AddItemToQueue(new OutputQueueItem
                    {
                        InputQueueItem = data,
                        WebResult = webResult
                    });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

    }
}