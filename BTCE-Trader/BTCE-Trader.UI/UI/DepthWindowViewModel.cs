using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using BTCE_Trader.Core.Depth;
using BTCE_Trader.UI.Annotations;
using BtcE;

namespace BTCE_Trader.UI.UI
{
    public class DepthWindowViewModel : INotifyPropertyChanged
    {
        private readonly IDepthAgent depthUpdater;
        private List<OrderInfo> asks;
        private List<OrderInfo> bids;

        public List<OrderInfo> Asks
        {
            get { return asks; }
        }
        public List<OrderInfo> Bids
        {
            get { return bids; }
        }

        private List<OrderInfo> aggregatedAsks;
        private List<OrderInfo> aggregatedBids;

        public List<OrderInfo> AggregatedAsks
        {
            get { return aggregatedAsks; }
        }
        public List<OrderInfo> AggregatedBids
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


        public DepthWindowViewModel(IDepthAgent depthUpdater)
        {
            asks = new List<OrderInfo>();
            bids = new List<OrderInfo>();
            aggregatedAsks = new List<OrderInfo>();
            aggregatedBids = new List<OrderInfo>();

            this.depthUpdater = depthUpdater;
            this.depthUpdater.DepthUpdated += depthUpdater_DepthUpdated;
        }

        void depthUpdater_DepthUpdated(System.Collections.Generic.Dictionary<BtcE.BtcePair, BtcE.Depth> pairDepthPairs)
        {
            if (!pairDepthPairs.ContainsKey(BtcePair.ltc_btc))
                return;

            var depth = pairDepthPairs[BtcePair.ltc_btc];

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
