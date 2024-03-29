﻿namespace Dianzhu.ApplicationService
{
    public class Common_Body
    {
        /// <summary>
        /// app设备推送计数
        /// </summary>
        /// <type>string</type>
        public string pushCount { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        /// <type>string</type>
        //[StringLength(11, ErrorMessage = "手机号码为11位.", MinimumLength = 11)]
        //[RegularExpression(@"^1[3578]\d{9}$", ErrorMessage = "手机号码不合法！")]
        public string phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        /// <type>string</type>
        public string email { get; set; }

        /// <summary>
        /// app设备推送计数
        /// </summary>
        /// <type>string</type>
        public string pWord { get; set; }

        /// <summary>
        /// 注册平台
        /// </summary>
        /// <type>string</type>
        public string platform { get; set; }

        /// <summary>
        /// 平台 code
        /// </summary>
        /// <type>string</type>
        public string code { get; set; }

        /// <summary>
        /// 要变更的订单状态
        /// </summary>
        /// <type>string</type>
        public string newStatus { get; set; }

        /// <summary>
        /// 确认服务时的服务ID
        /// </summary>
        /// <type>string</type>
        public string serviceID { get; set; }
    }
}