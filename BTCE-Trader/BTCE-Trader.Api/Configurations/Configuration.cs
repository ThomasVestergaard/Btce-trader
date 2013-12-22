using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BTCE_Trader.Api.Configurations
{
    public class Configuration : IConfiguration
    {
        private string publicKey { get; set; }
        private string secretKey { get; set; }

        public string PublicKey { get { return publicKey; } }
        public string SecretKey { get { return secretKey; } }

        public Configuration()
        {
            publicKey = ConfigurationManager.AppSettings["btcePublicKey"];
            secretKey = ConfigurationManager.AppSettings["btceSecretKey"];
        }
    }
}
