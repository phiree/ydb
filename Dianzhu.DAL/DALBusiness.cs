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
        public DALBusiness():base()
        {
            
        }
        //调用基类带参构造函数,避免初始化hibernatesession.
        public DALBusiness(string fortest):base(fortest)
        {
            
        }

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
        /// <summary>
        /// 全部已经启用的商铺
        /// </summary>
        /// <param name="ownerId"></param>
        public IList<Business> GetBusinessListByOwner(Guid ownerId)
        {
            return Session.QueryOver<Business>().Where(x => x.Owner.Id == ownerId).And(x => x.Enabled == true).List();
        }

        /// <summary>
        /// 根据页码和页数查询商家列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        public IList<Business> GetListByPage(int pageIndex, int pageSize, out long totalRecord)
        {
            IQuery qry = Session.CreateQuery("select b from Business b order by b.CreatedTime desc");
            IQuery qryTotal = Session.CreateQuery("select count(*) from Business b ");
            IList<Business> busList = qry.Future<Business>().Skip(pageIndex * pageSize).Take(pageSize).ToList();
            totalRecord = qryTotal.FutureValue<long>().Value;
            return busList;
        }
    }
}
