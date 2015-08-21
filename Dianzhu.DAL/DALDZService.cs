using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALDZService : DALBase<DZService>
    {
        
        public IList<DZService> GetList(Guid businessId, string serviceCode, int pageindex, int pagesize, out int totalRecord)
        {
            string where = "s.Business.Id='" + businessId + "'";
            //if(serviceCode!=null && serviceCode!=string.Empty)
            //{
            where += " and s.ServiceType.Code='" + serviceCode + "'";
            //}
            return  GetList("select s from DZService s where "
                +where +" order by s.LastModifiedTime desc",
                pageindex, pagesize, out totalRecord);
        }
        
    }
}
