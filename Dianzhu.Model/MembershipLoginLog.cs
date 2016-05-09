using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 用户登录/注销 记录
    /// </summary>
  public  class MembershipLoginLog
    {
        public Guid Id { get; set;}
        public DZMembership Membership { get; set; }
        public DateTime LogTime { get; set; }
        public enumLoginLogType LogType { get; set; }
        //备注
        public string Memo { get; set; }
    }
    /// <summary>
    /// 类型.
    /// </summary>
    public enum enumLoginLogType {
        Login,
        Logoff
    }
    
    
}
