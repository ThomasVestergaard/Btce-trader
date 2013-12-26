using BTCE_Trader.Api.Info;
using BTCE_Trader.Core.AccountInfo;

namespace BTCE_Trader.UI.UI.UserControls
{
    public class AccountInfoViewModel
    {
        private readonly IAccountInfoAgent accountInfoAgent;

        private IAccountInfo accountInfo { get; set; }
        public IAccountInfo AccountInfo
        {
            get { return accountInfo; }
        }

        public AccountInfoViewModel(IAccountInfoAgent accountInfoAgent)
        {
            this.accountInfoAgent = accountInfoAgent;
            
        }
    }
}
