using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
   public class ServiceOrderStateChangeHisMap : ClassMap<ServiceOrderStateChangeHis>
    {
       public ServiceOrderStateChangeHisMap()
       {
            Id(x => x.Id);
            References<ServiceOrder>(x => x.Order);
            Map(x => x.Status).CustomType<Model.Enums.enum_OrderStatus>();
            Map(x => x.NewAmount);
            Map(x => x.OldAmount);
            Map(x => x.Remark);
            Map(x => x.LastUpdateTime);
            References<DZMembership>(x => x.UpdateController);
        }
    }
}
