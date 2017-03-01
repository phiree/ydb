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
    public class UserNotice
    {
        public Guid UserId { get; internal set; }
        public Notice Notice { get; internal set; }
        public DateTime ReadTime { get; internal set; }
    }
}
