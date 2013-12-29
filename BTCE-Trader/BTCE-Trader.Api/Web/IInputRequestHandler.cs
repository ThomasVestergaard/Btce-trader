using BTCE_Trader.Api.RequestQueue;
using Disruptor;

namespace BTCE_Trader.Api.Web
{
    public interface IInputRequestHandler : IEventHandler<InputQueueItem>
    {
    }
}
