using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Membership.DomainModel
{
    public class MembershipLoginLog : Entity<Guid>
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
