namespace BTCE_Trader.Core.Orders
{
    public interface IActiveOrderAgent
    {
        event ActiveOrderAgent.ActiveOrdersUpdatedDelegate ActiveOrdersUpdated;
        void Start(int updateInterval);
        void Stop();
    }
}
