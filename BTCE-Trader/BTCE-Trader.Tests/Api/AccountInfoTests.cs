using System.Collections.Generic;
using BTCE_Trader.Api;
using BTCE_Trader.Api.Web;
using NUnit.Framework;
using Rhino.Mocks;

namespace BTCE_Trader.Tests.Api
{
    [TestFixture]
    public class AccountInfoTests
    {
        private IWebRequestWrapper webRequestWrapperMock;
        private IBtceTradeApi btceTradeApi;

        [SetUp]
        public void SetUp()
        {
            webRequestWrapperMock = MockRepository.GenerateMock<IWebRequestWrapper>();
            btceTradeApi = new BtceTradeApi(webRequestWrapperMock);
        }

        [Test]
        public void ShouldDeserializeAccountInfo()
        {
            string staticWebResult = "{\"success\":1,\"return\":{\"funds\":{\"usd\":1.1,\"btc\":2.2,\"ltc\":3.3,\"nmc\":4.4,\"rur\":5.5,\"eur\":6.6,\"nvc\":7.7,\"trc\":8.8,\"ppc\":9.9,\"ftc\":10.10,\"xpm\":11.11},\"rights\":{\"info\":1,\"trade\":1,\"withdraw\":0},\"transaction_count\":48,\"open_orders\":0,\"server_time\":1388086457}}";
            webRequestWrapperMock.Stub(a => a.RequestData(Arg<string>.Is.Equal("getInfo"), Arg<Dictionary<string, string>>.Is.Anything)).Return(staticWebResult);

            var accountInfo = btceTradeApi.GetAccountInfo();
            Assert.IsNotNull(accountInfo);
            Assert.AreEqual(1.1m, accountInfo.UsdAmount);
            Assert.AreEqual(2.2m, accountInfo.BtcAmount);
            Assert.AreEqual(3.3m, accountInfo.LtcAmount);
            Assert.AreEqual(4.4m, accountInfo.NmcAmount);
            Assert.AreEqual(5.5m, accountInfo.RurAmount);
            Assert.AreEqual(6.6m, accountInfo.EurAmount);
            Assert.AreEqual(7.7m, accountInfo.NvcAmount);
            Assert.AreEqual(8.8m, accountInfo.TrcAmount);
            Assert.AreEqual(9.9m, accountInfo.PpcAmount);
            Assert.AreEqual(10.10m, accountInfo.FtcAmount);
            Assert.AreEqual(11.11m, accountInfo.XpmAmount);

        }
    }
}
