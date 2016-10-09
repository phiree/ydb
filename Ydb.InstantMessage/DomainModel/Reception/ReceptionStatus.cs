using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Domain;
namespace Ydb.InstantMessage.DomainModel.Reception
{
    /// <summary>
    /// 客服/用户 接待对应情况
    /// </summary>
  public   class ReceptionStatus:Entity<Guid>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ReceptionStatus()
        {
            LastUpdateTime = DateTime.Now;
        }
         
        /// <summary>
        /// guid
        /// </summary>
    

        /// <summary>
        /// 客服
        /// </summary>
        public virtual string CustomerServiceId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual string CustomerId { get; set; }

        /// <summary>
        /// 存档时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 最后接待订单
        /// </summary>
        public virtual string OrderId { get; set; }

        

    }


    /// <summary>
    /// 当前接待情况.
    /// </summary>
    public class ReceptionStatusArchieve: Entity<Guid>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ReceptionStatusArchieve()
        {
            ArchieveTime = DateTime.Now;
        }

        /// <summary>
        /// guid
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 客服
        /// </summary>
        public virtual string CustomerServiceId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual string CustomerId { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime ArchieveTime { get; set; }

        /// <summary>
        /// 最后接待订单
        /// </summary>
        public virtual string OrderId { get; set; }
    }
}
