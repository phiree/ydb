using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Dianzhu.DAL.Mapping
{
    public class UserTokenMap : ClassMap<Dianzhu.Model.UserToken>
    {
        public UserTokenMap()
        {
            Id(x => x.Id);
            Map(x => x.UserID);
            Map(x => x.Token);
            Map(x => x.CreatedTime);
            Map(x => x.Flag);
        }
    }
}
