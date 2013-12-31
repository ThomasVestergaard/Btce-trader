using System;

namespace BTCE_Trader.Api
{
    public enum BtcePairEnum
    {
        btc_usd,
        btc_rur,
        btc_eur,
        ltc_btc,
        ltc_usd,
        ltc_rur,
        nmc_btc,
        nmc_usd,
        nvc_btc,
        usd_rur,
        eur_usd,
        trc_btc,
        ppc_btc,
        ftc_btc,
        Unknown
    }

    public class BtcePairHelper
    {
        public static BtcePairEnum FromString(string s)
        {
            BtcePairEnum ret = BtcePairEnum.Unknown;
            
            if (!Enum.TryParse<BtcePairEnum>(s.ToLowerInvariant(), out ret))
                throw new InvalidOperationException(string.Format("Cannot parse {0} to BtcePairEnum", s));
            
            
            return ret;
        }
        public static string ToString(BtcePairEnum v)
        {
            return Enum.GetName(typeof(BtcePairEnum), v).ToLowerInvariant();
        }
    }
}
