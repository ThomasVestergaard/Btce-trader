using System.Collections.Generic;
using System.Windows.Input;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Trade;

namespace BTCE_Trader.UI.UI.Dialogs
{
    public class EditTradeViewModel : BaseViewModel
    {
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

        public EditTradeViewModel(ITradeRequest initialTradeRequest)
        {
            TradeRequest = initialTradeRequest;
        }


    }
}
