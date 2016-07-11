using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace JSYK.Infrastructure.ClassMapper
{
    public  class DZMapper
    {
        public static void Init()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Dianzhu.Model.ServiceOpenTimeForDay, Dianzhu.Model.ServiceOpenTimeForDaySnapShotForOrder>());
        }
    }
}
