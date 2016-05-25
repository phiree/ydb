using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Agent
{
    public class AgentService : IAgentService
    {
        Dianzhu.Model.DZMembership dzMembership = null;
        public Dianzhu.Model.DZMembership GetAreaAgent(Dianzhu.Model.Area area)
        {
            return dzMembership;
        }
    }
}
