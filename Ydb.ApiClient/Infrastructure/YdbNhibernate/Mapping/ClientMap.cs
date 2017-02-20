using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.ApiClient.DomainModel;
using Ydb.Common;

namespace Ydb.ApiClient.Infrastructure.YdbNhibernate.Mapping
{
    public class ClientMap : ClassMap<  Client>
    {
        public ClientMap()
        {
            Id(x => x.Id);
            Map(x => x.Secret);
            Map(x => x.Name);
            Map(x => x.ApplicationType).CustomType< ApplicationTypes>();
            Map(x => x.Active);
            Map(x => x.RefreshTokenLifeTime);
            Map(x => x.AllowedOrigin);
        }
    }
}
