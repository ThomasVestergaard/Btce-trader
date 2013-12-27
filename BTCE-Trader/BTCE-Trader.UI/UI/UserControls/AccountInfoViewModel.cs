using System.ComponentModel;
using System.Runtime.CompilerServices;
using BTCE_Trader.Api.Info;
using BTCE_Trader.Core.AccountInfo;
using BTCE_Trader.UI.Annotations;

namespace BTCE_Trader.UI.UI.UserControls
{
    public class AccountInfoViewModel : INotifyPropertyChanged
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
            this.accountInfoAgent.AccountUpdated += accountInfoAgent_AccountUpdated;
            
        }

        void accountInfoAgent_AccountUpdated(IAccountInfo accountInfo)
        {
            this.accountInfo = accountInfo;
            OnPropertyChanged("AccountInfo");
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
