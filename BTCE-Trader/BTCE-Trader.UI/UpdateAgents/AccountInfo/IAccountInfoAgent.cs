namespace BTCE_Trader.UI.UpdateAgents.AccountInfo
{
    public interface IAccountInfoAgent
    {
        void Start(int updateInterval);
        void Stop();
    }
}
