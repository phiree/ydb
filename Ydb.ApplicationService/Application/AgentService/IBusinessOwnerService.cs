using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.Application;

namespace Ydb.ApplicationService.Application.AgentService
{
    public interface IBusinessOwnerService
    {
        IList<MemberDto> GetBusinessOwnerListByArea(IList<string> areaIdList, bool isLocked);
        IList<Business> GetBusinessListByArea(IList<string> areaIdList, bool enable);
        IList<ServiceDto> GetServiceListByArea(IList<string> areaIdList, bool enable);
    }
}
