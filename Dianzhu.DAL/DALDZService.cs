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
        public IList<DZService> SearchService(decimal priceMin,decimal priceMax,Guid serviceTypeId,DateTime preOrderTime, int pageindex, int pagesize, out int totalRecord)
        {
            string queryStr = "select service "
                               + " from DZService as service "
                               + " inner join service.OpenTimes as opentime" +
                                    " with opentime.DayOfWeek=" + (int)preOrderTime.DayOfWeek
                              + " inner join opentime.OpenTimeForDay as opentimeday"
                                    + " with  " + (preOrderTime.Hour * 60 + preOrderTime.Minute) + " between opentimeday.PeriodStart and opentimeday.PeriodEnd";
            string where = " where service.UnitPrice between " + priceMin + " and  " + priceMax;
            if (serviceTypeId != Guid.Empty)
            {
                where+= " and service.ServiceType.Id='" + serviceTypeId + "'";
            }
            where += " and service.Enabled=true";

            //var totalquery = Session.QueryOver<DZService>()
            //.
            Console.WriteLine(queryStr + where);
            // .Where(Restrictions.On<DZService>(x => x.Name).IsLike(string.Format("%{0}%", keywords))
            // || Restrictions.On<DZService>(x => x.Description).IsLike(string.Format("%{0}%", keywords))
            // ); 
            IQuery query = Session.CreateQuery(queryStr + where);
            totalRecord = query.List().Count;

            var result = query.List<DZService>()
                .Skip(pageindex * pagesize).Take(pagesize);

            //query.wrap
            return result.ToList();
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
