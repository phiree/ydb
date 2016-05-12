using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
 
namespace Dianzhu.DAL
{
    public interface IDALServiceOrder:IDAL.IRepository<ServiceOrder,Guid>
    { 
    }
}
