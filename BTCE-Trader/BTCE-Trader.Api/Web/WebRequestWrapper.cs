﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using BTCE_Trader.Api.Configurations;
using log4net;

namespace BTCE_Trader.Api.Web
{
    public class WebRequestWrapper : IWebRequestWrapper
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WebRequestWrapper));

        private IConfiguration configuration { get; set; }
        private HMACSHA512 keyHasher { get; set; }
        private long sequenceIncrementer;
        private long requestSequenceNumber
        {
            get
            {
                long seq = (long) (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + sequenceIncrementer;
                sequenceIncrementer++;
                return seq;
            }
        }

        public WebRequestWrapper(IConfiguration configuration)
        {
            this.configuration = configuration;
            sequenceIncrementer = 0;
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
            request.Headers.Add("Sign", BitConverter.ToString(keyHasher.ComputeHash(data)).Replace("-", "").ToLower());
            var reqStream = request.GetRequestStream();
            
            reqStream.Write(data, 0, data.Length);
            
            string toReturn = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
            reqStream.Close();
            return toReturn;
        }

        public string RequestV3(string method, string parameters)
        {
            string url = string.Format("https://btc-e.com/api/3/{0}/{1}?ignore_invalid=0", method, parameters);
            var request = WebRequest.Create(url);
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");
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
