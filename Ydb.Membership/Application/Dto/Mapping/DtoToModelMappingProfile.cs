using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using AutoMapper;

namespace Ydb.Membership.Application
{
   public class DtoToModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DtoToModelMappings"; }
        }

        /// <summary>
        /// DtoToModel映射配置
        /// </summary>
        //[System.Obsolete("No Use")]
        protected override void Configure()
        {
            Mapper.CreateMap<Dto.MemberDto, DZMembership>();
        }
    }
}
