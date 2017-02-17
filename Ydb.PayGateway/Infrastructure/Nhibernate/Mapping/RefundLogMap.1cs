using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
using Ydb.Common;

namespace Dianzhu.DAL.Mapping
{
    public class RefundLogMap : ClassMap<RefundLog>
    {
        public RefundLogMap(){
            Id(x=>x.Id);
            Map(x=>x.ApiString).Length(1000);
            Map(x => x.RefundId);
            Map(x => x.RefundAmount);
            Map(x => x.PaylogType).CustomType<enum_PaylogType>();
            Map(x => x.LogTime);
            Map(x => x.PayType).CustomType<enum_PayType>();
        }
    }
}
