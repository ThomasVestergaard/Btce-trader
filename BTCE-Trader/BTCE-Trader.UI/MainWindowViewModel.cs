using BTCE_Trader.Core.Depth;
using BTCE_Trader.Core.Orders;
using BTCE_Trader.UI.UI.UserControls;

namespace BTCE_Trader.UI
{
    public class MainWindowViewModel
    {
        private IDepthAgent depthAgent;
        private readonly IActiveOrderAgent activeOrderAgent;

        public IDepthAgent DepthAgent
        {
            get { return depthAgent; }
            set { depthAgent = value; }
        }

        public MarketDepthViewModel DepthViewModel { get; set; }
        public ActiveOrdersViewModel ActiveOrdersViewModel { get; set; }

        public MainWindowViewModel(IDepthAgent depthAgent, IActiveOrderAgent activeOrderAgent)
        {
            this.depthAgent = depthAgent;
            this.activeOrderAgent = activeOrderAgent;

            DepthViewModel = new MarketDepthViewModel(depthAgent);
            ActiveOrdersViewModel = new ActiveOrdersViewModel(activeOrderAgent);
        }
    }
}
