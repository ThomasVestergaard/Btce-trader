using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BTCE_Trader.Api.Configurations;

namespace BTCE_Trader.Api.Web
{
    public class WebRequestWrapper : IWebRequestWrapper
    {
        private WebRequest webRequest { get; set; }
        private IConfiguration configuration { get; set; }
        private HMACSHA512 keyHasher { get; set; }
        private long requestSequenceNumber
        {
            get
            {
                return (long)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            }
        }

        public WebRequestWrapper(IConfiguration configuration)
        {
            this.configuration = configuration;
            keyHasher = new HMACSHA512(Encoding.ASCII.GetBytes(configuration.SecretKey));
        }

        public string RequestData(string method, Dictionary<string, string> postArguments)
        {
            postArguments.Add("method", method);
            postArguments.Add("nonce", requestSequenceNumber.ToString());
            var dataStr = BuildPostData(postArguments);
            var data = Encoding.ASCII.GetBytes(dataStr);

            var request = WebRequest.Create(new Uri("https://btc-e.com/tapi")) as HttpWebRequest;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");

            request.Method = "POST";
            request.Timeout = 15000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            request.Headers.Add("Key", configuration.PublicKey);
            request.Headers.Add("Sign", Encoding.UTF8.GetString(keyHasher.ComputeHash(data)).ToLower());
            var reqStream = request.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();

            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }

        private string BuildPostData(Dictionary<string, string> arguments)
        {
            StringBuilder s = new StringBuilder();
            foreach (var item in arguments)
            {
                s.AppendFormat("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value));
                s.Append("&");
            }
            if (s.Length > 0) s.Remove(s.Length - 1, 1);
            return s.ToString();
        }

    }
}
