using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Application.Dto;

namespace Ydb.ApplicationService.Application.AgentService
{
    public interface IBusinessOwnerService
    {
        IList<MemberDto> GetBusinessOwnerListByArea(IList<string> areaIdList, bool isLocked);
    }
}
