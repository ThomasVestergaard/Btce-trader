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

                switch (data.MethodName)
                {
                    case BtceTradeApi.BtceCommandUpdateDepth:
                        string parameters = "";
                        int c = 0;
                        foreach (var pair in configuration.Pairs)
                        {
                            parameters += pair.ToString();
                            if (c < configuration.Pairs.Count - 1)
                                parameters += "-";

                            c++;
                        }

                        webResult = webRequestWrapper.RequestV3("depth", parameters);
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