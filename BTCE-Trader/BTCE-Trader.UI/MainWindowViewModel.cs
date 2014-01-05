using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.UI.Commons;
using BTCE_Trader.UI.Configurations;
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
        private readonly ITradingConfigurations tradingConfigurations;
        
        public ActiveOrdersViewModel ActiveOrdersViewModel { get; set; }
        public AccountInfoViewModel AccountInfoViewModel { get; set; }

        public List<BtcePairEnum> AvailablePairs
        {
            get { return configuration.Pairs; }
        }
        public BtcePairEnum SelectedPair { get; set; }

        public ObservableCollection<IAvalonDockViewModel> DockedViewModels { get; set; }

        public ICommand AddDepthCommand { get; set; }
        public ICommand AddMarketMakingOverview { get; set; }

        public MainWindowViewModel(IBtceTradeApi btceTradeApi, IBtceModels btceModels, IConfiguration configuration, ITradingConfigurations tradingConfigurations)
        {
            this.btceTradeApi = btceTradeApi;
            this.btceModels = btceModels;
            this.configuration = configuration;
            this.tradingConfigurations = tradingConfigurations;

            DockedViewModels = new ObservableCollection<IAvalonDockViewModel>();

            
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
                                var newMarketDepthViewModel = new MarketDepthViewModel(btceModels, configuration, btceTradeApi, tradingConfigurations, viewModel.SelectedPair);
                                
                                DockedViewModels.Add(newMarketDepthViewModel);
                                view.Close();
                            }
                        });

                    view.ShowDialog();
                });


    

        }
    }
}
