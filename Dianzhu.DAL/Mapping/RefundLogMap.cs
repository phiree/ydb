using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class RefundLogMap : ClassMap<RefundLog>
    {
        public RefundLogMap(){
            Id(x=>x.Id);
            Map(x=>x.ApiString).Length(1000);
            Map(x => x.RefundId);
            Map(x => x.RefundAmount);
            Map(x => x.PaylogType).CustomType<Model.Enums.enum_PaylogType>();
            Map(x => x.LogTime);
            Map(x => x.PayType).CustomType<Model.Enums.enum_PayType>();
        }
    }
}
