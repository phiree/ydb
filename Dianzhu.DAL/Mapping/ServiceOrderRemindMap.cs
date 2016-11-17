using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Ydb.Common;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
   public class ServiceOrderRemindMap : ClassMap<ServiceOrderRemind>
    {
       public ServiceOrderRemindMap()
       {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Content);
            Map(x => x.RemindTime);
            Map(x => x.CreateTime);
            Map(x => x.Open);
            Map(x => x.OrderId);
            Map(x => x.UserId);
        }
    }
}
