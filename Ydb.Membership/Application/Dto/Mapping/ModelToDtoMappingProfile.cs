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
            .ForAllMembers(opt => opt.NullSubstitute(""));
        }
    }
}
