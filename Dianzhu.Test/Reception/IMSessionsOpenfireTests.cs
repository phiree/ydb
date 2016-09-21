using NUnit.Framework;
using Dianzhu.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Dianzhu.BLL.Tests
{
    [TestFixture()]
    public class IMSessionsOpenfireTests
    {
        [Test()]
        public void GetOnlineSessionUserTest()
        {
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            System.Net.WebClient wc = new System.Net.WebClient();
            Uri uri = new Uri("https://dev.ydban.cn:9091/plugins/restapi/v1/sessions");
            string host = uri.Host;
            wc.Headers.Add("Authorization:an4P0ja6v3rykV4H");
            wc.Headers.Add("Host: " + host);
            wc.Headers.Add("Accept: application/json");
            //System.Threading.Thread.Sleep(2000);
            System.IO.Stream returnData = wc.OpenRead(uri);
            System.IO.StreamReader reader = new System.IO.StreamReader(returnData);
            string result = reader.ReadToEnd();
            Console.WriteLine(result);
        }
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}