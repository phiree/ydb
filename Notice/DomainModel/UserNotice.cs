using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Notice.DomainModel
{
    /// <summary>
    /// 已读的通知.
    /// </summary>
    public class UserNotice:Ydb.Common.Domain.Entity<Guid>
    {
        public UserNotice()
        { }

        public  UserNotice(Guid userid, Notice notice)
        {
            UserId = userid;
            this.Notice = notice;
            IsReaded = false;

        }

        public virtual Guid UserId { get; protected internal set; }
        public virtual Notice Notice { get; protected internal set; }

        public virtual bool IsReaded { get; protected internal set; }
        public virtual DateTime ReadTime { get; protected internal set; }

        public virtual void UserReaded()
        {
            IsReaded = true;
            ReadTime = DateTime.Now;
        }
    }
}
