using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class PaymentMap : ClassMap<Payment>
    {
        public PaymentMap()
        {
            Id(x => x.Id);
            Map(x => x.Amount);
            Map(x => x.CreatedTime);
            Map(x => x.LastUpdateTime);
            References<ServiceOrder>(x => x.Order);
            Map(x => x.PayTarget);
            Map(x => x.Status);
         

        }
    }
  

}
