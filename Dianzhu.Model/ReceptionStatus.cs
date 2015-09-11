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
      public virtual Guid Id { get; set; }
        public virtual DZMembership CustomerService { get; set; }
        public virtual DZMembership Customer { get; set; }
        
    }
}
