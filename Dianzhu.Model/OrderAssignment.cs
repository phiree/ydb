using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 订单分配详情.
    /// </summary>
    public   class OrderAssignment:DDDCommon.Domain.Entity<Guid>
    {
        /// <summary>
        /// 构造，初始化分配时间和是否有效
        /// </summary>
        public OrderAssignment()
        {
            CreatTime = DateTime.Now;
            Enabled = true;
        }

        public virtual Guid Id { get; set; }
        /// <summary>
        /// 被分配的订单
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
        /// <summary>
        /// 该订单分配的员工. 如果是多个员工,则创建多个对象.
        /// </summary>
        public virtual Staff AssignedStaff { get; set; }
        /// <summary>
        /// 分配的时间
        /// </summary>
        public virtual DateTime AssignedTime { get; set; }

        /// <summary>
        /// 取消分配的时间
        /// </summary>
        public virtual DateTime DeAssignedTime { get; set; }
        /// <summary>
        /// 是否有效. 否:已经取消了该分配
        /// </summary>
        public virtual bool Enabled { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public virtual DateTime CreatTime { get; set; }
    }
}
