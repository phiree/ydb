﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Ydb.Membership.Application
{
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Dto映射配置
        /// </summary>
       
        public static Action<IConfiguration> AutoMapperMembership= new Action<IConfiguration>(x =>
        {
            x.AddProfile<DtoToModelMappingProfile>();
            x.AddProfile<ModelToDtoMappingProfile>();
            //... more profiles
        });
    }
}
