using System.Collections.Generic;
using System.Configuration;

namespace BTCE_Trader.Api.Configurations
{
    public class Configuration : IConfiguration
    {
        private string publicKey { get; set; }
        private string secretKey { get; set; }
        private List<BtcePairEnum> pairs { get; set; }

        public string PublicKey { get { return publicKey; } }
        public string SecretKey { get { return secretKey; } }
        public List<BtcePairEnum> Pairs { get { return pairs; } }
        public Dictionary<BtcePairEnum, decimal> PairAggregatorIncrement { get; set; }

        public Configuration()
        {
            publicKey = ConfigurationManager.AppSettings["btcePublicKey"];
            secretKey = ConfigurationManager.AppSettings["btceSecretKey"];
            pairs = new List<BtcePairEnum>();
            PairAggregatorIncrement = new Dictionary<BtcePairEnum, decimal>();
            
            var pairsStrings = ConfigurationManager.AppSettings["Pairs"].Split(new[] {','});
            foreach (var pairsString in pairsStrings)
                pairs.Add(BtcePairHelper.FromString(pairsString));

            PairAggregatorIncrement.Add(BtcePairEnum.btc_usd, 1);
            PairAggregatorIncrement.Add(BtcePairEnum.ltc_usd, 0.1m);
            PairAggregatorIncrement.Add(BtcePairEnum.nmc_usd, 0.1m);
            PairAggregatorIncrement.Add(BtcePairEnum.ltc_btc, 0.0001m);

        }
    }
}
