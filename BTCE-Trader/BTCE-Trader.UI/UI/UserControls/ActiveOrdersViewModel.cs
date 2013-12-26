using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using BTCE_Trader.Api.Orders;
using BTCE_Trader.Core.Orders;
using BTCE_Trader.UI.Annotations;
using System.Linq;
using BTCE_Trader.UI.Commons;


namespace BTCE_Trader.UI.UI.UserControls
{
    public class ActiveOrdersViewModel : INotifyPropertyChanged
    {
        private readonly IActiveOrderAgent activeOrderAgent;
        public ObservableCollection<IOrder> ActiveOrders { get; set; }
        public ICommand CancelOrderCommand { get; set; }

        public ActiveOrdersViewModel(IActiveOrderAgent activeOrderAgent)
        {
            this.activeOrderAgent = activeOrderAgent;
            ActiveOrders = new ObservableCollection<IOrder>();
            CancelOrderCommand = new RelayCommand(o =>
                {
                    var currentOrder = ActiveOrders.ToList().Find(a => a.Id == (string) o);
                    if (currentOrder != null)
                    {
                        string message = string.Format("Cancel order?:\n\n {0}", currentOrder.Summery);

                        if (MessageBox.Show(message, "Cancel order", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            
                        }
                    }
                });
            this.activeOrderAgent.ActiveOrdersUpdated +=  activeOrderAgent_ActiveOrdersUpdated;
        }

        void activeOrderAgent_ActiveOrdersUpdated(List<IOrder> activeOrders)
        {
            UiThread.UiDispatcher.BeginInvoke(new Action(() =>
                {
                    SyncOrderCollection(activeOrders);
                }));

            
        }

        private void SyncOrderCollection(List<IOrder> activeOrders)
        {
            bool hasChanged = false;
            // Remove orders
            foreach (var activeOrder in ActiveOrders.ToList())
            {
                if (ActiveOrders.FirstOrDefault(a => a.Id == activeOrder.Id) == null)
                {
                    ActiveOrders.Remove(activeOrder);
                    hasChanged = true;
                }
            }

            foreach (var activeOrder in activeOrders.ToList())
            {
                if (ActiveOrders.FirstOrDefault(a => a.Id == activeOrder.Id) == null)
                {
                    ActiveOrders.Add(activeOrder);
                    hasChanged = true;
                }
            }

            if (activeOrders.Count == 0 && ActiveOrders.Count > 0)
                ActiveOrders.Clear();

            //OnPropertyChanged("ActiveOrders");
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
