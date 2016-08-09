using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_RefundFiltering
    {
        /// <summary>
        /// 理赔请求的动作("submit":[提交理赔请求]，"refund":[店铺同意理赔要求]，"reject":[店铺拒绝理赔]，"askPay":[店铺要求支付赔偿金]，"agree":[用户同意商户处理]，"cancel":[用户放弃理赔]，"intervention":[用户要求官方介入])
        /// </summary>
        /// <type>string</type>
        public string action { get; set; }
    }
}
