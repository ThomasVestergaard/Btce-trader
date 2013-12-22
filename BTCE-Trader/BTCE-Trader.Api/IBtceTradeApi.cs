using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTCE_Trader.Api.Orders;

namespace BTCE_Trader.Api
{
    public interface IBtceTradeApi
    {
        List<IOrder> GetActiveOrders();
    }
}
