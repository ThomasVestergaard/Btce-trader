using BTCE_Trader.Core.Depth;
using BTCE_Trader.UI.UI.UserControls;

namespace BTCE_Trader.UI
{
    public class MainWindowViewModel
    {
        private IDepthAgent depthAgent;
        public IDepthAgent DepthAgent
        {
            get { return depthAgent; }
            set { depthAgent = value; }
        }

        public MarketDepthViewModel DepthViewModel { get; set; }

        public MainWindowViewModel(IDepthAgent depthAgent)
        {
            this.depthAgent = depthAgent;
            DepthViewModel = new MarketDepthViewModel(depthAgent);
        }
    }
}
