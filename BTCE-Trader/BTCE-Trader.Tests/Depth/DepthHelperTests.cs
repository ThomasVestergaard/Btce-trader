using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BtcE;
using BTCE_Trader.Core.Depth;
using NUnit.Framework;

namespace BTCE_Trader.Tests.Depth
{
    [TestFixture]
    public class DepthHelperTests
    {


        [Test]
        public void ShouldAggregateListOfOrderInfos()
        {
            var fullList = new List<OrderInfo>();
            fullList.Add(new OrderInfo { Price = 12.0001m, Amount = 1 });
            fullList.Add(new OrderInfo { Price = 12.99999m, Amount = 1 });
            fullList.Add(new OrderInfo { Price = 13.499999m, Amount = 2 });
            fullList.Add(new OrderInfo { Price = 13.99999m, Amount = 3 });
            fullList.Add(new OrderInfo { Price = 13.79m, Amount = 0.5m });

            var aggregatedList = DepthHelper.GetAggregatedOrderList(fullList, 0.5m);

            Assert.AreEqual(1, aggregatedList.Find(a => a.Price == 12).Amount);
            Assert.AreEqual(1, aggregatedList.Find(a => a.Price == 12.5m).Amount);
            Assert.AreEqual(2, aggregatedList.Find(a => a.Price == 13).Amount);
            Assert.AreEqual(3.5, aggregatedList.Find(a => a.Price == 13.5m).Amount);

            Assert.AreEqual(fullList.Sum(a => a.Amount), aggregatedList.Sum(b => b.Amount));
        }
    }
}
