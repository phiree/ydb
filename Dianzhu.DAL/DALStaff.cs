using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using System.Linq.Expressions;

namespace Dianzhu.DAL
{
    public class DALStaff : NHRepositoryBase<Staff,Guid>,IDAL.IDALStaff
    {
       
        
        public int GetEnableSum(Business business)
        {
            return (int) GetRowCount(x => x.Belongto.Id == business.Id && x.Enable);
            
        }
        

        public IList<Staff> GetAllListByBusiness(Business business)
        {
            return Find(x => x.Belongto == business);
        }
        
    }
}
