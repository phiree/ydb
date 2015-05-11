using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    public class DZMembership
    {
        public virtual Guid Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime TimeCreated { get; set; }
        
    }
    public class BusinessUser : DZMembership
    {
        public virtual Business BelongTo { get; set; }
    }
}
