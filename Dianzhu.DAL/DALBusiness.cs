using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALBusiness : NHRepositoryBase<Business, Guid>, IDAL.IDALBusiness
    {


        public IList<Area> GetDistinctAreasOfBusiness()
        {
            IList<Area> result;
            using (var tr = Session.BeginTransaction())
            {
                string sql = "select  distinct b.AreaBelongTo from Business b";
                IQuery query = Session.CreateQuery(sql);
                result = query.List<Area>();
                tr.Commit();
            }
            return result;

        }
        public IList<Business> GetBusinessInSameCity(Area area)
        {
            IList<Business> result;
            using (var tr = Session.BeginTransaction())
            {
                string sql = "select  b from Business   b" +
                         "   inner join b.AreaBelongTo  a  "
                         + "left join fetch b.CashTicketTemplates ct "
                          + "   where a.Code like '%" + area.Code.Substring(0, 4) + "%'";

                IQuery query = Session.CreateQuery(sql);
                 result = query.List<Business>();

                tr.Commit();
            }

            return result;
        }
        public Business GetBusinessByPhone(string phone)
        {
            using (var tr = Session.BeginTransaction())
            {
                Business b = Session.QueryOver<Business>().Where(x => x.Phone == phone).SingleOrDefault();
                tr.Commit();
                return b;
            }
                

            
        }
        public Business GetBusinessByEmail(string email)
        {

            using (var tr = Session.BeginTransaction())
            {

                Business b = Session.QueryOver<Business>().Where(x => x.Email == email).SingleOrDefault();
                tr.Commit();
                return b; 
               
            }

        }

        public Business GetBusinessByIdAndOwner(Guid Id, Guid ownerId)
        {

            using (var tr = Session.BeginTransaction())
            {

                Business business = Session.QueryOver<Business>().Where(x => x.Id == Id).And(x => x.Owner.Id == ownerId).SingleOrDefault(); 
                tr.Commit();
                return business;
            }

        }
        /// <summary>
        /// 全部已经启用的商铺
        /// </summary>
        /// <param name="ownerId"></param>
        public IList<Business> GetBusinessListByOwner(Guid ownerId)
        {
            using (var tr = Session.BeginTransaction())
            {


                var result = Session.QueryOver<Business>().Where(x => x.Owner.Id == ownerId).And(x => x.Enabled == true).List();
                tr.Commit();
                return result; 
              
            }

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

            using (var tr = Session.BeginTransaction())
            {

                //IQuery qry = Session.CreateQuery("select b from Business b order by b.CreatedTime desc");
                IQuery qryTotal = Session.CreateQuery("select count(*) from Business b ");
                //IList<Business> busList = qry.Future<Business>().Skip(pageIndex * pageSize).Take(pageSize).ToList();
                IList<Business> busList = Session.QueryOver<Business>().Where(x => x.Enabled == true).OrderBy(x => x.CreatedTime).Desc.List();
                totalRecord = qryTotal.FutureValue<long>().Value;
                tr.Commit();
                return busList; 
             
            }

        }

        public int GetEnableSum(DZMembership member)
        {
            using (var tr = Session.BeginTransaction())
            {


                int result = Session.QueryOver<Business>().Where(x => x.Owner == member).And(x => x.Enabled == true).RowCount();
               
                tr.Commit();
                return result;
            }

        }

        public void SaveList(IList<Business> businesses)
        {
            foreach (Business b in businesses)
            {
                Session.Save(b);
            }
        }
    }
}
