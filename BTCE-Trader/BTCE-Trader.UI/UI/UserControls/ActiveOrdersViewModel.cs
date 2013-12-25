using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using BTCE_Trader.Api.Orders;
using BTCE_Trader.Core.Orders;
using BTCE_Trader.UI.Annotations;
using System.Linq;

namespace BTCE_Trader.UI.UI.UserControls
{
    public class ActiveOrdersViewModel : INotifyPropertyChanged
    {
        private readonly IActiveOrderAgent activeOrderAgent;
        
        private List<IOrder> activeOrders { get; set; }
        public List<IOrder> ActiveOrders
        {
            get { return activeOrders; }
            set { activeOrders = value; }
        }


        private SynchronizationContext UiContext;
        public ActiveOrdersViewModel(IActiveOrderAgent activeOrderAgent)
        {
            this.activeOrderAgent = activeOrderAgent;
            activeOrders = new List<IOrder>();
            ActiveOrders = new List<IOrder>();
            UiContext = SynchronizationContext.Current;
            this.activeOrderAgent.ActiveOrdersUpdated += activeOrderAgent_ActiveOrdersUpdated;
        }

        void activeOrderAgent_ActiveOrdersUpdated(List<IOrder> activeOrders)
        {
            UiContext.Send(a => SyncOrderCollection(activeOrders), null);

            //this.activeOrders = activeOrders;
            OnPropertyChanged("ActiveOrders");
        }

        private void SyncOrderCollection(List<IOrder> activeOrders)
        {
            // Remove orders
            foreach (var activeOrder in this.activeOrders.ToList())
            {
                if (activeOrders.Find(a => a.Id == activeOrder.Id) == null)
                    this.activeOrders.Remove(activeOrder);
            }

            foreach (var activeOrder in activeOrders.ToList())
            {
                if (this.activeOrders.Find(a => a.Id == activeOrder.Id) == null)
                    activeOrders.Add(activeOrder);
            }
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
