using System.Collections.Generic;

namespace BTCE_Trader.Api.Configurations
{
    public interface IConfiguration
    {
        string PublicKey { get; }
        string SecretKey { get; }
        List<BtcePairEnum> Pairs { get; }

        Dictionary<BtcePairEnum, decimal> PairAggregatorIncrement { get; set; }
    }
}
