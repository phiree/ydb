﻿ 
using System;
using Ydb.Common;
using Ydb.Common.Domain;
 
namespace Ydb.Order.DomainModel
{
    /// <summary>
    /// 支付项. 每个订单可能有多个支付项(订金,尾款,赔偿等)
    /// </summary>    
    public class Payment:Entity<Guid>
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model");

        protected Payment()
        {

        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="amount">本次支付总额</param>
        /// <param name="order">订单</param>
        /// <param name="payTarget">支付目标款项类型</param>
        /// <param name="payType">支付方式</param>
        public Payment(decimal amount,ServiceOrder order,enum_PayTarget payTarget)
        {
            CreatedTime =LastUpdateTime=  DateTime.Now;

            Amount = amount;
            Order = order;
            PayTarget = payTarget;
            PayType =  enum_PayType.None;
        }

        /// <summary>
        /// 订单
        /// </summary>
        public virtual ServiceOrder Order{ get; set; }

        /// <summary>
        /// 支付目标款项类型:  定金,尾款..赔偿金..
        /// </summary>
        public virtual enum_PayTarget PayTarget { get; set; }

        /// <summary>
        /// 本次支付总额
        /// </summary>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get; set; }

        /// <summary>
        /// 支付接口类型:支付宝,微信...
        /// </summary>
        public virtual enum_PayAPI PayApi { get; set; }

        /// <summary>
        /// 平台交易号,如果是在线支付，则由支付平台返回
        /// </summary>
        public virtual string PlatformTradeNo { get; set; }

        /// <summary>
        ///  最后操作时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public virtual enum_PaymentStatus Status { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo { get; set; }

        /// <summary>
        /// 支付方式:是否在线支付
        /// </summary>
        public virtual enum_PayType PayType { get; set; }




    }


    
}
