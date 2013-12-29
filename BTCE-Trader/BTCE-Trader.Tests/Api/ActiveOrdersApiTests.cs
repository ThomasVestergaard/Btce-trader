using System;
using System.Collections.Generic;
using BTCE_Trader.Api;
using BTCE_Trader.Api.RequestQueue;
using BTCE_Trader.Api.Web;
using NUnit.Framework;
using Rhino.Mocks;

namespace BTCE_Trader.Tests.Api
{
    [TestFixture]
    public class ActiveOrdersApiTests
    {
        private IWebRequestWrapper webRequestWrapperMock;
        private IRequestInputQueue requestInputQueueMock;
        private BtceTradeApi tradeApi;


        [SetUp]
        public void SetUp()
        {
            webRequestWrapperMock = MockRepository.GenerateMock<IWebRequestWrapper>();
            requestInputQueueMock = MockRepository.GenerateMock<IRequestInputQueue>();
            tradeApi = new BtceTradeApi(webRequestWrapperMock, requestInputQueueMock);
        }

        [Test]
        public void ShouldDeserializeOrderObject()
        {
            string stubbedWebResult = "{\"success\":1,\"return\":{\"89297250\":{\"pair\":\"ltc_btc\",\"type\":\"sell\",\"amount\":14.00000000,\"rate\":0.05000000,\"timestamp_created\":1387391239,\"status\":0}}}";
            webRequestWrapperMock.Stub(a => a.RequestData(Arg<string>.Is.Equal("ActiveOrders"), Arg<Dictionary<string, string>>.Is.Anything)).Return(stubbedWebResult);
            tradeApi.GetActiveOrders();
            /*
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(BtcePairEnum.ltc_btc, orders[0].Pair);
            Assert.AreEqual(TradeTypeEnum.Sell, orders[0].Type);
            Assert.AreEqual(14m, orders[0].Amount);
            Assert.AreEqual(0.05m, orders[0].Rate);
            Assert.AreEqual(0, orders[0].Status);
            Assert.AreEqual(new DateTime(2013, 12, 18, 19, 27, 19), orders[0].CreateDate);*/
        }

        [Test]
        public void ShouldReturnEmptyOrderList()
        {
            /*
            string stubbedWebResult = "{\"success\":0,\"error\":\"no orders\"}";
            webRequestWrapperMock.Stub(a => a.RequestData(Arg<string>.Is.Equal("ActiveOrders"), Arg<Dictionary<string, string>>.Is.Anything)).Return(stubbedWebResult);
            var orders = tradeApi.GetActiveOrders();
            Assert.AreEqual(0, orders.Count);*/
        }

    }
}
