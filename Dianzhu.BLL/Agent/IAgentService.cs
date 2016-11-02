using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
namespace Dianzhu.BLL.Agent
{
    public interface IAgentService
    {
        MemberDto GetAreaAgent(Dianzhu.Model.Area area);
    }
}
