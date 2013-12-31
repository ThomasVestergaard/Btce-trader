namespace BTCE_Trader.UI.UpdateAgents.Orders
{
    public interface IActiveOrderAgent
    {
        
        void Start(int updateInterval);
        void Stop();
    }
}
