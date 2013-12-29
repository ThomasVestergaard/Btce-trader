using System.Windows;
using BTCE_Trader.Api;
using BTCE_Trader.Api.RequestQueue;
using BTCE_Trader.UI.Commons;
using System.Collections.Generic;
using BTCE_Trader.UI.UpdateAgents.Orders;


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

            var pairs = new List<BtcePairEnum>();
            pairs.Add(BtcePairEnum.ltc_btc);
            pairs.Add(BtcePairEnum.btc_usd);
            pairs.Add(BtcePairEnum.ltc_usd);

            dependencyInjection.Container.Resolve<IRequestInputQueue>().Start();
            dependencyInjection.Container.Resolve<IRequestOutputQueue>().Start();

            //dependencyInjection.Container.Resolve<IDepthAgent>().Start(900, pairs);
            //dependencyInjection.Container.Resolve<IAccountInfoAgent>().Start(1000);
            dependencyInjection.Container.Resolve<IActiveOrderAgent>().Start(1000);

            Dispatcher.Invoke(Start);
        }

        private void Start()
        {
            UiThread.Init(Dispatcher);
            var mainWindow = new MainWindow() { DataContext = dependencyInjection.Container.Resolve<MainWindowViewModel>() };
            mainWindow.Show();
        }


        private void Application_Exit(object sender, ExitEventArgs e)
        {
            dependencyInjection.Container.Resolve<IRequestInputQueue>().Stop();
            dependencyInjection.Container.Resolve<IRequestOutputQueue>().Stop();
            //dependencyInjection.Container.Resolve<IDepthAgent>().Stop();
            dependencyInjection.Container.Resolve<IActiveOrderAgent>().Stop();
            //dependencyInjection.Container.Resolve<IAccountInfoAgent>().Stop();
        }
    }
}

