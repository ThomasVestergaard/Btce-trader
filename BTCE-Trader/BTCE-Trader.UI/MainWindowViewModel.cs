using System.Windows.Input;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Trade;
using BTCE_Trader.UI.Commons;
using BTCE_Trader.UI.UI.UserControls;
using BTCE_Trader.UI.UpdateAgents.AccountInfo;
using BTCE_Trader.UI.UpdateAgents.Depth;
using BTCE_Trader.UI.UpdateAgents.Orders;
using BTCE_Trader.UI.UpdateAgents.Trade;


namespace BTCE_Trader.UI
{
    public class MainWindowViewModel
    {
        private IDepthAgent depthAgent;
        private readonly IActiveOrderAgent activeOrderAgent;
        private readonly IBtceTradeApi btceTradeApi;
        private readonly IAccountInfoAgent accountInfoAgent;
        private readonly ITradeAgent tradeAgent;
        private readonly IBtceModels btceModels;
        public ICommand DemoTradeCommand { get; set; }

        public IDepthAgent DepthAgent
        {
            get { return depthAgent; }
            set { depthAgent = value; }
        }

        public MarketDepthViewModel DepthViewModel { get; set; }
        public ActiveOrdersViewModel ActiveOrdersViewModel { get; set; }
        public AccountInfoViewModel AccountInfoViewModel { get; set; }

        public MainWindowViewModel(IDepthAgent depthAgent, IActiveOrderAgent activeOrderAgent, IBtceTradeApi btceTradeApi, IAccountInfoAgent accountInfoAgent, ITradeAgent tradeAgent, IBtceModels btceModels)
        {
            this.depthAgent = depthAgent;
            this.activeOrderAgent = activeOrderAgent;
            this.btceTradeApi = btceTradeApi;
            this.accountInfoAgent = accountInfoAgent;
            this.tradeAgent = tradeAgent;
            this.btceModels = btceModels;

            DepthViewModel = new MarketDepthViewModel(depthAgent);
            ActiveOrdersViewModel = new ActiveOrdersViewModel(activeOrderAgent, btceTradeApi, btceModels);
            AccountInfoViewModel = new AccountInfoViewModel(btceModels);

            DemoTradeCommand = new RelayCommand((parameter) =>
                {
                    ITradeRequest tradeRequest = new TradeRequest
                        {
                            Pair = BtcePairEnum.btc_usd,
                            Amount = 0.01m,
                            Rate = 500,
                            TradeType = TradeTypeEnum.Buy
                        };

                    //tradeAgent.MakeTrade(tradeRequest);
                });
        }
    }
}
