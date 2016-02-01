using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
using System.Diagnostics;
namespace Dianzhu.Model
{
    /// <summary>
    /// 支付项. 每个订单可能有多个支付项(订金,尾款,赔偿等)
    /// </summary>
    
    public class Payment
    {

        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Model");
        
        public Payment()
        {
            CreatedTime =LastUpdateTime=  DateTime.Now;
            //Amount = Order.GetAmount(PayTarget);
        }
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        public virtual ServiceOrder Order{ get; set; }

        /// <summary>
        /// 支付目标
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
        /// 是否支付成功
        /// </summary>
        public virtual bool  IsSuccess { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public virtual enum_PaymentStatus Status { get; set; }
       
     
    }


    
}
