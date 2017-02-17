using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ydb.Push.JPush
{
    public class AuthorizationHeader
    {
        private static string AuthKey = string.Empty;
        public static string GenerateAuthorizationValue()
        {
            if (string.IsNullOrEmpty(AuthKey))

            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Config.AppKey + ":" + Config.MasterSecret);
                AuthKey = System.Convert.ToBase64String(plainTextBytes);
            }
            return "Basic "+ AuthKey;
        }
    }
}
