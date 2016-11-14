using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NHibernate;

using System.Linq;

using Ydb.Common.Specification;

using Ydb.Common.Repository;
using Ydb.BusinessResource.DomainModel;
 
using System.Device.Location;
using Newtonsoft.Json;

namespace Ydb.BusinessResource.Infrastructure.Repository
{
    public class RepositoryDZService : NHRepositoryBase<DZService, Guid>, IRepositoryDZService
    {
        public IList<DZService> GetList(Guid businessId, int pageindex, int pagesize, out int totalRecord)
        {
            long lnTotalRecord;
            IList<DZService> serviceList = Find(x => x.Business.Id == businessId, pageindex, pagesize, out lnTotalRecord);
            totalRecord = (int)lnTotalRecord;
            return serviceList;

        }
        public IList<DZService> GetOtherList(Guid businessId, Guid serviceId, int pageindex, int pagesize, out int totalRecord)
        {
            long lnTotalRecord;

            IList<DZService> serviceList = Find(x => x.Business.Id == businessId && x.Id == serviceId && x.IsDeleted == false, pageindex, pagesize, out lnTotalRecord);
            totalRecord = (int)lnTotalRecord;
            return serviceList;

        }
        public IList<DZService> SearchService(string name, decimal priceMin, decimal priceMax, Guid serviceTypeId, DateTime preOrderTime, double lng, double lat, int pageindex, int pagesize, out int totalRecord)
        {
            string queryStr = "select service "
                           + " from DZService as service "
                           + " inner join service.OpenTimes as opentime" +
                                " with opentime.DayOfWeek=" + (int)preOrderTime.DayOfWeek
                          + " inner join opentime.OpenTimeForDay as opentimeday"
                                + " with  " + (preOrderTime.Hour * 60 + preOrderTime.Minute) + " between opentimeday.PeriodStart and opentimeday.PeriodEnd";
            string where = " where 1=1 ";
            if (priceMin >= 0 && priceMax > 0 && priceMin < priceMax)
            {
                where += " and service.UnitPrice between " + priceMin + " and  " + priceMax;
            }
            if (name.Trim() != string.Empty)
            {
                where += " and service.Name like '%" + name + "%'";
            }
            if (serviceTypeId != Guid.Empty)
            {
                where += " and service.ServiceType.Id='" + serviceTypeId + "'";
            }
            where += " and service.Enabled=true";

            //var totalquery = Session.QueryOver<DZService>()
            //.
            Console.WriteLine(queryStr + where);
            // .Where(Restrictions.On<DZService>(x => x.Name).IsLike(string.Format("%{0}%", keywords))
            // || Restrictions.On<DZService>(x => x.Description).IsLike(string.Format("%{0}%", keywords))
            // ); 

            IList<DZService> list;

            IQuery query = session.CreateQuery(queryStr + where);
            totalRecord = query.List().Count;

            var result = query.List<DZService>()
                .Skip(pageindex * pagesize).Take(pagesize);

            list = result.ToList();


            //按地区筛选
            IList<DZService> finalList = new List<DZService>();
            foreach (DZService item in list)
            {
                GeoCoordinate c1 = new GeoCoordinate(lat, lng);

                BusinessArea area = JsonConvert.DeserializeObject<BusinessArea>(item.BusinessAreaCode);
                GeoCoordinate c2 = new GeoCoordinate(area.serPointCirle.lat, area.serPointCirle.lng);

                double distanceInMeter = c1.GetDistanceTo(c2);

                if (distanceInMeter <= area.serPointCirle.radius)
                {
                    finalList.Add(item);
                }
            }

            return finalList;
        }

        public DZService GetOneByBusAndId(Business business, Guid svcId)
        {
            return FindOne(x => x.Id == svcId && x.Business.Id == business.Id && x.IsDeleted == false);
        }

        public int GetSumByBusiness(Business business)
        {
            return (int)GetRowCount(x => x.Business.Id == business.Id && x.IsDeleted == false);
        }

    }

    public class BusinessArea
    {
        public BusinessAreaPoint serPointCirle { get; set; }
    }

    public class BusinessAreaPoint
    {
        public double lng { get; set; }
        public double lat { get; set; }
        public double radius { get; set; }
    }
}
