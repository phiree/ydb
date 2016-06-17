using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_ChatFiltering
    {
        /// <summary>
        /// 订单类别("chat":[文本]，"voice":[语音]，"image":[图片]，"video":[视频]，"url":[网址]，"pushOrder":[推送的订单])
        /// </summary>
        /// <type>string</type>
        public string type { get; set; }

        /// <summary>
        /// 我与另一个目标("customerService":[客服]，"store":[店铺]，"user":[用户]，"system":[系统])
        /// </summary>
        /// <type>string</type>
        public string fromTarget { get; set; }
    }
}
