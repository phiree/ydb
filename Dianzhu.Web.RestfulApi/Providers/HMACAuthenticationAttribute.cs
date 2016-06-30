using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Dianzhu.Model;
using Dianzhu.BLL;

namespace Dianzhu.Web.RestfulApi
{
    public class HMACAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private static Dictionary<string, string> allowedApps = new Dictionary<string, string>();
        private readonly UInt64 requestMaxAgeInSeconds = 3000;  //5 mins
        private readonly string authenticationScheme = "amx";
        /// <summary>
        /// 设定appName和security_key
        /// </summary>
        public HMACAuthenticationAttribute()
        {
            if (allowedApps.Count == 0)
            {
                allowedApps.Add("UI3f4185e97b3E4a4496594eA3b904d60d", "NoJBn3npJIvre2fC2SQL5aQGNB/3l73XXSqNZYdY6HU=");//IOS用户版 IOS_User
                allowedApps.Add("MI354d5aaa55Ff42fba7716C4e70e015f2", "h7lVzFNKU5Nlp7iCSVIyfs2bEgCzA2aFnQsJwia8utE=");//IOS商户版 IOS_Merchant
                allowedApps.Add("CI5baFa6180f5d4b9D85026073884c3566", "Ce6QgbBcwFxbB9yCAI5BEJ95L7RJi8AeQ9REYxvp79Q=");//IOS客服版 IOS_CustomerService
                allowedApps.Add("UA811Cd5343a1a41e4beB35227868541f8", "WDcajjuVXA6TToFfm1MWhFFgn6bsXTt8VNsGLjcqGMg=");//Android用户版 Android_User
                allowedApps.Add("MAA6096436548346B0b70ffb58A9b0426d", "3xhBie885/2f6dWg4O5rh7bUpcsgldeQxnwsx6f9638=");//Android商户版 Android_Merchant
                allowedApps.Add("CA660838f88147463CAF3a52bae6c30cbd", "suSjG+pPCu0gwXOqamNdp0zE3sY29vcHJHe1S429hNU=");//Android客户版 Android_CustomerService
                allowedApps.Add("JS1adBF8cbaf594d1ab2f1A68755e70440", "FJXTdZVLhmFLHO5M3Xweo5kHRmLH3qFdRzLyGFZLeBc=");//js客户端
            }
        }

        public string getAllowedApps(string appName)
        {
            return allowedApps[appName];
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var req = context.Request;
            bool b = true;
            string stamp_TIMES = "";
            string appName = "";
            string token = "";
            string sign = "";
            IEnumerable<string> keyValue = null;
            if (req.Headers.TryGetValues("stamp_TIMES", out keyValue))
            {
                stamp_TIMES = keyValue.FirstOrDefault();
            }
            else
            {
                b = false;
            }
            if (req.Headers.TryGetValues("appName", out keyValue))
            {
                appName = keyValue.FirstOrDefault();
            }
            else
            {
                b = false;
            }
            if (req.Headers.TryGetValues("token", out keyValue))
            {
                token = keyValue.FirstOrDefault();
            }
            if (req.Headers.TryGetValues("sign", out keyValue))
            {
                sign = keyValue.FirstOrDefault();
            }
            else
            {
                b = false;
            }
            if (b)
            {
                var isValid = isValidRequest(req, appName, sign, token, stamp_TIMES);
                if (isValid.Result)
                {
                    var currentPrincipal = new GenericPrincipal(new GenericIdentity(appName), null);
                    context.Principal = currentPrincipal;
                }
                else
                {
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                }
            }
            else
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// 认证失败结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            return Task.FromResult(0);
        }

        public bool AllowMultiple
        {
            get { return false; }
        }

        /// <summary>
        /// 认证判断
        /// </summary>
        /// <param name="req"></param>
        /// <param name="appName"></param>
        /// <param name="sign"></param>
        /// <param name="token"></param>
        /// <param name="stamp_TIMES"></param>
        /// <returns></returns>
        private async Task<bool> isValidRequest(HttpRequestMessage req, string appName, string sign, string token, string stamp_TIMES)
        {
            string requestContentBase64String = "";
            string requestUri =HttpUtility.UrlEncode(req.RequestUri.AbsoluteUri.ToLower());
            string requestHttpMethod = req.Method.Method;
            if (!allowedApps.ContainsKey(appName))
            {
                return false;
            }
            var sharedKey = allowedApps[appName];
            if (isReplayRequest(sign, stamp_TIMES))
            {
                return false;
            }
            if (req.Method==HttpMethod.Post && (req.RequestUri.AbsolutePath.ToLower() != "/api/token" || req.RequestUri.AbsolutePath.ToLower() != "/api/customers" || req.RequestUri.AbsolutePath.ToLower() != "/api/merchants"))
            {
                if (!System.Runtime.Caching.MemoryCache.Default.Contains(token))
                {
                    BLL.Client.BLLUserToken bllusertoken = Bootstrap.Container.Resolve<BLL.Client.BLLUserToken>();
                    //NHibernateUnitOfWork.UnitOfWork.Start();
                    if (bllusertoken.CheckToken(token))
                    {
                        //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                        return false;
                    }
                    //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                }
            }
            byte[] hash = await ComputeHash(req.Content);
            if (hash != null)
            {
                requestContentBase64String = Convert.ToBase64String(hash);
            }
            string data = String.Format("{0}{1}{2}{3}{4}", appName, token, requestContentBase64String, stamp_TIMES, requestUri);
            var secretKeyBytes = Convert.FromBase64String(sharedKey);
            byte[] signature = Encoding.UTF8.GetBytes(data);
            using (HMACSHA256 hmac = new HMACSHA256(secretKeyBytes))
            {
                byte[] signatureBytes = hmac.ComputeHash(signature);
                string tt = Convert.ToBase64String(signatureBytes);
                return (sign.Equals(tt, StringComparison.Ordinal));
            }

        }

        /// <summary>
        /// 缓存防止ReplayRequest
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="requestTimeStamp"></param>
        /// <returns></returns>
        private bool isReplayRequest(string nonce, string requestTimeStamp)
        {
            if (System.Runtime.Caching.MemoryCache.Default.Contains(nonce))
            {
                return true;
            }
            //unixtime防止跨时区调用
            //DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            //TimeSpan currentTs = DateTime.UtcNow - epochStart;
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Local);
            TimeSpan currentTs = DateTime.Now - epochStart;
            var serverTotalSeconds = Convert.ToUInt64(currentTs.TotalSeconds);
            var requestTotalSeconds = Convert.ToUInt64(requestTimeStamp);
            if ((serverTotalSeconds - requestTotalSeconds) > requestMaxAgeInSeconds)
            {
                return true;
            }
            System.Runtime.Caching.MemoryCache.Default.Add(nonce, requestTimeStamp, DateTimeOffset.UtcNow.AddSeconds(requestMaxAgeInSeconds));
            return false;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        private static async Task<byte[]> ComputeHash(HttpContent httpContent)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = null;
                var content = await httpContent.ReadAsByteArrayAsync();
                //string str = Encoding.Default.GetString(content);
                //string signatureRawData = await httpContent.ReadAsStringAsync();
                //byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);
                if (content.Length != 0)
                {
                    hash = md5.ComputeHash(content);
                }
                return hash;
            }
        }
    }

    
    public class ResultWithChallenge : IHttpActionResult
    {
        private readonly string authenticationScheme = "amx";
        private readonly IHttpActionResult next;

        public ResultWithChallenge(IHttpActionResult next)
        {
            this.next = next;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await next.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(authenticationScheme));
            }

            return response;
        }
    }
}