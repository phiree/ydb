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
        public Guid Id { get; set; }
        public DZMembership Member { get; set; }
        public DZRole Role { get; set; }

    }
}
