using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.BusinessResource.DomainModel;
using AutoMapper;

namespace Ydb.BusinessResource.Application
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
            Mapper.CreateMap<DZService,ServiceDto>()
             .ForMember(x => x.ServiceName, opt => opt.MapFrom(source => source.Name))
             .ForMember(x => x.AllowedPayType, opt => opt.MapFrom(source => source.AllowedPayType.ToString()))
             .ForMember(x => x.ChargeUnit, opt => opt.MapFrom(source => source.ChargeUnit.ToString ()))
             .ForMember(x => x.ServiceMode, opt => opt.MapFrom(source => source.ServiceMode.ToString()))
             .ForMember(x => x.ServiceBusinessId, opt => opt.MapFrom(source => source.Business.Id.ToString ()))
             .ForMember(x => x.ServiceBusinessName, opt => opt.MapFrom(source => source.Business.Name))
             .ForMember(x => x.ServiceBusinessPhone, opt => opt.MapFrom(source => source.Business.Phone))
             .ForMember(x => x.ServiceBusinessOwnerId, opt => opt.MapFrom(source => source.Business.OwnerId.ToString ()))
             .ForMember(x => x.ServiceTypeName, opt => opt.MapFrom(source => source.ServiceType.Name))
             .ForMember(x => x.ServiceTypeName, opt => opt.MapFrom(source => source.ServiceType.Name))
             .ForMember(x => x.ServiceTypeId, opt => opt.MapFrom(source => source.ServiceType.Id.ToString ()))
            .ForAllMembers(opt => opt.NullSubstitute(""));
        }
    }
}
