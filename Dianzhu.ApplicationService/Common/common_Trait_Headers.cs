namespace Dianzhu.ApplicationService
{
    public class common_Trait_Headers
    {
        /// <summary>
        /// 请求报文时间戳，为1970年0时0分0秒0毫秒至今的毫秒数
        /// </summary>
        /// <type>string</type>
        public string stamp_TIMES { get; set; }

        /// <summary>
        /// 平台名称(IOS,Android,Web,system)
        /// </summary>
        /// <type>string</type>
        public string appName { get; set; }

        /// <summary>
        /// token参数
        /// </summary>
        /// <type>string</type>
        public string token { get; set; }

        /// <summary>
        /// 签名(将 token、security_key、stamp_TIMES、appName、endpoint以及请求参数，进行 HMAC-SHA256 的签名算法，得到 sign)
        /// </summary>
        /// <type>string</type>
        public string sign { get; set; }

        /// <summary>
        /// 密匙
        /// </summary>
        /// <type>string</type>
        public string apiKey { get; set; }
    }
}