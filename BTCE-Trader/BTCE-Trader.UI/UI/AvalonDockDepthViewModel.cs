﻿using BTCE_Trader.Api;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.UI.Configurations;
using BTCE_Trader.UI.UI.UserControls;

namespace BTCE_Trader.UI.UI
{
    public class AvalonDockDepthViewModel : BaseViewModel
    {
        private readonly IBtceModels btceModels;
        private readonly IConfiguration configuration;
        private readonly IBtceTradeApi tradeApi;
        private readonly ITradingConfigurations tradingConfigurations;
        public MarketDepthViewModel Model { get; set; }
        
        private string paneTitle;
        public string PaneTitle
        {
            get { return paneTitle; }
            set
            {
                paneTitle = value;
                OnPropertyChanged();
            }
        }

        public AvalonDockDepthViewModel(BtcePairEnum pair, IBtceModels btceModels, IConfiguration configuration, IBtceTradeApi tradeApi, ITradingConfigurations tradingConfigurations)
        {
            this.btceModels = btceModels;
            this.configuration = configuration;
            this.tradeApi = tradeApi;
            this.tradingConfigurations = tradingConfigurations;
            paneTitle = pair.ToString();
            Model = new MarketDepthViewModel(btceModels, configuration, tradeApi, tradingConfigurations);
            Model.CurrentPair = pair;
        }
    }
}
