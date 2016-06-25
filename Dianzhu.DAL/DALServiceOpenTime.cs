using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALServiceOpenTime :NHRepositoryBase<ServiceOpenTime,Guid>,IDAL.IDALServiceOpenTime
    {
        

        
    }
    public class DALServiceOpenTimeForDay : NHRepositoryBase<ServiceOpenTimeForDay,Guid>,IDAL.IDALServiceOpenTimeForDay
    {
        
        


    }
}
