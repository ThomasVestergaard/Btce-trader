using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.UI.Commons;
using BTCE_Trader.UI.UI;
using BTCE_Trader.UI.UI.Dialogs;
using BTCE_Trader.UI.UI.UserControls;

namespace BTCE_Trader.UI
{
    public class MainWindowViewModel
    {
        private readonly IBtceTradeApi btceTradeApi;
        private readonly IBtceModels btceModels;
        private readonly IConfiguration configuration;

        public MarketDepthViewModel DepthViewModel { get; set; }
        public ActiveOrdersViewModel ActiveOrdersViewModel { get; set; }
        public AccountInfoViewModel AccountInfoViewModel { get; set; }

        public List<BtcePairEnum> AvailablePairs
        {
            get { return configuration.Pairs; }
        }
        public BtcePairEnum SelectedPair { get; set; }

        public ObservableCollection<AvalonDockDepthViewModel> DepthViewModels { get; set; }

        public ICommand AddDepthCommand { get; set; }

        public MainWindowViewModel(IBtceTradeApi btceTradeApi, IBtceModels btceModels, IConfiguration configuration)
        {
            this.btceTradeApi = btceTradeApi;
            this.btceModels = btceModels;
            this.configuration = configuration;

            DepthViewModels = new ObservableCollection<AvalonDockDepthViewModel>();

            DepthViewModel = new MarketDepthViewModel(this.btceModels, this.configuration, btceTradeApi);
            ActiveOrdersViewModel = new ActiveOrdersViewModel(this.btceTradeApi, this.btceModels);
            AccountInfoViewModel = new AccountInfoViewModel(this.btceModels);

            AddDepthCommand = new RelayCommand((parameters) =>
                {
                    var viewModel = new SelectPairViewModel(configuration);
                    var view = new SelectPair();
                    view.DataContext = viewModel;

                    viewModel.CancelCommand = new RelayCommand((pms) =>
                        {
                            view.Close();
                        });

                    viewModel.OkCommand = new RelayCommand((pms) =>
                        {
                            if (viewModel.SelectedPair != null)
                            {
                                DepthViewModels.Add(new AvalonDockDepthViewModel(viewModel.SelectedPair, btceModels, configuration, btceTradeApi));
                                view.Close();
                            }
                        });

                    view.ShowDialog();
                });
        }
    }
}
