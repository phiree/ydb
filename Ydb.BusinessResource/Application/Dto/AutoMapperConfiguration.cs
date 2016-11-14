using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Ydb.BusinessResource.Application
{
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Dto映射配置
        /// </summary>
       
        public static Action<IConfiguration> AutoMapperBusinessResource= new Action<IConfiguration>(x =>
        {
            
            x.AddProfile<ModelToDtoMappingProfile>();
            //... more profiles
        });
    }
}
