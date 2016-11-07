using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Ydb.Finance.Application
{
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Dto映射配置
        /// </summary>
       
        public static Action<IConfiguration> AutoMapperFinance = new Action<IConfiguration>(x =>
        {
            x.AddProfile<DtoToModelMappingProfile>();
            x.AddProfile<ModelToDtoMappingProfile>();
            //... more profiles
        });
    }
}
