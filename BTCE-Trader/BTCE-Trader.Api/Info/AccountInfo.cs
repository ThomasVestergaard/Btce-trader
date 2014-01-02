namespace BTCE_Trader.Api.Info
{
    class AccountInfo : IAccountInfo
    {
        public decimal UsdAmount { get; set; }
        public decimal BtcAmount { get; set; }
        public decimal LtcAmount { get; set; }
        public decimal NmcAmount { get; set; }
        public decimal RurAmount { get; set; }
        public decimal EurAmount { get; set; }
        public decimal NvcAmount { get; set; }
        public decimal TrcAmount { get; set; }
        public decimal PpcAmount { get; set; }
        public decimal FtcAmount { get; set; }
        public decimal XpmAmount { get; set; }

        public decimal GetAmountFromEnum(BtcePairEnum Pair)
        {
            switch (Pair)
            {
                case BtcePairEnum.ltc_usd:
                    return LtcAmount;
                    break;

                case BtcePairEnum.nmc_usd:
                    return NmcAmount;
                    break;

                case BtcePairEnum.btc_usd:
                    return BtcAmount;
                    break;

                case BtcePairEnum.ftc_btc:
                    return FtcAmount;
                    break;

                default:
                    return 1;
                    break;
            }
        }
    }
}