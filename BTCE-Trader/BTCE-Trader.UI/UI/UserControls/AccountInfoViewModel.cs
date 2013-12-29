using BTCE_Trader.Api;
using BTCE_Trader.Api.Info;

namespace BTCE_Trader.UI.UI.UserControls
{
    public class AccountInfoViewModel : BaseViewModel
    {
        private IBtceModels BtceModels;
        public IAccountInfo AccountInfo {get { return BtceModels.AccountInfo; }}

        public AccountInfoViewModel(IBtceModels btceModels)
        {
            BtceModels = btceModels;
            BtceModels.AccountInfoUpdated += BtceModels_AccountInfoUpdated;
        }

        void BtceModels_AccountInfoUpdated(object sender, System.EventArgs e)
        {
            OnPropertyChanged("AccountInfo");
        }
    }
}
