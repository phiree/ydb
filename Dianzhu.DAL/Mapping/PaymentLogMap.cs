﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
using Ydb.Common;

namespace Dianzhu.DAL.Mapping
{
    public class PaymentLogMap : ClassMap<PaymentLog>
    {
        public PaymentLogMap() { 
            Id(x=>x.Id);
            Map(x=>x.ApiString).Length(1000);
            Map(x => x.PaymentId);
            Map(x => x.PaylogType).CustomType<enum_PaylogType>();
            Map(x => x.LogTime);
            Map(x => x.PayAmount);
            Map(x => x.PayType).CustomType<enum_PayType>();
        }
    }
}
