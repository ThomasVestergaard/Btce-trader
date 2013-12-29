using System;
using BTCE_Trader.Api.RequestQueue;
using log4net;

namespace BTCE_Trader.Api.Web
{
    public class InputRequestHandler : IInputRequestHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(InputRequestHandler));
        private readonly IWebRequestWrapper webRequestWrapper;
        private readonly IRequestOutputQueue requestOutputQueue;

        public InputRequestHandler(IWebRequestWrapper webRequestWrapper, IRequestOutputQueue requestOutputQueue)
        {
            this.webRequestWrapper = webRequestWrapper;
            this.requestOutputQueue = requestOutputQueue;
        }

        public void OnNext(InputQueueItem data, long sequence, bool endOfBatch)
        {
            try
            {
                string webResult = string.Empty;
                webResult = webRequestWrapper.RequestData(data.MethodName, data.MethodParameters);
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