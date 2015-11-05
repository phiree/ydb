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
         
      public virtual Guid Id { get; set; }
        public virtual DZMembership CustomerService { get; set; }
        public virtual DZMembership Customer { get; set; }
        public virtual DateTime LastUpdateTime { get; set; }
        
    }
}
