using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class payObj
    {
        string _id = null;
        /// <summary>
        /// 支付款项的ID
        /// </summary>
        /// <type>string</type>
        public string id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        string _amount = "";
        /// <summary>
        /// 需要支付的金额（元）
        /// </summary>
        /// <type>string</type>
        public string amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        string _payStatus = "";
        /// <summary>
        /// 支付的状态（"waitForPay":[等待支付]，"waitForVerify":[等待审核]，"success":[支付成功]，"failed":[支付失败]，"invalid":[失效]）
        /// </summary>
        /// <type>string</type>
        public string payStatus
        {
            get
            {
                return _payStatus;
            }
            set
            {
                _payStatus = value;
            }
        }

        string _type = "";
        /// <summary>
        /// 类型（"deposit":[订金]，"finalPayment":[尾款]，"compensation":[赔偿金]）
        /// </summary>
        /// <type>string</type>
        public string type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        string _updateTime = "";
        /// <summary>
        /// 更新时间(yyyyMMddHHmmss)
        /// </summary>
        /// <type>string</type>
        public string updateTime
        {
            get
            {
                return _updateTime;
            }
            set
            {
                _updateTime = value;
            }
        }

        bool _bOnline = true;
        /// <summary>
        /// 是否在线支付（true Or false）
        /// </summary>
        /// <type>boolean</type>
        public bool bOnline
        {
            get
            {
                return _bOnline;
            }
            set
            {
                _bOnline = value;
            }
        }

        string _payTarget = "";
        /// <summary>
        /// 支付接口类型:支付宝,微信..
        /// </summary>
        /// <type>string</type>
        public string payTarget
        {
            get
            {
                return _payTarget;
            }
            set
            {
                _payTarget = value;
            }
        }

        //bool _online = true;
        ///// <summary>
        ///// 是否在线支付（true Or false）//修改的参数
        ///// </summary>
        ///// <type>boolean</type>
        //public bool online
        //{
        //    get
        //    {
        //        return _online;
        //    }
        //    set
        //    {
        //        _online = value;
        //    }
        //}

    }
}
