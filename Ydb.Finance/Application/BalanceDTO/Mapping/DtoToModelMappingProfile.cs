using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using AutoMapper;

namespace Ydb.Finance.Application
{
   public class DtoToModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DtoToModelMappings"; }
        }

        [System.Obsolete("No Use")]
        protected override void Configure()
        {
            CreateMap<BalanceFlowDto, BalanceFlow>();
            CreateMap<BalanceTotalDto, BalanceTotal>();
            CreateMap<ServiceTypePointDto, ServiceTypePoint>();
            CreateMap<UserTypeSharePointDto, UserTypeSharePoint>();
        }
    }
}
