using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using Ydb.Finance.DomainModel.Enums;
using FluentNHibernate.Mapping;
using Ydb.Common.Domain;
 
namespace Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping
{
    public class BalanceFlowMap:ClassMap<BalanceFlow>
    {
        public BalanceFlowMap()
        {
            Id(x => x.Id);
            Map(x => x.AccountId); 
            Map(x => x.Amount);
            Map(x => x.FlowType).CustomType<FlowType>();
            Map(x => x.OccurTime);
            Map(x => x.RelatedObjectId);
            Map(x => x.SerialNo);
            Map(x => x.Income);
            Map(x => x.AmountTotal);
            Map(x => x.Rate);
            Map(x => x.AmountView);
        }
    }
}
