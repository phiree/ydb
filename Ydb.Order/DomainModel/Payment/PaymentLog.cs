using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common;

namespace Ydb.Order.DomainModel
{
    /// <summary>
    /// 支付记录.
    /// </summary>
    public class PaymentLog : Entity<Guid>
    {
        public PaymentLog()
        {
            LogTime = DateTime.Now;
        }

        /// <summary>
        /// 请求参数 // api回调参数
        /// </summary>
        public virtual string ApiString { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public virtual Guid PaymentId{ get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal PayAmount { get; set; }

        /// <summary>
        /// 支付记录的类型：发起支付,支付接口回调
        /// </summary>
        public virtual  enum_PaylogType PaylogType { get; set; }
        
        /// <summary>
        /// 离线,在线支付.
        /// </summary>
        public virtual  enum_PayType PayType { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public virtual DateTime LogTime { get; set; }
     
    }
}
