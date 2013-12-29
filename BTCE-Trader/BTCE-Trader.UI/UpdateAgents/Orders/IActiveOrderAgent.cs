namespace BTCE_Trader.UI.UpdateAgents.Orders
{
    public interface IActiveOrderAgent
    {
        event ActiveOrderAgent.ActiveOrdersUpdatedDelegate ActiveOrdersUpdated;
        void Start(int updateInterval);
        void Stop();
    }
}
