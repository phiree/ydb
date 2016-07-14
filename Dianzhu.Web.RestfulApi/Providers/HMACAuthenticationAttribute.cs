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
using System.Configuration;
using System.Web.Configuration;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi
{
    public class HMACAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.Web.RestfulApi.HMACAuthenticationAttribute");
        private static Dictionary<string, string> allowedApps = new Dictionary<string, string>();
        private readonly Int64 requestMaxAgeInSeconds = 300000;  //5 mins
        private readonly string authenticationScheme = "amx";
        /// <summary>
        /// 设定appName和security_key
        /// </summary>
        public HMACAuthenticationAttribute()
        {
            if (allowedApps.Count == 0)
            {
                //allowedApps.Add("UI3f4185e97b3E4a4496594eA3b904d60d", "NoJBn3npJIvre2fC2SQL5aQGNB/3l73XXSqNZYdY6HU=");//IOS用户版 IOS_User
                //allowedApps.Add("MI354d5aaa55Ff42fba7716C4e70e015f2", "h7lVzFNKU5Nlp7iCSVIyfs2bEgCzA2aFnQsJwia8utE=");//IOS商户版 IOS_Merchant
                //allowedApps.Add("CI5baFa6180f5d4b9D85026073884c3566", "Ce6QgbBcwFxbB9yCAI5BEJ95L7RJi8AeQ9REYxvp79Q=");//IOS客服版 IOS_CustomerService
                //allowedApps.Add("UA811Cd5343a1a41e4beB35227868541f8", "WDcajjuVXA6TToFfm1MWhFFgn6bsXTt8VNsGLjcqGMg=");//Android用户版 Android_User
                //allowedApps.Add("MAA6096436548346B0b70ffb58A9b0426d", "3xhBie885/2f6dWg4O5rh7bUpcsgldeQxnwsx6f9638=");//Android商户版 Android_Merchant
                //allowedApps.Add("CA660838f88147463CAF3a52bae6c30cbd", "suSjG+pPCu0gwXOqamNdp0zE3sY29vcHJHe1S429hNU=");//Android客户版 Android_CustomerService
                //allowedApps.Add("JS1adBF8cbaf594d1ab2f1A68755e70440", "FJXTdZVLhmFLHO5M3Xweo5kHRmLH3qFdRzLyGFZLeBc=");//js客户端
                MySectionCollection sectionCollection = (MySectionCollection)ConfigurationManager.GetSection("MySectionCollection");
                //sectionCollection.KeyValues[]
                foreach (MySectionKeyValueSettings kv in sectionCollection.KeyValues.Cast<MySectionKeyValueSettings>())
                {
                    allowedApps.Add(kv.Key, kv.Value);
                }
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
                ilog.Debug("Request(stamp_TIMES):" + stamp_TIMES);
            }
            else
            {
                b = false;
            }
            if (req.Headers.TryGetValues("appName", out keyValue))
            {
                appName = keyValue.FirstOrDefault();
                ilog.Debug("Request(appName):" + appName);
            }
            else
            {
                b = false;
            }
            if (req.Headers.TryGetValues("token", out keyValue))
            {
                token = keyValue.FirstOrDefault();
                ilog.Debug("Request(token):" + token);
            }
            if (req.Headers.TryGetValues("sign", out keyValue))
            {
                sign = keyValue.FirstOrDefault();
                ilog.Debug("Request(sign):" + sign);
            }
            else
            {
                b = false;
            }
            if (b)
            {
                string IP4Address = String.Empty;
                if (appName == "JS1adBF8cbaf594d1ab2f1A68755e70440")
                {
                    //context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
                    //HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                    // 获取远程客户端的浏览器信息
                    //HttpBrowserCapabilities httpbc = System.Web.HttpContext.Current.Request.Browser;
                    //string strInfo = "您好，您正在使用   " + httpbc.Browser + "   v. " + httpbc.Version + ",你的运行平台是   " + httpbc.Platform;
                    ////获取远程客户端的ip主机地址 
                    //strInfo = System.Web.HttpContext.Current.Request.UserHostAddress;
                    ////获取远程客户端的DNS名称 
                    //strInfo = System.Web.HttpContext.Current.Request.UserHostName;
                    ////客户端上次请求的URL路径 
                    //strInfo = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
                    ////当前请求的URl 
                    //strInfo = System.Web.HttpContext.Current.Request.Url.ToString();
                    ////客户端浏览器的原始用户代理信息 
                    //strInfo = System.Web.HttpContext.Current.Request.UserAgent;
                    string str = HttpContext.Current.Request.UserHostAddress;
                    foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
                    {
                        if (IPA.AddressFamily.ToString() == "InterNetwork")
                        {
                            IP4Address = IPA.ToString();
                            break;
                        }
                    }
                    //获取本地的IP
                    //foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                    //{
                    //    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    //    {
                    //        IP4Address = IPA.ToString();
                    //        break;
                    //    }
                    //}
                }
                var isValid = isValidRequest(req, appName, sign, token, stamp_TIMES, IP4Address);
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
        private async Task<bool> isValidRequest(HttpRequestMessage req, string appName, string sign, string token, string stamp_TIMES,string IP4Address)
        {
            string requestContentBase64String = "";
            string requestUri =HttpUtility.UrlEncode(req.RequestUri.AbsoluteUri.ToLower());
            string requestHttpMethod = req.Method.Method;
            if (!allowedApps.ContainsKey(appName))
            {
                return false;
            }
            var sharedKey = allowedApps[appName];
            if (appName == "JS1adBF8cbaf594d1ab2f1A68755e70440")
            {
                string[] stringToken = token.Split('.');
                if (stringToken.Length != 4)
                {
                    return false;
                }
                sharedKey = allowedApps[stringToken[3]];
                token = stringToken[0] + "." + stringToken[1] + "." + stringToken[2];
            }
            ilog.Debug("Request(apiKey):" + sharedKey);
            if (isReplayRequest(sign, stamp_TIMES))
            {
                return false;
            }

            ilog.Debug("Request(RequestUri):" + req.RequestUri.AbsolutePath.ToLower());
            if (req.Method == HttpMethod.Post && (req.RequestUri.AbsolutePath.ToLower() == "/api/v1/authorization" || req.RequestUri.AbsolutePath.ToLower() == "/api/v1/customers" || req.RequestUri.AbsolutePath.ToLower() == "/api/v1/merchants"))
            { }
            else
            {
                ApplicationService.Customer customer = new ApplicationService.Customer();
                try { customer = customer.getCustomer(token, sharedKey, true); }
                catch { return false; }
                if (System.Runtime.Caching.MemoryCache.Default.Contains(customer.UserID))
                {
                    string strToken = System.Runtime.Caching.MemoryCache.Default[customer.UserID].ToString();
                    if (token != strToken)
                    {
                        return false;
                    }
                }
                else
                {
                    BLL.Client.BLLUserToken bllusertoken = Bootstrap.Container.Resolve<BLL.Client.BLLUserToken>();
                    //NHibernateUnitOfWork.UnitOfWork.Start();
                    if (bllusertoken.CheckToken(token))
                    {
                        //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                        return false;
                    }
                    System.Runtime.Caching.MemoryCache.Default.Add(customer.UserID,token,DateTimeOffset.UtcNow.AddSeconds(172800));
                    //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                }
            }

            byte[] hash = await ComputeHash(req.Content);
            StringBuilder sb = new StringBuilder();
            if (hash != null)
            {
                //string str = Encoding.Default.GetString(hash);
                for (int i = 0; i < hash.Length; i++)
                { sb.Append(hash[i].ToString("x2")); }
                requestContentBase64String = sb.ToString();
                //byte[] baseBuffer = Encoding.UTF8.GetBytes(sb.ToString()); //把转码后的MD5 32位密文转成byte[ ] txtNeed.Text = Convert.ToBase64String(baseBuffer); //这个要注意，不要在newbuffer就转，你解密的时候会乱码（有时候）
                //requestContentBase64String = Convert.ToBase64String(baseBuffer);
                ilog.Debug("Create(requestContentBase64String):" + requestContentBase64String);
            }
            string data = String.Format("{0}{1}{2}{3}{4}", appName, token, requestContentBase64String, stamp_TIMES, requestUri);
            byte[] signature = Encoding.UTF8.GetBytes(data);
            sb = new StringBuilder();
            byte[] baseBuffer = null;
            if (appName == "JS1adBF8cbaf594d1ab2f1A68755e70440")
            {
                //sign = hybrid端appName +
                //移动端token + "." + 移动端的appName +
                //请求参数requestContent(转json字符串， MD5) +
                //stamp_TIMES时间戳 +
                //endpoint请求API路径（URL编码）
                //对sign在进行MD5后进行Base64加密
                //请求时添加 token 和 sign 到header进行请求
                string strsb = "";
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hashMD5 = md5.ComputeHash(signature);
                    for (int i = 0; i < hashMD5.Length; i++)
                    { sb.Append(hashMD5[i].ToString("x2")); }
                    baseBuffer = Encoding.UTF8.GetBytes(sb.ToString());
                    strsb = Convert.ToBase64String(baseBuffer); 
                }
                if (sign.Equals(strsb, StringComparison.Ordinal))
                {
                    string strIP = ConfigurationManager.AppSettings[appName];
                    HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", strIP);
                    return (strIP.Equals(IP4Address, StringComparison.Ordinal));
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //var secretKeyBytes = Convert.FromBase64String(sharedKey);
                byte[] signature22 = Encoding.UTF8.GetBytes(sharedKey);
                using (HMACSHA256 hmac = new HMACSHA256(signature22))
                {
                    byte[] signatureBytes = hmac.ComputeHash(signature);
                    for (int i = 0; i < signatureBytes.Length; i++)
                    { sb.Append(signatureBytes[i].ToString("x2")); }
                    //string strsb = sb.ToString();
                    baseBuffer = Encoding.UTF8.GetBytes(sb.ToString());
                    string tt = Convert.ToBase64String(baseBuffer);
                    ilog.Debug("Create(sign):" + tt);
                    return (sign.Equals(tt, StringComparison.Ordinal));
                }
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
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan currentTs = DateTime.UtcNow - epochStart;
            //DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Local);
            //TimeSpan currentTs = DateTime.Now - epochStart;
            //var serverTotalSeconds = Convert.ToUInt64(currentTs.TotalSeconds);
            var serverTotalSeconds = Convert.ToInt64(currentTs.TotalMilliseconds);//.TotalSeconds
            ilog.Debug("Create(stamp_TIMES):" + serverTotalSeconds.ToString());
            //var requestTotalSeconds = Convert.ToUInt64(requestTimeStamp);
            var requestTotalSeconds = Convert.ToInt64(requestTimeStamp);
            ilog.Debug("Request(stamp_TIMES1):" + requestTotalSeconds.ToString());
            ilog.Debug("Request(requestMaxAgeInSeconds):" + requestMaxAgeInSeconds.ToString());
            ilog.Debug("Check(bool):" + (serverTotalSeconds - requestTotalSeconds).ToString());
            ilog.Debug("Check(bool1):" + ((serverTotalSeconds - requestTotalSeconds) > requestMaxAgeInSeconds).ToString());
            if (Math.Abs(serverTotalSeconds - requestTotalSeconds) > 120000)
            {
                ilog.Debug("Create(stamp_TIMES2):" + serverTotalSeconds.ToString());
                return true;
            }
            ilog.Debug("Create(stamp_TIMES3):" + serverTotalSeconds.ToString());
            try
            {
                System.Runtime.Caching.MemoryCache.Default.Add(nonce, requestTimeStamp, DateTimeOffset.UtcNow.AddSeconds(requestMaxAgeInSeconds));
            }
            catch (Exception ex)
            {
                ilog.Error("Cache(sgin):" + ex.Message);
            }
            ilog.Debug("Create(stamp_TIMES4):" + serverTotalSeconds.ToString());
            return false;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        private async Task<byte[]> ComputeHash(HttpContent httpContent)
        {
            //MD5 md = new MD5CryptoServiceProvider();
            //MD5 md5 =MD5.Create()
            using (MD5 md5 = MD5.Create())
            {
                //MD5 md5 = new MD5CryptoServiceProvider();
                byte[] hash = null;
                var content = await httpContent.ReadAsByteArrayAsync();
                string str = Encoding.Default.GetString(content);
                ilog.Debug("Request(httpContent):" + str.ToString());
                //string signatureRawData = await httpContent.ReadAsStringAsync();
                //byte[] signature = Encoding.UTF8.GetBytes(str);

                //byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
                if (content.Length != 0)
                {
                    hash = md5.ComputeHash(content);

                }
                return hash;
                //return result;
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
                response.Content = new StringContent("{\"errCode\":\"001001\",\"errString\":\"用户认证失败\"}");
            }

            return response;
        }
    }
}