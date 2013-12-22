using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Api.Configurations
{
    public interface IConfiguration
    {
        string PublicKey { get; }
        string SecretKey { get; }
    }
}
