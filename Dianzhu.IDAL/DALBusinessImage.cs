﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.IDAL
{
    public interface IDALBusinessImage:IDAL.IRepository<BusinessImage,Guid>
    {


          BusinessImage FindBusImageByName(string imgName);
      
    }
}
