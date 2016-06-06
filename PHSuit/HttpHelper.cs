using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PHSuit
{
    public class HttpHelper
    {
        public static string CreateHttpRequest(string url, string type, NameValueCollection paras)
        {
            var responseString = string.Empty;
            using (var wb = new WebClient())
            {
                wb.Encoding = Encoding.UTF8;
                switch (type.ToLower())
                {
                    case "get":
                        responseString = wb.DownloadString(url);
                        break;
                    case "post":
                        byte[] response = wb.UploadValues(url, "POST", paras);
                        responseString = Encoding.UTF8.GetString(response);
                        break;                    
                    default:
                        throw new Exception("Unsupported method");
                }
            }
            return responseString;
        }

        public static string CreateHttpRequest(string url, string type, NameValueCollection paras,Encoding code)
        {
            var responseString = string.Empty;
            using (var wb = new WebClient())
            {
                wb.Encoding = code;
                switch (type.ToLower())
                {
                    case "get":
                        responseString = wb.DownloadString(url);
                        break;
                    case "post":
                        byte[] response = wb.UploadValues(url, "POST", paras);
                        responseString = code.GetString(response);
                        break;
                    default:
                        throw new Exception("Unsupported method");
                }
            }
            return responseString;
        }
        public static string CreateHttpRequestPostXml(string url, string parasXml)
        {
            return CreateHttpRequestPostXml(url, parasXml, new WebHeaderCollection());
        }
        public static string CreateHttpRequestPostXml(string url, string parasXml,WebHeaderCollection headers)
        {
            var responseString = string.Empty;
            using (var wb = new WebClient())
            {
                wb.Headers = headers;
                wb.Encoding = Encoding.UTF8;

                responseString = wb.UploadString(url, "POST", parasXml);
            }

            return responseString;

        }

        public static string CreateHttpRequestPostXml(string url,string requestXml, string certName)
        {
            try
            {
                string strHtml = string.Empty;

                X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection coll = store.Certificates.Find(X509FindType.FindBySubjectName, certName, false);
                if (coll.Count <= 0)
                {
                    throw new Exception("未找到证书");
                }

                X509Certificate2 cert = coll[0];

                byte[] bytes;
                bytes = System.Text.Encoding.UTF8.GetBytes(requestXml);

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                X509Certificate cer = new X509Certificate(cert);

                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.ClientCertificates.Add(cer);
                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.ContentLength = bytes.Length;
                webrequest.Method = "post";
                webrequest.KeepAlive = true;

                using (Stream reqStream = webrequest.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                    reqStream.Close();
                }
                HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse();
                Stream stream = webreponse.GetResponseStream();
                string resp = string.Empty;
                using (StreamReader reader = new StreamReader(stream))
                {
                    resp = reader.ReadToEnd();
                    strHtml = resp;
                }

                return strHtml;
            }
            catch (Exception exp)
            {
                return exp.ToString();
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

    }
}
