using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALStaff : IDAL.IDALStaff
    {
        IDAL.IDALBase<Staff> dalBase = null;
        public IDAL.IDALBase<Staff> DalBase
        {
            get { return new DalBase<Staff>(); }
            set { dalBase = value; }
        }
        public IList<Staff> GetList(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecord)
        {
            string where = "s.Business.Id='" + businessId + "'";
            if (serviceTypeId != Guid.Empty || serviceTypeId != null)
            {
                where += " and s.ServiceType'" + serviceTypeId + "'";
            }
            return dalBase.GetList("select s from Staff s where " + where, pageindex, pagesize, out totalRecord);
        }
        
    }
}
