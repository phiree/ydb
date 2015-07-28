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
            string sql = "select  distinct b.AreaBelongTo from Business b";
            IQuery query = Session.CreateQuery(sql);
           IList<Area> result= query.List<Area>();
           return result;
            
        }
        public IList<Business> GetBusinessInSameCity(Area area)
        {
            string sql = "select  b from Business   b"+
                         "   inner join b.AreaBelongTo  a  "
                         +"left join fetch b.CashTicketTemplates ct "
                          +"   where a.Code like '%" + area.Code.Substring(0,4)+"%'";
            
            IQuery query = Session.CreateQuery(sql);
            var result = query.List<Business>();
            
            return result;
        }
        public Business GetBusinessByPhone(string phone)
        {
            Business b = Session.QueryOver<Business>().Where(x => x.Phone == phone).SingleOrDefault();

            return b;
        }
        public Business GetBusinessByEmail(string email)
        {
            Business b = Session.QueryOver<Business>().Where(x => x.Email == email).SingleOrDefault();

            return b;
        }
    }
}
