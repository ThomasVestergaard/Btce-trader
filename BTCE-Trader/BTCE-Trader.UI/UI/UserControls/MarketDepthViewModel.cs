using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Depth;
using BTCE_Trader.UI.Annotations;
using BTCE_Trader.UI.UpdateAgents.Depth;
using DepthHelper = BTCE_Trader.UI.UpdateAgents.Depth.DepthHelper;

namespace BTCE_Trader.UI.UI.UserControls
{
    public class MarketDepthViewModel : INotifyPropertyChanged
    {
        private readonly IDepthAgent depthUpdater;
        private List<IDepthOrderInfo> asks;
        private List<IDepthOrderInfo> bids;

        public List<IDepthOrderInfo> Asks
        {
            get { return asks; }
        }
        public List<IDepthOrderInfo> Bids
        {
            get { return bids; }
        }

        private List<IDepthOrderInfo> aggregatedAsks;
        private List<IDepthOrderInfo> aggregatedBids;

        public List<IDepthOrderInfo> AggregatedAsks
        {
            get { return aggregatedAsks; }
        }
        public List<IDepthOrderInfo> AggregatedBids
        {
            get { return aggregatedBids; }
        }

        private decimal spread { get; set; }
        public string Spread
        {
            get
            {
                return string.Format("Spread: {0}", spread);
            }
        }

        public MarketDepthViewModel(IDepthAgent depthUpdater)
        {
            asks = new List<IDepthOrderInfo>();
            bids = new List<IDepthOrderInfo>();
            aggregatedAsks = new List<IDepthOrderInfo>();
            aggregatedBids = new List<IDepthOrderInfo>();

            this.depthUpdater = depthUpdater;
            this.depthUpdater.DepthUpdated += depthUpdater_DepthUpdated;
        }

        void depthUpdater_DepthUpdated(Dictionary<BtcePairEnum, IMarketDepth> pairDepthPairs)
        {
            if (!pairDepthPairs.ContainsKey(BtcePairEnum.ltc_btc))
                return;

            var depth = pairDepthPairs[BtcePairEnum.ltc_btc];

            asks = depth.Asks.OrderBy(a => a.Price).Take(10).OrderByDescending(a => a.Price).ToList();
            bids = depth.Bids.OrderByDescending(a => a.Price).Take(10).ToList();

            aggregatedAsks = DepthHelper.GetAggregatedAskOrderList(depth.Asks, 0.0001m).OrderByDescending(a => a.Price).ToList();
            aggregatedBids = DepthHelper.GetAggregatedBidOrderList(depth.Bids, 0.0001m).OrderByDescending(a => a.Price).ToList();

            spread = asks[asks.Count - 1].Price - bids[0].Price;

            OnPropertyChanged("Bids");
            OnPropertyChanged("Asks");
            OnPropertyChanged("AggregatedAsks");
            OnPropertyChanged("AggregatedBids");
            OnPropertyChanged("Spread");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
