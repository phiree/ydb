using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.BusinessResource.DomainModel;

using AutoMapper;
using Ydb.Order.DomainModel;
using Ydb.ApplicationService.ModelDto;

namespace Ydb.ApplicationService
{
    public class ModelToDtoMappingProfileCrossDomain : Profile
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

            Mapper.CreateMap<DZService,ServiceSnapShot>()
            .ForAllMembers(opt => opt.NullSubstitute(""));


            Mapper.CreateMap<ServiceOrder, ServiceOrderDto>()
            .ForMember(x => x.GetStatusTitleFriendly, opt => opt.MapFrom(source => source.GetStatusTitleFriendly(source.OrderStatus)))
            .ForAllMembers(opt => opt.NullSubstitute(""));
        }
    }
}
