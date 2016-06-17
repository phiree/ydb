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
    public class DALServiceOpenTimeForDay : DALBase<ServiceOpenTimeForDay>
    {
        public DALServiceOpenTimeForDay() : base()
        {

        }
        //调用基类带参构造函数,避免初始化hibernatesession.
        public DALServiceOpenTimeForDay(string fortest) : base(fortest)
        {

        }
        


    }
}
