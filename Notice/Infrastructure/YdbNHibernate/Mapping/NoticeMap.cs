using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = Ydb.Notice.DomainModel;

namespace Ydb.Notice.Infrastructure.YdbNHibernate.Mapping
{
    public class NoticeMap : ClassMap<M.Notice>
    {
        public NoticeMap()
        {
            Id(x => x.Id);
            Map(x => x.ApprovedTime);
            Map(x => x.Title);
            Map(x => x.ApproverId);
            Map(x => x.AuthorId);
            Map(x => x.Body);
            Map(x => x.IsApproved);
            Map(x => x.TargetUserType);
            Map(x => x.TimeCreated);
        }
    }
}