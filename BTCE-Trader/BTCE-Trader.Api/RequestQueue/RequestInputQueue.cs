using System;
using System.Threading.Tasks;
using BTCE_Trader.Api.Web;
using Disruptor;
using Disruptor.Dsl;

namespace BTCE_Trader.Api.RequestQueue
{
    public class RequestInputQueue : IRequestInputQueue
    {
        private readonly IInputRequestHandler inputRequestHandler;
        private Disruptor<InputQueueItem> requestDisrupter;
        private RingBuffer<InputQueueItem> requestRingBuffer;

        public RequestInputQueue(IInputRequestHandler inputRequestHandler)
        {
            this.inputRequestHandler = inputRequestHandler;
        }

        public void Start()
        {
            requestDisrupter = new Disruptor<InputQueueItem>(() => new InputQueueItem(), (int)Math.Pow(16, 2), TaskScheduler.Default);
            requestDisrupter.HandleEventsWith(inputRequestHandler);
            requestRingBuffer = requestDisrupter.Start();
        }

        public void Stop()
        {
            requestDisrupter.Halt();
        }

        public void AddItemToQueue(InputQueueItem inputItem)
        {
            long sequenceNo = requestRingBuffer.Next();
            var entry = requestRingBuffer[sequenceNo];
            entry.RequestId = inputItem.RequestId;
            entry.MethodParameters = inputItem.MethodParameters;
            entry.MethodName = inputItem.MethodName;
            requestRingBuffer.Publish(sequenceNo);
        }
    }
}
