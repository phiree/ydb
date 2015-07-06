using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
   public  class DeviceBind
    {
       public virtual Guid Id { get; set; }
       public virtual DZMembership DZMembership { get; set; }
       public virtual string AppName { get; set; }
       public virtual string AppToken { get; set; }
       public virtual bool IsBinding { get; set; }//是否正在绑定,设定该值的时候 要解除该Membership 和 Apptoken的所有绑定.
       public virtual DateTime BindChangedTime { get; set; }
    }
}
