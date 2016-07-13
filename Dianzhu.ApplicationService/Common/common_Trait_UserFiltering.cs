using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_UserFiltering
    {
        /// <summary>
        /// 手机号
        /// </summary>
        /// <type>string</type>
        public string phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        /// <type>string</type>
        public string email { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        /// <type>string</type>
        public string alias { get; set; }

        /// <summary>
        /// 注册的平台（"WeChat":[微信]，"SinaWeiBo":[新浪微博]，"TencentQQ":[腾讯QQ]，"system":[系统]）
        /// </summary>
        /// <type>string</type>
        public string platform { get; set; }
        
    }
}
