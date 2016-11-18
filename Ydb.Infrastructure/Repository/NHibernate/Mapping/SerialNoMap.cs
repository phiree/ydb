using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Ydb.Common.Domain;

namespace Ydb.Infrastructure.Repository.NHibernate.Mapping
{
    public class SerialNoMap : ClassMap<SerialNo>
    {
        public SerialNoMap()
        {
            Id(x => x.Id);
            Map(x => x.SerialKey);
            Map(x => x.SerialValue);
        }
    }
}
