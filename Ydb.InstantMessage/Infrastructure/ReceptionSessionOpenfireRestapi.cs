using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.InstantMessage.DomainModel.Enums;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;

namespace Ydb.InstantMessage.Infrastructure
{
    class ReceptionSessionOpenfireRestapi:IReceptionSession
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.InstantMessage.Infrastructure.ReceptionSessionOpenfireRestapi");
        string restApiUrl, restApiSecretKey;
        public ReceptionSessionOpenfireRestapi(string restApiUrl, string restApiSecretKey)
        {
            this.restApiSecretKey = restApiSecretKey;
            this.restApiUrl = restApiUrl;
        }

        public IList<OnlineUserSession> GetOnlineSessionUser()
        {
            return RequestAPI(API_Sessions);

        }
        public IList<OnlineUserSession> GetOnlineSessionUser(XmppResource xmppResource)
        {
            IList<OnlineUserSession> onlineUsers = GetOnlineSessionUser();
            if (onlineUsers == null)
            {
                return null;
            }
            var filteredByResourceName = onlineUsers.Where(x => x.ressource == xmppResource.ToString());
            return filteredByResourceName.ToList();
        }

        public bool IsUserOnline(string userId)
        {
            var result = RequestAPI(API_Sessions + userId);
            //todo: 
            log.Debug(result);
            return result != null;

        }
        private IList<OnlineUserSession> RequestAPI(string apiName)
        {
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;

            System.Net.WebClient wc = new System.Net.WebClient();

            Uri uri = new Uri(restApiUrl + apiName);
            string host = uri.Host;
            wc.Headers.Add("Authorization:" + restApiSecretKey);
            wc.Headers.Add("Host: " + host);
            wc.Headers.Add("Accept: application/json");
            //System.Threading.Thread.Sleep(2000);
            System.IO.Stream returnData = wc.OpenRead(uri);
            System.IO.StreamReader reader = new System.IO.StreamReader(returnData);
            string result = reader.ReadToEnd();

            try
            {
                OnlineUserSessionResult sessionResult
                    = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<OnlineUserSessionResult>(result);
                return sessionResult.session;
            }
            catch (Exception ex)
            {
                try
                {
                    OnlineUserSessionResult_OnlyOne sessionResult
                       = Newtonsoft.Json.JsonConvert
                       .DeserializeObject<OnlineUserSessionResult_OnlyOne>(result);
                    return new List<OnlineUserSession>(new OnlineUserSession[] { sessionResult.session });
                }
                catch (Exception eex)
                {
                    return null;
                }
            }
        }

        const string API_Sessions = "sessions/";//获取所有用户会话
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
