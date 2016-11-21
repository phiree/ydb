using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;

namespace Dianzhu.ApplicationService
{
    public class appObj
    {
        string _userID = "";
        /// <summary>
        /// 用户或客服或商家或员工ID
        /// </summary>
        /// <type>string</type>
        public string userID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

       enum_appName _appName = 0;
        /// <summary>
        /// 平台标识（"IOS_User":[IOS用户版]，"IOS_Merchant":[IOS商户版]，"IOS_CustomerService":[IOS客服版]，"Andriod_User":[Android用户版]，"Android_Merchant":[Android商户版]，"Android_CustomerService":[Android客户版]）
        /// </summary>
        /// <type>string</type>
        public enum_appName appName
        {
            get
            {
                return _appName;
            }
            set
            {
                _appName = value;
            }
        }

        string _appUUID = "";
        /// <summary>
        /// 移动设备UUID
        /// </summary>
        /// <type>string</type>
        public string appUUID
        {
            get
            {
                return _appUUID;
            }
            set
            {
                _appUUID = value;
            }
        }

        string _appToken = "";
        /// <summary>
        /// 移动设备推送标示符
        /// </summary>
        /// <type>string</type>
        public string appToken
        {
            get
            {
                return _appToken;
            }
            set
            {
                _appToken = value;
            }
        }
        

    }
}
