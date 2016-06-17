using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class adObj
    {
        /// <summary>
        /// 图片Url
        /// </summary>
        /// <type>string</type>
        public string imgUrl { get; set; }

        /// <summary>
        /// 广告的链接
        /// </summary>
        /// <type>string</type>
        public string url { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        /// <type>string</type>
        public string num { get; set; }

        /// <summary>
        /// 更新该广告的时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string updateTime { get; set; }
    }
}
