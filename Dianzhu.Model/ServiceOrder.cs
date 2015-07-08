using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
namespace Dianzhu.Model
{
    /// <summary>
    /// 订单
    /// </summary>
    public class ServiceOrder
    {
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 预订的服务
        /// </summary>
        public virtual DZService Service { get; set; }
        /// <summary>
        /// 分配的职员
        /// </summary>
        public virtual IList<Staff> Staff { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public virtual DZMembership Customer { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public virtual DateTime OrderCreated { get; set; }
      /// <summary>
      /// 订单状态.
      /// </summary>
        
        public virtual enum_OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// 服务的目标地址
        /// </summary>
        public virtual string TargetAddress { get; set; }

        public virtual decimal TotalPrice { get; set; }

    }
}
