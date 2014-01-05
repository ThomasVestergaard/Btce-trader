using BTCE_Trader.Api;
using BTCE_Trader.Api.Configurations;
using BTCE_Trader.Api.RequestQueue;
using BTCE_Trader.Api.Web;
using BTCE_Trader.UI.Configurations;
using BTCE_Trader.UI.UpdateAgents.AccountInfo;
using BTCE_Trader.UI.UpdateAgents.Depth;
using BTCE_Trader.UI.UpdateAgents.MarketTrades;
using BTCE_Trader.UI.UpdateAgents.Trade;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Disruptor;
using ActiveOrderAgent = BTCE_Trader.UI.UpdateAgents.Orders.ActiveOrderAgent;
using IActiveOrderAgent = BTCE_Trader.UI.UpdateAgents.Orders.IActiveOrderAgent;

namespace BTCE_Trader.UI.Commons
{
    public class DependencyInjection
    {
        public IWindsorContainer Container { get; private set; }
        private IEventHandler<OutputQueueItem>[] outputEventHandlers;
        
        public void Init()
        {
            Container = new WindsorContainer();
            Container.Register(Component.For<IWindsorContainer>().Instance(Container));
            Container.Register(Component.For<IDepthAgent>().ImplementedBy<DepthAgent>().LifestyleSingleton());
            Container.Register(Component.For<IActiveOrderAgent>().ImplementedBy<ActiveOrderAgent>().LifestyleSingleton());
            Container.Register(Component.For<IAccountInfoAgent>().ImplementedBy<AccountInfoAgent>().LifestyleSingleton());
            Container.Register(Component.For<IMarketTradesAgent>().ImplementedBy<MarketTradesAgent>().LifestyleSingleton());
            Container.Register(Component.For<IBtceTradeApi>().ImplementedBy<BtceTradeApi>().LifestyleSingleton());
            Container.Register(Component.For<IConfiguration>().ImplementedBy<Configuration>().LifestyleSingleton());
            Container.Register(Component.For<ITradeAgent>().ImplementedBy<TradeAgent>().LifestyleSingleton());
            Container.Register(Component.For<IWebRequestWrapper>().ImplementedBy<WebRequestWrapper>().LifestyleSingleton());
            Container.Register(Component.For<IInputRequestHandler>().ImplementedBy<InputRequestHandler>().LifestyleSingleton());
            Container.Register(Component.For<IRequestInputQueue>().ImplementedBy<RequestInputQueue>().LifestyleSingleton());
            Container.Register(Component.For<IRequestOutputQueue>().ImplementedBy<RequestOutputQueue>().LifestyleSingleton());
            Container.Register(Component.For<IBtceModels>().ImplementedBy<BtceModels>().LifestyleSingleton());
            Container.Register(Component.For<MainWindowViewModel>().ImplementedBy<MainWindowViewModel>().LifestyleTransient());
            Container.Register(Component.For<ITradingConfigurations>().ImplementedBy<TradingConfigurations>().LifestyleTransient());

            outputEventHandlers = new IEventHandler<OutputQueueItem>[1];
            outputEventHandlers[0] = Container.Resolve<IBtceModels>();
            Container.Register(Component.For<IEventHandler<OutputQueueItem>[]>().Instance(outputEventHandlers));



        }
    }
}
