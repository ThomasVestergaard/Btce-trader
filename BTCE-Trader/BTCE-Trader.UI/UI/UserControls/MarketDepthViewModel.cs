using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.Api.Depth;
using BTCE_Trader.Api.Trade;
using BTCE_Trader.UI.Commons;
using BTCE_Trader.UI.Configurations;
using BTCE_Trader.UI.UI.Dialogs;
using BTCE_Trader.UI.UpdateAgents.Depth;


namespace BTCE_Trader.UI.UI.UserControls
{
    public class MarketDepthViewModel : BaseViewModel
    {
        public BtcePairEnum CurrentPair { get; set; }
        private readonly IBtceModels btceModels;
        private readonly IConfiguration configuration;
        private readonly IBtceTradeApi btceTradeApi;
        private readonly ITradingConfigurations tradingConfigurations;

        public List<IDepthOrderInfo> Asks { get; set; }
        public List<IDepthOrderInfo> Bids { get; set; }

        public List<IDepthOrderInfo> AggregatedAsks { get; set; }
        public List<IDepthOrderInfo> AggregatedBids { get; set; }

        public ICommand AskDoubbleClickCommand { get; set; }
        public ICommand BidsDoubbleClickCommand { get; set; }

        private decimal spread { get; set; }
        public string Spread
        {
            get
            {
                return string.Format("Spread: {0}", spread);
            }
        }

        public MarketDepthViewModel(IBtceModels btceModels, IConfiguration configuration, IBtceTradeApi btceTradeApi, ITradingConfigurations tradingConfigurations)
        {
            this.btceModels = btceModels;
            this.configuration = configuration;
            this.btceTradeApi = btceTradeApi;
            this.tradingConfigurations = tradingConfigurations;
            this.CurrentPair = BtcePairEnum.nmc_usd;

            AggregatedAsks = new List<IDepthOrderInfo>();
            AggregatedBids = new List<IDepthOrderInfo>();
            Asks = new List<IDepthOrderInfo>();
            Bids = new List<IDepthOrderInfo>();

            btceModels.DepthUpdated += btceModels_DepthUpdated;

            AskDoubbleClickCommand = new RelayCommand((tradeClickParameters) =>
                {
                    ShowTradeWindow(((IDepthOrderInfo)tradeClickParameters).Price, TradeTypeEnum.Sell);
                });

            BidsDoubbleClickCommand = new RelayCommand((tradeClickParameters) =>
            {
                ShowTradeWindow(((IDepthOrderInfo)tradeClickParameters).Price, TradeTypeEnum.Buy);
            });
        }

        private void ShowTradeWindow(decimal price, TradeTypeEnum way)
        {
            ITradeRequest sellTrade = new TradeRequest
            {
                Amount = 1,
                Pair = CurrentPair,
                Rate = price,
                TradeType = way
            };

            var editTradeViewModel = new EditTradeViewModel(sellTrade, tradingConfigurations, btceModels.AccountInfo);
            var editTradeView = new EditTrade();
            editTradeView.DataContext = editTradeViewModel;

            editTradeViewModel.CancelCommand = new RelayCommand((parameters) =>
            {
                editTradeView.Close();
            });

            editTradeViewModel.OkCommand = new RelayCommand((parameters) =>
            {
                btceTradeApi.MakeTrade(editTradeViewModel.TradeRequest);
                editTradeView.Close();
            });


            editTradeView.ShowDialog();

        }

        void btceModels_DepthUpdated(object sender, System.EventArgs e)
        {
            UiThread.UiDispatcher.Invoke(() =>
            {
                Asks = btceModels.MarketDepths[CurrentPair].Asks.OrderBy(a => a.Price).Take(20).OrderByDescending(a => a.Price).ToList();
                Bids = btceModels.MarketDepths[CurrentPair].Bids.OrderByDescending(a => a.Price).Take(20).ToList();

                AggregatedAsks = DepthHelper.GetAggregatedAskOrderList(btceModels.MarketDepths[CurrentPair].Asks, configuration.PairAggregatorIncrement[CurrentPair]).OrderByDescending(a => a.Price).ToList();
                AggregatedBids = DepthHelper.GetAggregatedBidOrderList(btceModels.MarketDepths[CurrentPair].Bids, configuration.PairAggregatorIncrement[CurrentPair]).OrderByDescending(a => a.Price).ToList();

                foreach (var order in btceModels.ActiveOrders.FindAll(a => a.Pair == CurrentPair && a.Type == TradeTypeEnum.Sell))
                {
                    var ask = Asks.Find(a => a.Price == order.Rate);
                    if (ask != null)
                        ask.ActiveOrder = string.Format("Sell {0}", order.Amount);
                }

                foreach (var order in btceModels.ActiveOrders.FindAll(a => a.Pair == CurrentPair && a.Type == TradeTypeEnum.Buy))
                {
                    var bid = Bids.Find(a => a.Price == order.Rate);
                    if (bid != null)
                        bid.ActiveOrder = string.Format("Buy {0}", order.Amount);
                }

                spread = Asks[Asks.Count - 1].Price - Bids[0].Price;
                OnPropertyChanged("Asks");
                OnPropertyChanged("Bids");
                OnPropertyChanged("AggregatedAsks");
                OnPropertyChanged("AggregatedBids");
                OnPropertyChanged("Spread");
            });
        }
    }
}
