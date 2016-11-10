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

        /// <summary>
        /// DtoToModel映射配置
        /// </summary>
        //[System.Obsolete("No Use")]
        protected override void Configure()
        {
            Mapper.CreateMap<BalanceFlowDto, BalanceFlow>();
            Mapper.CreateMap<BalanceTotalDto, BalanceTotal>();
            Mapper.CreateMap<ServiceTypePointDto, ServiceTypePoint>();
            Mapper.CreateMap<UserTypeSharePointDto, UserTypeSharePoint>();
            Mapper.CreateMap<BalanceAccountDto, BalanceAccount>();
            Mapper.CreateMap<WithdrawApplyDto, WithdrawApply>();
        }
    }
}
