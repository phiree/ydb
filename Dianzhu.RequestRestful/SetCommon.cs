using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Dianzhu.RequestRestful
{
    public class SetCommon
    {
        /// <summary>
        /// 设置时间戳
        /// </summary>
        /// <returns></returns>
        public static string SetTimeStamp()
        {
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = DateTime.UtcNow - epochStart;
            return Convert.ToUInt64(timeSpan.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 设置body参数的MD5加密
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string SetContentMD5(string strContent)
        {
            string requestContentBase64String = "";
            if (strContent.Trim() != "")
            {
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(strContent.Replace("\r", string.Empty));
                MD5 md5 = MD5.Create();
                byte[] requestContentHash = md5.ComputeHash(byteData);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < requestContentHash.Length; i++)
                { sb.Append(requestContentHash[i].ToString("x2")); }
                requestContentBase64String = sb.ToString();
            }
            return requestContentBase64String;
        }

        /// <summary>
        /// 设置请求签名
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="appKey"></param>
        /// <param name="strToken"></param>
        /// <param name="strContentMD5"></param>
        /// <param name="strTimeStamp"></param>
        /// <param name="strUri"></param>
        /// <returns></returns>
        public static string SetSign(string appName, string appKey, string strToken,string strContentMD5,string strTimeStamp,string strUri)
        {
            string signatureRawData = String.Format("{0}{1}{2}{3}{4}", appName, strToken, strContentMD5, strTimeStamp, strUri);

            byte[] signature22 = Encoding.UTF8.GetBytes(appKey);
            byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);
            string requestSignatureBase64String = "";
            using (HMACSHA256 hmac = new HMACSHA256(signature22))
            {
                byte[] signatureBytes = hmac.ComputeHash(signature);
                StringBuilder sb1 = new StringBuilder();
                for (int i = 0; i < signatureBytes.Length; i++)
                { sb1.Append(signatureBytes[i].ToString("x2")); }
                byte[] baseBuffer = Encoding.UTF8.GetBytes(sb1.ToString());
                requestSignatureBase64String = Convert.ToBase64String(baseBuffer);
            }
            return requestSignatureBase64String;
        }

        /// <summary>
        /// 设置请求签名
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="appKey"></param>
        /// <param name="strToken"></param>
        /// <param name="strContentMD5"></param>
        /// <param name="strTimeStamp"></param>
        /// <param name="strUri"></param>
        /// <returns></returns>
        public static RequestParams SetParams(string appName, string appKey, RequestParams param)
        {
            if (string.IsNullOrEmpty(appName.Trim()))
            {
                throw new Exception("appName不能为空!");
            }
            if (string.IsNullOrEmpty(appKey.Trim()))
            {
                throw new Exception("appKey不能为空!");
            }
            string requestUri = System.Web.HttpUtility.UrlEncode(param.url.ToLower());
            string requestTimeStamp = SetTimeStamp();
            string requestContentBase64String = SetContentMD5(param.content);
            string requestSignatureBase64String = SetSign(appName, appKey, param.token, requestContentBase64String, requestTimeStamp, requestUri);

            param.appName = appName;
            param.stamp_TIMES = requestTimeStamp;
            param.sign = requestSignatureBase64String;
            return param;
        }
    }
}
