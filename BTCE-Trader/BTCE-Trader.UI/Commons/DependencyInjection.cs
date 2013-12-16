using BTCE_Trader.Core.Depth;
using BTCE_Trader.UI.UI;
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
            Container.Register(Component.For<IDepthUpdater>().ImplementedBy<DepthUpdater>().LifestyleSingleton());
            Container.Register(Component.For<DepthWindowViewModel>().ImplementedBy<DepthWindowViewModel>().LifestyleTransient());

        }
    }
}
