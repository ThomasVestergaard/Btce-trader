using System.Collections.Generic;
using System.Windows.Input;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Configurations;

namespace BTCE_Trader.UI.UI.Dialogs
{
    public class SelectPairViewModel
    {
        private readonly IConfiguration configuration;
        public List<BtcePairEnum> AvailablePairs { get { return configuration.Pairs; }}
        public BtcePairEnum SelectedPair { get; set; }

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public SelectPairViewModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
