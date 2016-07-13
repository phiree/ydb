using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace Dianzhu.Model
{
    class DZMapper
    {
        public DZMapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ServiceOpenTimeForDay, ServiceOpenTimeForDaySnapShotForOrder>());
        }
    }
}
