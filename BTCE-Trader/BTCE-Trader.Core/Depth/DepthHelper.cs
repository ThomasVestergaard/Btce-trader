using System;
using System.Linq;
using System.Collections.Generic;
using BTCE_Trader.Api.Depth;


namespace BTCE_Trader.Core.Depth
{
    public class DepthHelper
    {
        public static List<IDepthOrderInfo> GetAggregatedAskOrderList(List<IDepthOrderInfo> fullOrderList, decimal increment)
        {
            var toReturn = new List<IDepthOrderInfo>();
            decimal lowestValue = Math.Round(fullOrderList.OrderBy(a => a.Price).First().Price, 4);

            decimal counter = lowestValue;
            decimal amountSum = 0;
            while (counter < lowestValue + (15 * increment))
            {
                amountSum += fullOrderList.FindAll(a => a.Price >= counter && a.Price < counter + increment).Sum(b => b.Amount);
                var interval = new DepthOrderInfo
                    {
                        Amount = Math.Round(amountSum),
                        Price = counter
                    };

                toReturn.Add(interval);
                counter += increment;
            }
            
            return toReturn;
        }

        public static List<IDepthOrderInfo> GetAggregatedBidOrderList(List<IDepthOrderInfo> fullOrderList, decimal increment)
        {
            var toReturn = new List<IDepthOrderInfo>();
            decimal highestValue = Math.Round(fullOrderList.OrderByDescending(a => a.Price).First().Price, 4);

            decimal counter = highestValue;
            decimal amountSum = 0;
            while (counter > highestValue - (15 * increment))
            {
                amountSum += fullOrderList.FindAll(a => a.Price >= counter && a.Price < counter + increment).Sum(b => b.Amount);
                var interval = new DepthOrderInfo
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
