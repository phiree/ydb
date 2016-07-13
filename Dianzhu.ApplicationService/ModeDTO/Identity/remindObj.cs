using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class remindObj
    {
        /// <summary>
        /// 提醒ID
        /// </summary>
        /// <type>string</type>
        public string id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        /// <type>string</type>
        public string title { get; set; }

        /// <summary>
        /// 提醒时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string remindTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <type>string</type>
        public string content { get; set; }

        /// <summary>
        /// 是否开启（true Or false）
        /// </summary>
        /// <type>boolean</type>
        public bool bOpen { get; set; }

        /// <summary>
        /// 关联的订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID { get; set; }
    }
}
