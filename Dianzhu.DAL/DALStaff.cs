using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALStaff : DALBase<Staff>
    {
       
        public IList<Staff> GetList(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecord)
        {
            string where = "s.Belongto.Id='" + businessId + "'";
            if (serviceTypeId != Guid.Empty && serviceTypeId != null)
            {
                where += " and s.ServiceType.Id='" + serviceTypeId + "'";
            }
            return GetList("select s from Staff s where " + where, pageindex, pagesize, out totalRecord);
        }
        
    }
}
