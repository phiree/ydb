using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 支付日志
    /// </summary>
    public class PaymentLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public virtual string Pames { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public virtual ServiceOrder ServiceOrder { get; set; }

        /// <summary>
        /// 订单类型：发送or返回
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public virtual DateTime LastTime { get; set; }
     
    }
}
