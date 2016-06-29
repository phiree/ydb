using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.IDAL;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALServiceOpenTime : NHRepositoryBase<ServiceOpenTime, Guid>, IDALServiceOpenTime//:DALBase<ServiceOpenTime>
    {
        public DALServiceOpenTime()//:base()
        {
            
        }
        //调用基类带参构造函数,避免初始化hibernatesession.
        //public DALServiceOpenTime(string fortest):base(fortest)
        //{
            
        //}
        
    }

    public class DALServiceOpenTimeForDay : NHRepositoryBase<ServiceOpenTimeForDay,Guid>,IDAL.IDALServiceOpenTimeForDay
    {
        


    }
}
