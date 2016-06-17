using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class appObj
    {

        /// <summary>
        /// 用户或客服或商家或员工ID
        /// </summary>
        /// <type>string</type>
        public string userID { get; set; }

        /// <summary>
        /// 平台标识（"IOS_User":[IOS用户版]，"IOS_Merchant":[IOS商户版]，"IOS_CustomerService":[IOS客服版]，"Andriod_User":[Android用户版]，"Android_Merchant":[Android商户版]，"Android_CustomerService":[Android客户版]）
        /// </summary>
        /// <type>string</type>
        public Model.Enums.enum_appName appName { get; set; }

        /// <summary>
        /// 移动设备UUID
        /// </summary>
        /// <type>string</type>
        public string appUUID { get; set; }

        /// <summary>
        /// 移动设备推送标示符
        /// </summary>
        /// <type>string</type>
        public string appToken { get; set; }
        
    }
}
