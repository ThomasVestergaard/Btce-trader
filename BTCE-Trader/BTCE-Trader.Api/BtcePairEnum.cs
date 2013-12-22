using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Enum.TryParse<BtcePairEnum>(s.ToLowerInvariant(), out ret);
            return ret;
        }
        public static string ToString(BtcePairEnum v)
        {
            return Enum.GetName(typeof(BtcePairEnum), v).ToLowerInvariant();
        }
    }
}
