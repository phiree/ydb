using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;

namespace Ydb.Notice.DomainModel
{
    public class Notice : Ydb.Common.Domain.Entity<Guid>
    {
        public virtual enum_UserType TargetUserType { get; protected internal set; }
        public virtual string Title { get; protected internal set; }
        public virtual string Body { get; protected internal set; }
        public virtual DateTime TimeCreated { get; protected internal set; }
        public virtual Guid AuthorId { get; protected internal set; }

        public virtual bool IsApproved { get; protected internal set; }
        public virtual Guid ApproverId { get; protected internal set; }
        public virtual DateTime ApprovedTime { get; protected internal set; }
    }
}