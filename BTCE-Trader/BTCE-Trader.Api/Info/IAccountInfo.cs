namespace BTCE_Trader.Api.Info
{
    public interface IAccountInfo
    {
        decimal UsdAmount { get; set; }
        decimal BtcAmount { get; set; }
        decimal LtcAmount { get; set; }
        decimal NmcAmount { get; set; }
        decimal RurAmount { get; set; }
        decimal EurAmount { get; set; }
        decimal NvcAmount { get; set; }
        decimal TrcAmount { get; set; }
        decimal PpcAmount { get; set; }
        decimal FtcAmount { get; set; }
        decimal XpmAmount { get; set; }

        decimal GetAmountFromEnum(BtcePairEnum Pair);

    }
}
