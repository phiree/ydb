using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Application.Dto;
namespace Dianzhu.BLL.Agent
{
    public class AgentService : IAgentService
    {
       MemberDto dzMembership = null;
        public MemberDto GetAreaAgent(Dianzhu.Model.Area area)
        {
            return dzMembership;
        }
    }
}
