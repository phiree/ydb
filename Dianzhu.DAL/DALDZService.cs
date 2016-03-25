using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;
using NHibernate.Transform;

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
            string where = "s.Business.Id='" + businessId + "' and s.IsDeleted=false";
            return  GetList("select s from DZService s where "
                +where +" order by s.LastModifiedTime desc",
                pageindex, pagesize, out totalRecord);
        }
        public IList<DZService> GetOtherList(Guid businessId,Guid serviceId, int pageindex, int pagesize, out int totalRecord)
        {
            string where = "s.Business.Id='" + businessId + "' and s.Id!='"+serviceId+"' and s.IsDeleted=false";
            return GetList("select s from DZService s where "
                + where + " order by s.LastModifiedTime desc",
                pageindex, pagesize, out totalRecord);
        }
        public IList<DZService> SearchService(decimal priceMin,decimal priceMax,string typeId, DateTime datetime, int pageindex, int pagesize, out int totalRecord)
        {
            //var totalquery = Session.QueryOver<DZService>()
            // // .Where(x => x.Name.Contains(keywords) || x.Description.Contains(keywords));
            // .Where(Restrictions.On<DZService>(x => x.Name).IsLike(string.Format("%{0}%", keywords))
            // || Restrictions.On<DZService>(x => x.Description).IsLike(string.Format("%{0}%", keywords))
            // );

            //totalRecord = totalquery.RowCount();

            //var result = totalquery
            //    .Skip(pageindex * pagesize).Take(pagesize);

            //return result.List();

            int times = datetime.Hour * 60 + datetime.Minute;
            string select = @"SELECT d FROM dzservice d 
                                LEFT JOIN serviceopentime st ON st.DZService_id = d.Id 
                                LEFT JOIN serviceopentimeforday std ON std.ServiceOpenTime_id = st.Id ";
            string where = @" d.UnitPrice >= " + priceMin + " AND d.UnitPrice <= " + priceMax + " AND d.ServiceType_id = '" + typeId + "' AND st.DayOfWeek = " + datetime.DayOfWeek + " AND std.PeriodStart <= " + times + " AND std.PeriodEnd >= " + times;
            return GetList(select + where + " order by s.LastModifiedTime desc", pageindex, pagesize,out totalRecord);
        }

        public DZService GetOneByBusAndId(Business business, Guid svcId)
        {
            return Session.QueryOver<DZService>().Where(x => x.Id == svcId).And(x => x.Business == business).And(x=>x.IsDeleted==false).SingleOrDefault();
        }

        public int GetSumByBusiness(Business business)
        {
            return Session.QueryOver<DZService>().Where(x => x.Business == business).And(x => x.IsDeleted == false).RowCount();
        }
    }
}
