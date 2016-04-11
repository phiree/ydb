using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 用户和角色的对应关系
    /// </summary>
   public  class RoleMember
    {
        public virtual Guid Id { get; set; }
        public virtual DZMembership Member { get; set; }
        public virtual DZRole Role { get; set; }

        
    }
}
