using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class orderStatusObj
    {
        /// <summary>
        /// 状态字符串
        /// </summary>
        /// <type>string</type>
        public string status { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        /// <type>string</type>
        public string createTime { get; set; }

        /// <summary>
        /// 对应的上一状态
        /// </summary>
        /// <type>string</type>
        public string lastStatus { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        /// <type>string</type>
        public string title { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        /// <type>string</type>
        public string content { get; set; }
    }
}
