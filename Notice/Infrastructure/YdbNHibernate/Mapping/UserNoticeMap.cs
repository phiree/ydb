using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = Ydb.Notice.DomainModel;

namespace Ydb.Notice.Infrastructure.YdbNHibernate.Mapping
{
    public class UserNoticeMap : ClassMap<M.UserNotice>
    {
        public UserNoticeMap()
        {
            Id(x => x.Id);
            Map(x => x.IsReaded);
            References<DomainModel.Notice>(x => x.Notice);
            Map(x => x.ReadTime);
            Map(x => x.UserId);
        }
    }
}