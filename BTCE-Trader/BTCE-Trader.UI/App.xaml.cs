using System.Windows;
using BTCE_Trader.Core.Depth;
using BTCE_Trader.UI.Commons;
using BTCE_Trader.UI.UI;
using BtcE;
using System.Collections.Generic;
using BTCE_Trader.Core.Orders;

namespace BTCE_Trader.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DependencyInjection dependencyInjection;


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            dependencyInjection = new DependencyInjection();
            dependencyInjection.Init();

            var pairs = new List<BtcePair>();
            pairs.Add(BtcePair.ltc_btc);
            pairs.Add(BtcePair.btc_usd);
            pairs.Add(BtcePair.ltc_usd);

            dependencyInjection.Container.Resolve<IDepthAgent>().Start(500, pairs);
            dependencyInjection.Container.Resolve<IActiveOrderAgent>().Start(500);
            Dispatcher.Invoke(Start);
        }

        private void Start()
        {
            var depthView = new DepthWindow() { DataContext = dependencyInjection.Container.Resolve<DepthWindowViewModel>() };
            depthView.Show();
        }


        private void Application_Exit(object sender, ExitEventArgs e)
        {
            dependencyInjection.Container.Resolve<IDepthAgent>().Stop();
            dependencyInjection.Container.Resolve<IActiveOrderAgent>().Stop();
        }
    }
}

