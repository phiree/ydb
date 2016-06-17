using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;

namespace Dianzhu.ApplicationService
{
    public class complaintObj
    {
        /// <summary>
        /// 发送者ID
        /// </summary>
        /// <type>string</type>
        public string senderID { get; set; }

        /// <summary>
        /// 投诉的订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID { get; set; }

        /// <summary>
        /// 投诉的目标("customerService":[客服]，"store":[商铺])
        /// </summary>
        /// <type>string</type>
        public Model.Enums.enum_ComplaintTarget target { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <type>string</type>
        public string content { get; set; }

        /// <summary>
        /// 图片资源链接
        /// </summary>
        /// <type>array[string]</type>
        public IList<String> resourcesUrl { get; set; }

        /// <summary>
        /// 该投诉的状态
        /// </summary>
        /// <type>string</type>
        public Model.Enums.enum_ComplaintStatus status { get; set; }
        
    }
}
