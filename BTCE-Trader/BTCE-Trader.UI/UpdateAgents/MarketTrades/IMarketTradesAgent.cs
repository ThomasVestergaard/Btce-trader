namespace BTCE_Trader.UI.UpdateAgents.MarketTrades
{
    public interface IMarketTradesAgent
    {
        void Start(int updateInterval);
        void Stop();
    }
}
