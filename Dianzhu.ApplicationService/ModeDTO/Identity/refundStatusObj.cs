using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class refundStatusObj
    {

        /// <summary>
        /// 标题
        /// </summary>
        /// <type>string</type>
        public string title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <type>string</type>
        public string content { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        /// <type>string</type>
        public string amount { get; set; }

        /// <summary>
        /// 图片资源Url数组
        /// </summary>
        /// <type>array[string]</type>
        public IList<string> resourcesUrls { get; set; }

        /// <summary>
        /// 生成的时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string createTime { get; set; }

        /// <summary>
        /// 对应的订单状态
        /// </summary>
        /// <type>string</type>
        public string orderStatus { get; set; }

        /// <summary>
        /// 提交本次理赔的目标（"customerService":客服，"store":店铺，"user":用户，"system":系统）
        /// </summary>
        /// <type>string</type>
        public string target { get; set; }
    }
}
