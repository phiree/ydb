﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class payObj
    {
        /// <summary>
        /// 支付款项的ID
        /// </summary>
        /// <type>string</type>
        public string id { get; set; }

        /// <summary>
        /// 需要支付的金额（元）
        /// </summary>
        /// <type>string</type>
        public string amount { get; set; }

        /// <summary>
        /// 支付的状态（"waitForPay":[等待支付]，"waitForVerify":[等待审核]，"success":[支付成功]，"failed":[支付失败]，"invalid":[失效]）
        /// </summary>
        /// <type>string</type>
        public string payStatus { get; set; }

        /// <summary>
        /// 类型（"deposit":[订金]，"finalPayment":[尾款]，"compensation":[赔偿金]）
        /// </summary>
        /// <type>string</type>
        public string type { get; set; }

        /// <summary>
        /// 更新时间(yyyyMMddHHmmss)
        /// </summary>
        /// <type>string</type>
        public string updateTime { get; set; }

        /// <summary>
        /// 是否在线支付（true Or false）
        /// </summary>
        /// <type>boolean</type>
        public bool bOnline { get; set; }
        
    }
}