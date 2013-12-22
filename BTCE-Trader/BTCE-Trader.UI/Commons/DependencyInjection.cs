using System.Configuration;
using BtcE;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.Api.Web;
using BTCE_Trader.Core.Depth;
using BTCE_Trader.Core.Orders;
using BTCE_Trader.UI.UI;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace BTCE_Trader.UI.Commons
{
    public class DependencyInjection
    {
        public IWindsorContainer Container { get; private set; }
        private BtceApi btceApiInstance { get; set; }
        
        public void Init()
        {
            btceApiInstance = new BtceApi(ConfigurationManager.AppSettings["btcePublicKey"], ConfigurationManager.AppSettings["btceSecretKey"]);

            Container = new WindsorContainer();
            Container.Register(Component.For<IWindsorContainer>().Instance(Container));
            Container.Register(Component.For<IDepthAgent>().ImplementedBy<DepthAgent>().LifestyleSingleton());
            Container.Register(Component.For<IActiveOrderAgent>().ImplementedBy<ActiveOrderAgent>().LifestyleSingleton());
            Container.Register(Component.For<DepthWindowViewModel>().ImplementedBy<DepthWindowViewModel>().LifestyleTransient());
            Container.Register(Component.For<BtceApi>().Instance(btceApiInstance));
            Container.Register(Component.For<IConfiguration>().ImplementedBy<BTCE_Trader.Api.Configurations.Configuration>().LifestyleSingleton());
            Container.Register(Component.For<IWebRequestWrapper>().ImplementedBy<WebRequestWrapper>().LifestyleTransient());
            

        }
    }
}
