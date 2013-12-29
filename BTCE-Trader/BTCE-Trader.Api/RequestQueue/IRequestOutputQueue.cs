namespace BTCE_Trader.Api.RequestQueue
{
    public interface IRequestOutputQueue
    {
        void Start();
        void Stop();
        void AddItemToQueue(OutputQueueItem outputItem);
    }
}
