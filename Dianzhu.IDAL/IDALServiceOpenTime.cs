﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.IDAL
{
   public interface IDALServiceOpenTime:IRepository<Model.ServiceOpenTime,Guid>
    {
    }

    public interface IDALServiceOpenTimeForDay : IRepository<Model.ServiceOpenTimeForDay, Guid>
    {
    }
}
