using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALBusiness :DALBase<Business>
    {
       

        public void CreateBusinessAndUser(string code)
        {
            throw new NotImplementedException();
        }
        public IList<Area> GetAreasOfBusiness()
        {
            string sql = "select distinct a from Area a inner join Business b";
            IQuery query = Session.CreateQuery(sql);
           IList<Area> result= query.List<Area>();
           return result;
            
        }
        public IList<Business> GetBusinessInSameCity(Area area)
        {
            string sql = "select  b from Business b  inner join Area a on b.area_id=a.id"+
                         "where left(a.code,4)='" + area.Code.Substring(0,4)+"'";
            IQuery query = Session.CreateSQLQuery(sql);
            IList<Business> result = query.List<Business>();
            return result;
        }
    }
}
