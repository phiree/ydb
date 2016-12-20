using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common.Domain;

namespace Ydb.Order.DomainModel
{
    /// <summary>
    /// 订单提醒
    /// </summary>
    public class ServiceOrderRemind: Entity<Guid>
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        protected ServiceOrderRemind()
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="time"></param>
        /// <param name="open"></param>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        public ServiceOrderRemind(string title,string content,DateTime time,bool open,Guid orderId,Guid userId)
        {
            this.Title = title;
            this.Content = content;
            this.RemindTime = time;
            this.CreateTime = DateTime.Now;
            this.Open = open;
            this.OrderId = orderId;
            this.UserId = userId;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 提醒名称
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 提醒内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 提醒时间
        /// </summary>
        public virtual DateTime RemindTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        public virtual bool Open { get; set; }
        /// <summary>
        /// 提醒对应的订单id
        /// </summary>
        public virtual Guid OrderId { get; set; }
        /// <summary>
        /// 提醒对应的用户id
        /// </summary>
        public virtual Guid UserId { get; set; }

    }
}
