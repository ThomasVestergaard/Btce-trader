using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Core.Orders
{
    public interface IActiveOrderAgent
    {
        event ActiveOrderAgent.ActiveOrdersUpdatedDelegate ActiveOrdersUpdated;
        void Start(int updateInterval);
        void Stop();
    }
}
