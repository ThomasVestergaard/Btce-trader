using System.Collections.Generic;
using System.Linq;
using BTCE_Trader.Api;
using BTCE_Trader.Api.MarketTrades;
using BTCE_Trader.UI.Commons;

namespace BTCE_Trader.UI.UI.UserControls
{
    public class TradeTickerViewModel : BaseViewModel
    {
        private readonly IBtceModels btceModels;
        private readonly BtcePairEnum pair;

        public List<IMarketTrade> Trades { get; set; }

        public TradeTickerViewModel(IBtceModels btceModels, BtcePairEnum pair)
        {
            this.btceModels = btceModels;
            this.pair = pair;
            Trades = new List<IMarketTrade>();
            this.btceModels.MarketTradesUpdated += btceModels_MarketTradesUpdated;
        }

        void btceModels_MarketTradesUpdated(object sender, System.EventArgs e)
        {
            UiThread.UiDispatcher.Invoke(() =>
                {
                    Trades = btceModels.PairTrades[pair].OrderByDescending(a => a.Timestamp).Take(50).OrderByDescending(a => a.Timestamp).ToList();
                    OnPropertyChanged("Trades");
                });
        }
    }
}
