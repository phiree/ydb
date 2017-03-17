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
    public class BusinessOwnerService:IBusinessOwnerService
    {
        IBusinessService businessService;
        IDZMembershipService dzMembershipService;
        public BusinessOwnerService(IBusinessService businessService, IDZMembershipService dzMembershipService)
        {
            this.businessService = businessService;
            this.dzMembershipService = dzMembershipService;
        }

        public IList<MemberDto> GetBusinessOwnerListByArea(IList<string> areaIdList,bool isLocked)
        {
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> memberIdList = businessList.Select(x => x.OwnerId.ToString()).ToList();
            IList<MemberDto> memberDtoList = dzMembershipService.GetUsersByIdList(memberIdList);
            memberDtoList = memberDtoList.Where(x => x.IsLocked == isLocked).ToList();
            //RecoveryCode
            foreach (MemberDto m in memberDtoList)
            {
                m.RecoveryCode = businessList.Count(x => x.OwnerId == m.Id).ToString();
            }
            return memberDtoList;
        }


        public IList<Business> GetBusinessListByArea(IList<string> areaIdList, bool enable)
        {
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList,enable);
            IList<string> memberIdList = businessList.Select(x => x.OwnerId.ToString()).ToList();
            IList<MemberDto> memberDtoList = dzMembershipService.GetUsersByIdList(memberIdList);
            foreach (Business b in businessList)
            {
                b.OwnerName = memberDtoList.First(x => x.Id == b.OwnerId).UserName;
            }
            return businessList;
        }
    }
}
