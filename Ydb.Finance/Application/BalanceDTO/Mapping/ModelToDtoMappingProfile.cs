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

        [System.Obsolete("No Use")]
        protected override void Configure()
        {
            CreateMap<BalanceFlow, BalanceFlowDto>();
            CreateMap<BalanceTotal, BalanceTotalDto>();
            CreateMap<ServiceTypePoint, ServiceTypePointDto>();
            CreateMap<UserTypeSharePoint, UserTypeSharePointDto>();
        }
    }
}
