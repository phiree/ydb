using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.DomainModel;
namespace Dianzhu.BLL.Agent
{
    public class AgentService : IAgentService
    {
       MemberDto dzMembership = null;
        public MemberDto GetAreaAgent( Area area)
        {
            return dzMembership;
        }
    }
}
