using System.Windows;
using System.Windows.Input;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Trade;
using BTCE_Trader.Core.AccountInfo;
using BTCE_Trader.Core.Depth;
using BTCE_Trader.Core.Orders;
using BTCE_Trader.Core.Trade;
using BTCE_Trader.UI.Commons;
using BTCE_Trader.UI.UI.UserControls;

namespace BTCE_Trader.UI
{
    public class MainWindowViewModel
    {
        private IDepthAgent depthAgent;
        private readonly IActiveOrderAgent activeOrderAgent;
        private readonly IBtceTradeApi btceTradeApi;
        private readonly IAccountInfoAgent accountInfoAgent;
        private readonly ITradeAgent tradeAgent;
        public ICommand DemoTradeCommand { get; set; }

        public IDepthAgent DepthAgent
        {
            get { return depthAgent; }
            set { depthAgent = value; }
        }

        public MarketDepthViewModel DepthViewModel { get; set; }
        public ActiveOrdersViewModel ActiveOrdersViewModel { get; set; }
        public AccountInfoViewModel AccountInfoViewModel { get; set; }

        public MainWindowViewModel(IDepthAgent depthAgent, IActiveOrderAgent activeOrderAgent, IBtceTradeApi btceTradeApi, IAccountInfoAgent accountInfoAgent, ITradeAgent tradeAgent)
        {
            this.depthAgent = depthAgent;
            this.activeOrderAgent = activeOrderAgent;
            this.btceTradeApi = btceTradeApi;
            this.accountInfoAgent = accountInfoAgent;
            this.tradeAgent = tradeAgent;

            DepthViewModel = new MarketDepthViewModel(depthAgent);
            ActiveOrdersViewModel = new ActiveOrdersViewModel(activeOrderAgent, btceTradeApi);
            AccountInfoViewModel = new AccountInfoViewModel(accountInfoAgent);

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
