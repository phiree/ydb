using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 用户登录/注销 记录
    /// </summary>
    public class MembershipLoginLog : DDDCommon.Domain.Entity<Guid>
    {
        public MembershipLoginLog()
        { }
        public MembershipLoginLog(string memberId, enumLoginLogType logType, string memo)
        {
            LogTime = DateTime.Now;
            MemberId = memberId;
            LogType = logType;
            Memo = memo;
        }
        public virtual string MemberId { get; protected set; }
        public virtual DateTime LogTime { get; protected set; }
        public virtual enumLoginLogType LogType { get; protected set; }
        //备注
        public virtual string Memo { get; protected set; }
    }
    /// <summary>
    /// 类型.
    /// </summary>
    public enum enumLoginLogType
    {
        Login,
        Logoff
    }


}
