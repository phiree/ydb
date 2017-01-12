using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.Membership.DomainModel;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping
{
    public class UserTokenMap : ClassMap<UserToken>
    {
        public UserTokenMap()
        {
            Id(x => x.Id);
            Map(x => x.UserID).UniqueKey("usertoken");
            Map(x => x.Token).Length(1000);
            Map(x => x.CreatedTime);
            Map(x => x.Flag);
            Map(x => x.AppName).UniqueKey("usertoken");
        }
    }
}
