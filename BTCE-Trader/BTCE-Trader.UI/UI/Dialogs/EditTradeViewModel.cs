using System.Collections.Generic;
using System.Windows.Input;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Info;
using BTCE_Trader.Api.Trade;
using BTCE_Trader.UI.Commons;
using BTCE_Trader.UI.Configurations;

namespace BTCE_Trader.UI.UI.Dialogs
{
    public class EditTradeViewModel : BaseViewModel
    {
        private const decimal TickUpDownInterval = 0.01m;

        private readonly ITradingConfigurations tradingConfigurations;
        private readonly IAccountInfo accountInfo;
        private ITradeRequest tradeRequest;
        public ITradeRequest TradeRequest
        {
            get { return tradeRequest; }
            set { tradeRequest = value; }
        }
        public List<TradeTypeEnum> TradeWays { get
        {
            return new List<TradeTypeEnum> { TradeTypeEnum.Buy, TradeTypeEnum.Sell };
        }}
        
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand RateTickUp { get; set; }
        public ICommand RateTickDown { get; set; }

        public decimal CurrentHoldings {get { return accountInfo.GetAmountFromEnum(tradeRequest.Pair); }}

        public EditTradeViewModel(ITradeRequest initialTradeRequest, ITradingConfigurations tradingConfigurations, IAccountInfo accountInfo)
        {
            this.tradingConfigurations = tradingConfigurations;
            this.accountInfo = accountInfo;
            TradeRequest = initialTradeRequest;

            RateTickUp = new RelayCommand((parameters) =>
                {
                    tradeRequest.Rate += TickUpDownInterval;
                    OnPropertyChanged("TradeRequest");
                });

            RateTickDown = new RelayCommand((parameters) =>
            {
                tradeRequest.Rate -= TickUpDownInterval;
                OnPropertyChanged("TradeRequest");
            });

            tradeRequest.Amount = tradingConfigurations.CalculateAmount(initialTradeRequest.Pair, initialTradeRequest.TradeType, accountInfo.GetAmountFromEnum(initialTradeRequest.Pair));
        }
    }
}
