using BTCE_Trader.Api;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.UI.UI.UserControls;
using BTCE_Trader.UI.UpdateAgents.Depth;

namespace BTCE_Trader.UI
{
    public class MainWindowViewModel
    {
        private IDepthAgent depthAgent;
        private readonly IBtceTradeApi btceTradeApi;
        private readonly IBtceModels btceModels;
        private readonly IConfiguration configuration;

        public IDepthAgent DepthAgent
        {
            get { return depthAgent; }
            set { depthAgent = value; }
        }

        public MarketDepthViewModel DepthViewModel { get; set; }
        public ActiveOrdersViewModel ActiveOrdersViewModel { get; set; }
        public AccountInfoViewModel AccountInfoViewModel { get; set; }

        public MainWindowViewModel(IBtceTradeApi btceTradeApi, IBtceModels btceModels, IConfiguration configuration)
        {
            this.btceTradeApi = btceTradeApi;
            this.btceModels = btceModels;
            this.configuration = configuration;

            DepthViewModel = new MarketDepthViewModel(this.btceModels, this.configuration, btceTradeApi);
            ActiveOrdersViewModel = new ActiveOrdersViewModel(this.btceTradeApi, this.btceModels);
            AccountInfoViewModel = new AccountInfoViewModel(this.btceModels);
        }
    }
}
