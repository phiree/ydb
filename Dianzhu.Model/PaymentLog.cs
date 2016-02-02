using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 支付记录.
    /// </summary>
    public class PaymentLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }

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
        public virtual decimal PayAmount
        { get; set; }
        /// <summary>
        /// 支付记录的类型：发起支付,支付接口回调
        /// </summary>
        public virtual Enums.enum_PaylogType PaylogType { get; set; }

        /// <summary>
        /// 支付接口类型:支付宝,微信...
        /// </summary>
        public virtual Enums.enum_PayAPI PayApi { get; set; }

        /// <summary>
        /// 款项类型:  定金,尾款..赔偿金..
        /// </summary>
        public virtual Enums.enum_PayTarget PayTarget { get; set; }

        /// <summary>
        /// 离线,在线支付.
        /// </summary>
        public virtual Enums.enum_PayType PayType { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public virtual DateTime LogTime { get; set; }
     
    }
}
