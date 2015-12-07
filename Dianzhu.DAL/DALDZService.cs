using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Dianzhu.DAL
{
    public class DALDZService : DALBase<DZService>
    {
        public DALDZService() { }
        [Obsolete("just for test ,don't use it outside testing code.")]
        public DALDZService(string fortest):base(fortest)
        {
        }
        public IList<DZService> GetList(Guid businessId,  int pageindex, int pagesize, out int totalRecord)
        {
            string where = "s.Business.Id='" + businessId + "'";
            return  GetList("select s from DZService s where "
                +where +" order by s.LastModifiedTime desc",
                pageindex, pagesize, out totalRecord);
        }
        public IList<DZService> SearchService(string keywords, int pageindex, int pagesize, out int totalRecord)
        {
            //var totalquery = Session.QueryOver<DZService>()
            //   // .Where(x => x.Name.Contains(keywords) || x.Description.Contains(keywords));
            //   .Where(Restrictions.On<DZService>(x => x.Name).IsLike(string.Format("%{0}%", keywords))
            //   || Restrictions.On<DZService>(x => x.Description).IsLike(string.Format("%{0}%", keywords))
            //   );

            //totalRecord = totalquery.RowCount();

            //var result = totalquery
            //    .Skip(pageindex * pagesize).Take(pagesize);

            //return result.List();

            //string sql = "select  b from Business   b" +
            //             "   inner join b.AreaBelongTo  a  "
            //             + "left join fetch b.CashTicketTemplates ct "
            //              + "   where a.Code like '%" + keywords + "%'";
            string sql = "select s from DZService s " +
                         //"  left join fetch s.Business b " +
                         //" inner join b.business_abs " +
                         //" inner join s.servicetype " +
                         " where s.Name like '%" + keywords + "%' ";

            IQuery query = Session.CreateQuery(sql);
            var result = query.List<DZService>();
            totalRecord = result.Count();
            return result;
            //string selectstr = "select * from DZService "+
            //    " left join business on dzservice.Business_id = business.Business_Abs_id and dzservice.Business_id = business.Business_Abs_id "+
            //    " inner join business_abs on business.Business_Abs_id = business_abs.Id and business.Business_Abs_id = business_abs.Id "+
            //    " inner join servicetype on dzservice.ServiceType_id = servicetype.Id and dzservice.ServiceType_id = servicetype.Id ";
            //string wherestr = " where DZService.Name like '%"+ keywords + "%' "+
            //                    " or DZService.Description like '%" + keywords + "%'";

            //return GetList(selectstr + wherestr+ " order by DZService.LastModifiedTime desc", pageindex, pagesize, out totalRecord);
        }
        
    }
}
