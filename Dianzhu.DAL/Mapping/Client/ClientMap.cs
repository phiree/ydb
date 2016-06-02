using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;

namespace Dianzhu.DAL.Mapping
{
    public class ClientMap : ClassMap<Dianzhu.Model.Client>
    {
        public ClientMap()
        {
            Id(x => x.Id);
            Map(x => x.Secret);
            Map(x => x.Name);
            Map(x => x.ApplicationType);
            Map(x => x.Active);
            Map(x => x.RefreshTokenLifeTime);
            Map(x => x.AllowedOrigin);
        }
    }
}
