 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 设备绑定
    /// </summary>
   public  class DeviceBind : DDDCommon.Domain.Entity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string DZMembershipId { get; set; }
        public virtual string AppName { get; set; }
        public virtual string AppToken { get; set; }
        public virtual bool IsBinding { get; set; }//是否正在绑定,设定该值的时候 要解除该Membership 和 Apptoken的所有绑定.
        public virtual DateTime BindChangedTime { get; set; }
        public virtual Guid AppUUID { get; set; }
        public virtual DateTime SaveTime { get; set; }//保存时间
        public virtual int PushAmount { get; set; }//推送条数

        
    }
}
