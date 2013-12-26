using BTCE_Trader.Api;
using BTCE_Trader.Core.Depth;
using BTCE_Trader.Core.Orders;
using BTCE_Trader.UI.UI.UserControls;

namespace BTCE_Trader.UI
{
    public class MainWindowViewModel
    {
        private IDepthAgent depthAgent;
        private readonly IActiveOrderAgent activeOrderAgent;
        private readonly IBtceTradeApi btceTradeApi;

        public IDepthAgent DepthAgent
        {
            get { return depthAgent; }
            set { depthAgent = value; }
        }

        public MarketDepthViewModel DepthViewModel { get; set; }
        public ActiveOrdersViewModel ActiveOrdersViewModel { get; set; }

        public MainWindowViewModel(IDepthAgent depthAgent, IActiveOrderAgent activeOrderAgent, IBtceTradeApi btceTradeApi)
        {
            this.depthAgent = depthAgent;
            this.activeOrderAgent = activeOrderAgent;
            this.btceTradeApi = btceTradeApi;

            DepthViewModel = new MarketDepthViewModel(depthAgent);
            ActiveOrdersViewModel = new ActiveOrdersViewModel(activeOrderAgent, btceTradeApi);
        }
    }
}
