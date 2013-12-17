using System;
using System.Linq;
using System.Collections.Generic;
using BtcE;

namespace BTCE_Trader.Core.Depth
{
    public class DepthHelper
    {
        public static List<OrderInfo> GetAggregatedAskOrderList(List<OrderInfo> fullOrderList, decimal increment)
        {
            var toReturn = new List<OrderInfo>();
            decimal lowestValue = Math.Round(fullOrderList.OrderBy(a => a.Price).First().Price, 4);

            decimal counter = lowestValue;
            decimal amountSum = 0;
            while (counter < lowestValue + (15 * increment))
            {
                amountSum += fullOrderList.FindAll(a => a.Price >= counter && a.Price < counter + increment).Sum(b => b.Amount);
                var interval = new OrderInfo
                    {
                        Amount = Math.Round(amountSum),
                        Price = counter
                    };

                toReturn.Add(interval);
                counter += increment;
            }
            
            return toReturn;
        }

        public static List<OrderInfo> GetAggregatedBidOrderList(List<OrderInfo> fullOrderList, decimal increment)
        {
            var toReturn = new List<OrderInfo>();
            decimal highestValue = Math.Round(fullOrderList.OrderByDescending(a => a.Price).First().Price, 4);

            decimal counter = highestValue;
            decimal amountSum = 0;
            while (counter > highestValue - (15 * increment))
            {
                amountSum += fullOrderList.FindAll(a => a.Price >= counter && a.Price < counter + increment).Sum(b => b.Amount);
                var interval = new OrderInfo
                {
                    Amount = Math.Round(amountSum),
                    Price = counter
                };

                toReturn.Add(interval);
                counter -= increment;
            }

            return toReturn;
        }

    }
}
