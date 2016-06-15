﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class ClaimsDetailsMap : ClassMap<ClaimsDetails>
    {
        public ClaimsDetailsMap() {
            Id(x => x.Id);
            References(x => x.Claims);
            Map(x => x.Target).CustomType<Model.Enums.enum_ChatTarget>();
            Map(x => x.Context);
            Map(x => x.Amount);
            Map(x => x.ResourcesUrl).Length(1000);
            Map(x => x.CreatTime);
            References(x => x.Member);
            Map(x => x.LastUpdateTime);
        }
    }
}
