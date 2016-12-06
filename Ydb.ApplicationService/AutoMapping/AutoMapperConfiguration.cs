using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Ydb.ApplicationService
{
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Dto映射配置
        /// </summary>
       
        public static Action<IConfiguration> AutoMapperCrossDomain= new Action<IConfiguration>(x =>
        {
            
            x.AddProfile<ModelToDtoMappingProfileCrossDomain>();
            //... more profiles
        });
    }
}
