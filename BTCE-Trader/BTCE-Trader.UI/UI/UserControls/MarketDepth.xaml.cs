using System.Windows.Controls;
using BTCE_Trader.Api;

namespace BTCE_Trader.UI.UI.UserControls
{
    /// <summary>
    /// Interaction logic for MarketDepth.xaml
    /// </summary>
    public partial class MarketDepth : UserControl
    {
        public BtcePairEnum Pair { get; set; }
    
        public MarketDepth()
        {
            InitializeComponent();
            
        }
    }
}
