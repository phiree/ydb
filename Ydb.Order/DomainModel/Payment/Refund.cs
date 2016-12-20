using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Diagnostics;
using Ydb.Common;
using Ydb.Common.Domain;

namespace Ydb.Order.DomainModel
{
    /// <summary>
    /// 退款项. 每个订单可能有多次退款项
    /// </summary>
    public class Refund:Entity<Guid>
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model");

        protected Refund()
        {

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="order">订单</param>
        /// <param name="payment">支付项</param>
        /// <param name="totalAmount">支付总额</param>
        /// <param name="refundAmount">退款总额</param>
        /// <param name="refundReason">退款原因</param>
        /// <param name="platformTradeNo">平台交易号</param>
        /// <param name="refundStatus">支付状态</param>
        /// <param name="memo">备注</param>
        public Refund(ServiceOrder order,Payment payment,decimal totalAmount,decimal refundAmount,string refundReason,string platformTradeNo, enum_RefundStatus refundStatus,string memo)
        {
            this.CreatedTime =this.LastUpdateTime = DateTime.Now;
            this.Order = order;
            this.Payment = payment;
            this.TotalAmount = totalAmount;
            this.RefundAmount = refundAmount;
            this.RefundReason = refundReason;
            this.PlatformTradeNo = platformTradeNo;
            this.RefundStatus = refundStatus;
            this.Memo = memo;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        public virtual ServiceOrder Order { get; set; }

        /// <summary>
        /// 支付项
        /// </summary>
        public virtual Payment Payment { get; set; }

        /// <summary>
        /// 支付总额
        /// </summary>
        public virtual decimal TotalAmount { get; set; }

        /// <summary>
        /// 本次退款总额
        /// </summary>
        public virtual decimal RefundAmount { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public virtual string RefundReason { get; set; }

        /// <summary>
        /// 平台交易号,如果是在线支付，则由支付平台返回
        /// </summary>
        public virtual string PlatformTradeNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get; set; }

        /// <summary>
        ///  最后操作时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public virtual enum_RefundStatus RefundStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo { get; set; }




    }



}
