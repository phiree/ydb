using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 当前接待情况.
    /// </summary>
  public   class ReceptionStatus
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
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 客服
        /// </summary>
        public virtual DZMembership CustomerService { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual DZMembership Customer { get; set; }

        /// <summary>
        /// 存档时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 最后接待订单
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
        
    }


    /// <summary>
    /// 当前接待情况.
    /// </summary>
    public class ReceptionStatusArchieve
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
        public virtual DZMembership CustomerService { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual DZMembership Customer { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime ArchieveTime { get; set; }

        /// <summary>
        /// 最后接待订单
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
    }
}
