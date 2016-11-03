using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Domain;
using Ydb.InstantMessage.Application.Dto;

namespace Ydb.InstantMessage.DomainModel.Reception
{
    /// <summary>
    /// 客服/用户 接待对应情况
    /// </summary>
  public   class ReceptionStatus:Entity<Guid>
    {
        public ReceptionStatus() { }

        /// <summary>
        /// 构造
        /// </summary>
        public ReceptionStatus(string customerId, string customerServiceId,string orderId)
        {
            this.CustomerServiceId = customerServiceId;
            this.CustomerId = customerId;
            this.LastUpdateTime = DateTime.Now; 
            this.OrderId = orderId;
        } 

        public virtual void ChangeCS(string csId)
        {
            this.CustomerServiceId = csId;
        }

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

        public virtual ReceptionStatusDto ToDto()
        {
            ReceptionStatusDto dto = new ReceptionStatusDto();

            dto.Id = this.Id;
            dto.CustomerId = this.CustomerId;
            dto.CustomerServiceId = this.CustomerServiceId;
            dto.OrderId = this.OrderId;

            return dto;
        }

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
