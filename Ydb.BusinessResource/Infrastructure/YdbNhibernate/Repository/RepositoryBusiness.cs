using System;
using System.Collections.Generic;
using System.Linq.Expressions;
 
using NHibernate;
 
using System.Linq;
 
using Ydb.Common.Specification;
 
using Ydb.Common.Repository;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.BusinessResource.Infrastructure.YdbNHibernate.Repository
{
    public class RepositoryBusiness : NHRepositoryBase<Business,Guid>,IRepositoryBusiness
    {



        public IList<Area> GetDistinctAreasOfBusiness()
        {
            IList<Area> result;

            string sql = "select  distinct b.AreaBelongTo from Business b";
            IQuery query = session.CreateQuery(sql);
            result = query.List<Area>();

            return result;

        }
        public IList<Business> GetBusinessInSameCity(Area area)
        {
            IList<Business> result;

            string sql = "select  b from Business   b" +
                     "   inner join b.AreaBelongTo  a  "
                     + "left join fetch b.CashTicketTemplates ct "
                      + "   where a.Code like '%" + area.Code.Substring(0, 4) + "%'";

            IQuery query = session.CreateQuery(sql);
            result = query.List<Business>();


            return result;
        }
        public Business GetBusinessByPhone(string phone)
        {

            Business b = session.QueryOver<Business>().Where(x => x.Phone == phone).SingleOrDefault();

            return b;



        }
        public Business GetBusinessByEmail(string email)
        {


            Business b = session.QueryOver<Business>().Where(x => x.Email == email).SingleOrDefault();

            return b;



        }

        public Business GetBusinessByIdAndOwner(Guid Id, Guid ownerId)
        {



            Business business = session.QueryOver<Business>().Where(x => x.Id == Id).And(x => x.OwnerId == ownerId).SingleOrDefault();

            return business;


        }
        /// <summary>
        /// 全部已经启用的商铺
        /// </summary>
        /// <param name="ownerId"></param>
        public IList<Business> GetBusinessListByOwner(Guid ownerId)
        {



            var result = session.QueryOver<Business>().Where(x => x.OwnerId == ownerId).And(x => x.Enabled == true).List();

            return result;


        }

        /// <summary>
        /// 根据页码和页数查询商家列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <returns></returns>
        //public IList<Business> GetListByPage(int pageIndex, int pageSize, out long totalRecord)
        //{



        //    //IQuery qry = Session.CreateQuery("select b from Business b order by b.CreatedTime desc");
        //    IQuery qryTotal = session.CreateQuery("select count(*) from Business b ");
        //    //IList<Business> busList = qry.Future<Business>().Skip(pageIndex * pageSize).Take(pageSize).ToList();
        //    IList<Business> busList = session.QueryOver<Business>().Where(x => x.Enabled == true).OrderBy(x => x.CreatedTime).Desc.List();
        //    totalRecord = qryTotal.FutureValue<long>().Value;

        //    return busList;



        //}

        public int GetEnableSum(string memberId)
        {



            int result = session.QueryOver<Business>().Where(x => x.OwnerId.ToString() == memberId).And(x => x.Enabled == true).RowCount();


            return result;


        }

        public void SaveList(IList<Business> businesses)
        {
            foreach (Business b in businesses)
            {
                session.Save(b);
            }
        }
        public new Business FindById(Guid identityId)
        {
           var b= session.Get<Business>(identityId);

            NHibernateUtil.Initialize(b.AreaBelongTo);
            NHibernateUtil.Initialize(b.BusinessImages);
            return b;
        }
        public IList<Business> GetListByPage(int pageIndex, int pageSize, out long total)
        {
            var businessList = session.QueryOver<Business>()
               
                 .Fetch(x => x.AreaBelongTo).Eager
                .Skip((pageIndex-1)*pageSize).Take(pageSize)
                .List();
              total = session.QueryOver<Business>().RowCount();
            return businessList;
        }


    }
}
