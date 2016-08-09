using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_PayFiltering
    {
        /// <summary>
        /// 支付的状态("waitForPay":[等待支付]，"waitForVerify":[等待审核]，"success":[支付成功]，"failed":[支付失败])
        /// </summary>
        /// <type>string</type>
        public string payStatus { get; set; }

        /// <summary>
        /// 类型("deposit":[订金]，"finalPayment":[尾款]，"compensation":[赔偿金])
        /// </summary>
        /// <type>string</type>
        public string payType { get; set; }
    }
}
