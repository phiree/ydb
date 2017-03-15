using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.BusinessResource.Application;
using Ydb.Membership.Application;
using Ydb.ApplicationService.ModelDto;
using Ydb.BusinessResource.DomainModel;
using Ydb.Membership.Application.Dto;

namespace Ydb.ApplicationService.Application.AgentService
{
    public class BusinessOwnerService
    {
        IBusinessService businessService;
        IDZMembershipService dzMembershipService;
        public BusinessOwnerService(IBusinessService businessService, IDZMembershipService dzMembershipService)
        {
            this.businessService = businessService;
            this.dzMembershipService = dzMembershipService;
        }

        public IList<MemberDto> GetBusinessOwnerListByArea(IList<string> areaIdList)
        {
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> memberIdList = businessList.Select(x => x.OwnerId.ToString()).ToList();
            IList<MemberDto> memberDtoList = dzMembershipService.GetUsersByIdList(memberIdList);
            return memberDtoList;
        }
    }
}
