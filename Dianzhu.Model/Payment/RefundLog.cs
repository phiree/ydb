using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common;

namespace Dianzhu.Model
{
    /// <summary>
    /// 支付记录.
    /// </summary>
    public class RefundLog:DDDCommon.Domain.Entity<Guid>
    {
        protected RefundLog()
        {

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="apiStrind">api返回字符串</param>
        /// <param name="refundId">退款Id</param>
        /// <param name="refundAmount">退款金额</param>
        /// <param name="payLogType">支付记录的类型</param>
        /// <param name="payType">支付方式</param>
        public RefundLog(string apiStrind,Guid refundId,decimal refundAmount, enum_PaylogType payLogType, enum_PayType payType)
        {
            this.LogTime = DateTime.Now;

            this.ApiString = apiStrind;
            this.RefundId = refundId;
            this.RefundAmount = refundAmount;
            this.PaylogType = payLogType;
            this.PayType = payType;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// api返回字符串
        /// </summary>
        public virtual string ApiString { get; set; }

        /// <summary>
        /// 退款Id
        /// </summary>
        public virtual Guid RefundId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal RefundAmount { get; set; }

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
