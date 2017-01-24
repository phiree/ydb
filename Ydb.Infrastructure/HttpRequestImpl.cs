using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Infrastructure;
using System.Web;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

namespace Ydb.Infrastructure
{  
    public class HttpRequestImpl : IHttpRequest
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.HttpHelper");
        public   string CreateHttpRequest(string url, string type, NameValueCollection paras)
        {
            return CreateHttpRequest(url, type, paras, Encoding.UTF8);
        }

        public   string CreateHttpRequest(string url, string type, NameValueCollection paras, Encoding code)
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
        public   string CreateHttpRequestPostXml(string url, string requestXml, string certName)
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
        private   bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

        public string CreateHttpRequest(string url, string type, string values, string authorizationSecret)
        {
            var responseString = string.Empty;
            using (var wb = new WebClient())
            {
                wb.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=utf-8");

                if (!string.IsNullOrEmpty(authorizationSecret))
                {
                    wb.Headers.Add(HttpRequestHeader.Authorization, authorizationSecret);
                }
                wb.Encoding = Encoding.UTF8;
                switch (type.ToLower())
                {
                    case "get":
                        responseString = wb.DownloadString(url);
                        break;
                    case "post":
                        responseString = wb.UploadString(url, "POST", values);

                        //responseString = Encoding.UTF8.GetString(response);
                        break;
                    default:
                        throw new Exception("Unsupported method");
                }
            }
            return responseString;
        }
        public   string CreateHttpRequestPostXml(string url, string parasXml, WebHeaderCollection headers)
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
    }
}
