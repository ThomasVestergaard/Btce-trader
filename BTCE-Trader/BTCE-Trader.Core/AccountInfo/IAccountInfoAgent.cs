namespace BTCE_Trader.Core.AccountInfo
{
    public interface IAccountInfoAgent
    {
        void Start(int updateInterval);
        void Stop();
        event AccountInfoAgent.AccountInfoUpdatedDelegate AccountUpdated;
    }
}
