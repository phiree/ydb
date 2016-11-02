using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using AutoMapper;

namespace Ydb.Finance.Application
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
            Mapper.CreateMap<BalanceFlow, BalanceFlowDto>()
            .ForAllMembers(opt => opt.NullSubstitute(""));
            Mapper.CreateMap<BalanceTotal, BalanceTotalDto>()
            .ForAllMembers(opt => opt.NullSubstitute(""));
            Mapper.CreateMap<ServiceTypePoint, ServiceTypePointDto>()
            .ForAllMembers(opt => opt.NullSubstitute(""));
            Mapper.CreateMap<UserTypeSharePoint, UserTypeSharePointDto>()
            .ForAllMembers(opt => opt.NullSubstitute(""));
            Mapper.CreateMap<BalanceAccount, BalanceAccountDto>()
            .ForAllMembers(opt => opt.NullSubstitute(""));
        }
    }
}
