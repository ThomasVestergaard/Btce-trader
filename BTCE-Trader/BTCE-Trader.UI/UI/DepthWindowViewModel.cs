﻿using System;
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
        private readonly IDepthUpdater depthUpdater;
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


        public DepthWindowViewModel(IDepthUpdater depthUpdater)
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
            bids = depth.Bids.OrderBy(a => a.Price).Take(10).ToList();

            OnPropertyChanged("Bids");
            OnPropertyChanged("Asks");
            OnPropertyChanged("AggregatedAsks");
            OnPropertyChanged("AggregatedBids");
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