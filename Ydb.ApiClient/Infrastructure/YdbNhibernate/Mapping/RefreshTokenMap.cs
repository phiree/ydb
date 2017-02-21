using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.ApiClient.DomainModel;
using Ydb.Common;

namespace Ydb.ApiClient.Infrastructure.YdbNhibernate.Mapping
{
    public class RefreshTokenMap : ClassMap<RefreshToken>
    {
        public RefreshTokenMap()
        {
            Id(x => x.Id);
            Map(x => x.Subject);
            Map(x => x.ClientId);
            Map(x => x.IssuedUtc);
            Map(x => x.ExpiresUtc);
            Map(x => x.ProtectedTicket);
        }
    }
}
