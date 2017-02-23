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
        public  enum_UserType TargetUserType { get; internal set; }
        public string Title { get; internal set; }
        public string Body { get; internal set; }
        public DateTime TimeCreated { get; internal set; }
        public Guid AuthorId { get; internal set; }

        public bool IsApproved { get; internal set; }
        public Guid ApproverId { get; internal set; }
        public DateTime ApprovedTime { get; internal set; }
    }
}
