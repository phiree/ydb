using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using FluentNHibernate.Mapping;
using Ydb.Common.Domain;
namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate.Mapping
{
    public class ReceptionStatusMapping:ClassMap<ReceptionStatus>
    {
        public ReceptionStatusMapping()
        {
            Id(x => x.Id);
            Map(x => x.CustomerId);
            Map(x => x.CustomerServiceId);
            Map(x => x.LastUpdateTime);
        }
    }
}
