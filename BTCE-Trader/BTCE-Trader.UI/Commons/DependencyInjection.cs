﻿using BTCE_Trader.Api;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.Api.Web;
using BTCE_Trader.Core.Depth;
using BTCE_Trader.Core.Orders;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace BTCE_Trader.UI.Commons
{
    public class DependencyInjection
    {
        public IWindsorContainer Container { get; private set; }

        
        public void Init()
        {
            Container = new WindsorContainer();
            Container.Register(Component.For<IWindsorContainer>().Instance(Container));
            Container.Register(Component.For<IDepthAgent>().ImplementedBy<DepthAgent>().LifestyleSingleton());
            Container.Register(Component.For<IActiveOrderAgent>().ImplementedBy<ActiveOrderAgent>().LifestyleSingleton());
            Container.Register(Component.For<IBtceTradeApi>().ImplementedBy<BtceTradeApi>().LifestyleSingleton());
            Container.Register(Component.For<IConfiguration>().ImplementedBy<BTCE_Trader.Api.Configurations.Configuration>().LifestyleSingleton());
            Container.Register(Component.For<IWebRequestWrapper>().ImplementedBy<WebRequestWrapper>().LifestyleTransient());
            Container.Register(Component.For<MainWindowViewModel>().ImplementedBy<MainWindowViewModel>().LifestyleTransient());
            

        }
    }
}
