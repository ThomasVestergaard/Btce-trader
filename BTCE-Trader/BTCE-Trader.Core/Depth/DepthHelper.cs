using System;
using System.Linq;
using System.Collections.Generic;
using BtcE;

namespace BTCE_Trader.Core.Depth
{
    public class DepthHelper
    {
        public static List<OrderInfo> GetAggregatedOrderList(List<OrderInfo> fullOrderList, decimal increment)
        {
            var toReturn = new List<OrderInfo>();

            decimal lowestValue = Math.Floor(fullOrderList.OrderBy(a => a.Price).First().Price);
            decimal counter = lowestValue;
            while (counter < lowestValue + 15)
            {
                var interval = new OrderInfo
                    {
                        Amount = fullOrderList.FindAll(a => a.Price >= counter && a.Price < counter + increment).Sum(b => b.Amount),
                        Price = counter
                    };

                toReturn.Add(interval);
                counter += increment;
            }
            
            return toReturn;
        }

    }
}
