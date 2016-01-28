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
            LastUpdateTime = DateTime.Now;
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
        public virtual enum_OrderStatus Status { get; set; }
        /// <summary>
        /// 订单当前价格
        /// </summary>
        public virtual decimal NewAmount { get; set; }        
        /// <summary>
        /// 订单未更改前价格
        /// </summary>
        public virtual decimal OldAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 更新订单操作人员
        /// </summary>
        public virtual DZMembership UpdateController { get; set; }
    }  
 
     

}
