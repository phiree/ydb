using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using FluentNHibernate.Mapping;

namespace Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping
{
    internal class UserTypeSharePointMap : ClassMap<UserTypeSharePoint>
    {
        public UserTypeSharePointMap()
        {
            Id(x => x.Id)
            Map(x => x.UserType);
            Map(x => x.Point);
        }
    }
}
