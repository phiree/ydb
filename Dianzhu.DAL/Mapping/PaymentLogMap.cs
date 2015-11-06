using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class PaymentLogMap : ClassMap<PaymentLog>
    {
        public PaymentLogMap() { 
            Id(x=>x.Id);
            Map(x=>x.Pames).Length(1000);
            References<ServiceOrder>(x => x.ServiceOrder);
            Map(x => x.Type).Length(20);
            Map(x => x.LastTime);
        }
    }
}
