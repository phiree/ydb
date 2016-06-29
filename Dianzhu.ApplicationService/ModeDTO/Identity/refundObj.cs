using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class refundObj
    {
        /// <summary>
        /// 关联的订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID { get; set; }

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
        /// 理赔请求的动作（"submit":提交理赔请求，"refund":店铺同意理赔要求，"reject":店铺拒绝理赔，"askPqy":店铺要求支付赔偿金，"agree":用户同意商户处理，"cancel":用户放弃理赔，"intervention":用户要求官方介入）
        /// </summary>
        /// <type>boolean</type>
        public string action { get; set; }

        

    }
}
