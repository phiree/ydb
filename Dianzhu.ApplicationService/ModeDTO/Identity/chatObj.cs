using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class chatObj
    {

        /// <summary>
        /// 消息ID
        /// </summary>
        /// <type>string</type>
        public string id { get; set; }

        /// <summary>
        /// 接收者ID
        /// </summary>
        /// <type>string</type>
        public string to { get; set; }

        /// <summary>
        /// 发送者ID
        /// </summary>
        /// <type>string</type>
        public string from { get; set; }

        /// <summary>
        /// 关联的订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID { get; set; }

        /// <summary>
        /// 消息文本
        /// </summary>
        /// <type>string</type>
        public string body { get; set; }

        /// <summary>
        /// 类型("chat":[文本]，"voice":[语音]，"image":[图片]，"video":[视频]，"url":[网址]，"pushOrder":[推送的订单])
        /// </summary>
        /// <type>string</type>
        public string type { get; set; }

        /// <summary>
        /// 发送时间(yyyyMMddHHmmss)
        /// </summary>
        /// <type>string</type>
        public string sendTime { get; set; }
    }
}
