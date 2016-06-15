﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping.Finance
{
    public class SharePointMapping:ClassMap<Model.Finance.SharePoint>
    {
        public SharePointMapping()
        {
            Id(x => x.Id);//.GeneratedBy.Assigned();
            References<Model.DZMembership>(x => x.Membership);
            Map(x => x.Point);
        }
    }
}
