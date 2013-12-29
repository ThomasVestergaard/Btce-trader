using System;
using System.Threading.Tasks;
using Disruptor;
using Disruptor.Dsl;

namespace BTCE_Trader.Api.RequestQueue
{
    public class RequestOutputQueue : IRequestOutputQueue
    {
        private readonly IEventHandler<OutputQueueItem>[] eventHandlers;
        private Disruptor<OutputQueueItem> requestDisrupter;
        private RingBuffer<OutputQueueItem> requestRingBuffer;

        public RequestOutputQueue(IEventHandler<OutputQueueItem>[] eventHandlers )
        {
            this.eventHandlers = eventHandlers;
        }
        
        public void Start()
        {
            requestDisrupter = new Disruptor<OutputQueueItem>(() => new OutputQueueItem(), (int)Math.Pow(16, 2), TaskScheduler.Default);
            requestDisrupter.HandleEventsWith(eventHandlers);
            requestRingBuffer = requestDisrupter.Start();
        }

        public void Stop()
        {
            requestDisrupter.Halt();
        }

        public void AddItemToQueue(OutputQueueItem outputItem)
        {
            long sequenceNo = requestRingBuffer.Next();
            var entry = requestRingBuffer[sequenceNo];

            entry.InputQueueItem = outputItem.InputQueueItem;
            entry.WebResult = outputItem.WebResult;
            requestRingBuffer.Publish(sequenceNo);
        }
    }
}