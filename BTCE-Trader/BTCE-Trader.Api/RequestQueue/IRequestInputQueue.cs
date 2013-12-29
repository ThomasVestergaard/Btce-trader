namespace BTCE_Trader.Api.RequestQueue
{
    public interface IRequestInputQueue
    {
        void Start();
        void Stop();
        void AddItemToQueue(InputQueueItem inputItem);
    }
}
