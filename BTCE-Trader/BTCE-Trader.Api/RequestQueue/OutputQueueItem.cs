namespace BTCE_Trader.Api.RequestQueue
{
    public class OutputQueueItem
    {
        public string WebResult { get; set; }
        public InputQueueItem InputQueueItem { get; set; }

    }
}
