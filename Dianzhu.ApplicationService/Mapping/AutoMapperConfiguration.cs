using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Mapping
{
    public class AutoMapperConfiguration
    {
        


        public static Action<IConfiguration> AutoMapperApplicationService= new Action<IConfiguration>(x =>
        {
            x.AddProfile<DtoToModelMappingProfile>();
            x.AddProfile<ModelToDtoMappingProfile>();
            //... more profiles
        });
    
    }
}
