using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Orders;
using System.Linq;
using BTCE_Trader.UI.Commons;

namespace BTCE_Trader.UI.UI.UserControls
{
    public class ActiveOrdersViewModel : BaseViewModel
    {
        private readonly IBtceTradeApi tradeApi;
        private readonly IBtceModels btceModels;
        public ObservableCollection<IOrder> ActiveOrders { get; set; }
        public ICommand CancelOrderCommand { get; set; }

        public ActiveOrdersViewModel(IBtceTradeApi tradeApi, IBtceModels btceModels)
        {
            this.tradeApi = tradeApi;
            this.btceModels = btceModels;
            this.btceModels.ActiveOrdersUpdated += btceModels_ActiveOrdersUpdated;
            
            CancelOrderCommand = new RelayCommand(o =>
                {
                    var currentOrder = ActiveOrders.ToList().Find(a => a.Id == (string) o);
                    if (currentOrder != null)
                    {
                        string message = string.Format("Confirm cancelation of order:\n\n {0}", currentOrder.Summery);

                        if (MessageBox.Show(message, "Cancel order?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            tradeApi.CancelOrder(currentOrder.Id);
                        }
                    }
                });
            
        }

        void btceModels_ActiveOrdersUpdated(object sender, System.EventArgs e)
        {
            UiThread.UiDispatcher.Invoke(() =>
                {
                    ActiveOrders = new ObservableCollection<IOrder>(btceModels.ActiveOrders);
                    OnPropertyChanged("ActiveOrders");
                });

        }

        

    }
}
