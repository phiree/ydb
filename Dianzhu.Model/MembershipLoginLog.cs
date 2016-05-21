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
        public MembershipLoginLog()
        { }
        public MembershipLoginLog(DZMembership membership,enumLoginLogType logType,string memo)
        {
            LogTime = DateTime.Now;
            Membership = membership;
            logType = logType;
            Memo = memo;
        }
        public virtual Guid Id { get; protected set; }
        public virtual DZMembership Membership { get; protected set; }
        public virtual DateTime LogTime { get; protected set; }
        public virtual enumLoginLogType LogType { get; protected set; }
        //备注
        public virtual string Memo { get; protected set; }
    }
    /// <summary>
    /// 类型.
    /// </summary>
    public enum enumLoginLogType {
        Login,
        Logoff
    }
    
    
}
