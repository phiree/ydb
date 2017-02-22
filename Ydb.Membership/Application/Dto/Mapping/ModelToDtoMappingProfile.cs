using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using AutoMapper;

namespace Ydb.Membership.Application
{
    public class ModelToDtoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ModelToDtoMappings"; }
        }

        /// <summary>
        /// ModelToDto映射配置
        /// </summary>
        //[System.Obsolete("No Use")]
        protected override void Configure()
        {
            Mapper.CreateMap<DZMembership, Dto.MemberDto>()
                  .ForMember(x => x.UserType, opt => opt.MapFrom(source => source.UserType.ToString()))
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<DZMembershipCustomerService, Dto.DZMembershipCustomerServiceDto>()
            .ForAllMembers(opt => opt.NullSubstitute(""));

            Mapper.CreateMap<DZMembershipImage, Dto.DZMembershipImageDto>()
                  .ForMember(x => x.ImageType, opt => opt.MapFrom(source => source.ImageType.ToString()))
            .ForAllMembers(opt => opt.NullSubstitute(""));
        }
    }
}
