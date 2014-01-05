using System.Linq;
using BTCE_Trader.Api;

namespace BTCE_Trader.UI.UI.UserControls.MarketMaker
{
    public class MarketMakerViewModel : BaseViewModel
    {
        private readonly BtcePairEnum pair;
        private readonly IBtceModels models;

        public MarketMakingSituation CurrentSituation { get; set; }
        public MarketMakingSituation ObservingSituation { get; set; }

        
        public MarketMakerViewModel(BtcePairEnum pair, IBtceModels models)
        {
            this.pair = pair;
            this.models = models;

            ObservingSituation = new MarketMakingSituation();
            this.models.DepthUpdated += models_DepthUpdated;
        }

        void models_DepthUpdated(object sender, System.EventArgs e)
        {
            var Asks = models.MarketDepths[pair].Asks.OrderBy(a => a.Price).Take(20).OrderByDescending(a => a.Price).ToList();
            var Bids = models.MarketDepths[pair].Bids.OrderByDescending(a => a.Price).Take(20).ToList();

            ObservingSituation.Spread = Asks[Asks.Count - 1].Price - Bids[0].Price;

            ObservingSituation.MidPointPrice = (Asks[Asks.Count - 1].Price + Bids[0].Price) / 2;

            OnPropertyChanged("ObservingSituation");
        }
    }
}
