using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dianzhu.CSClient
{
    public class StringHelper
    {
        /// <summary>
        /// 普通用户名转换成openfire用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string EnsureNormalUserName(string userName)
        {
            string normalUserName = userName;
            if (Regex.IsMatch(userName, @"^[^\.@]+||[^\.@]+\.[^\.@]+$"))
            {
                normalUserName = userName.Replace("||", "@");
            }
            return normalUserName;
        }
        /// <summary>
        ///openfire 用户转换成普通用户.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string EnsureOpenfireUserName(string userName)
        {
            string openfireName = userName;
            if (Regex.IsMatch(userName, @"^[^\.@]+@[^\.@]+\.[^\.@]+$"))
            {
                openfireName = userName.Replace("@", "||");
            }
            return openfireName;
        }
    }
}
