using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping.Finance
{
    public class BalanceFlowMap:ClassMap<Model.Finance.BalanceFlow>
    {
        public BalanceFlowMap()
        {
            Id(x => x.Id);//.GeneratedBy.Assigned();
            Map(x => x.MemberId);
            Map(x => x.FlowType).CustomType<Dianzhu.Model.Finance.enumFlowType>();
            Map(x => x.Amount);
            Map(x => x.OccurTime);
            Map(x => x.RelatedObjectId);
            
        }
    }
}
