using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model.Enums;
namespace Dianzhu.DAL.Mapping.Finance
{
    public class DefaultSharePointMapping:ClassMap<Model.Finance.DefaultSharePoint>
    {
        public DefaultSharePointMapping()
        {
            Id(x => x.Id);//.GeneratedBy.Assigned();
            
            Map(x => x.Point);
            Map(x => x.UserType);
        }
    }
}
