using Dianzhu.Model.Enums;
using System;

namespace Dianzhu.Model
{
    /// <summary>
    /// 订单状态变化记录
    /// </summary>
    public class ServiceOrderStateChangeHis

    {
        public ServiceOrderStateChangeHis()
        {
            CreatTime = DateTime.Now;
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
        /// 订单状态
        /// </summary>
        public virtual enum_OrderStatus OldStatus { get; set; }
        public virtual enum_OrderStatus NewStatus { get; set; }
        /// <summary>
        ///订单预期
        /// </summary>
        public virtual decimal OrderAmount { get; set; }
        /// <summary>
        /// 订金
        /// </summary>
        public virtual decimal DepositAmount { get; set; }
        /// <summary>
        /// 协商总价
        /// </summary>
        public virtual decimal NegotiateAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public virtual DateTime CreatTime { get; set; }
        /// <summary>
        /// 订单操作人员
        /// </summary>
        public virtual DZMembership Controller { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int Number { get; set; }
    }  
 
     

}
