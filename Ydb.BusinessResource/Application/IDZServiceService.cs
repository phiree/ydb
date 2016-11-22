using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Application;
using Ydb.Common.Specification;

namespace Ydb.BusinessResource.Application
{
    public interface IDZServiceService
    {
        ActionResult<ServiceOpenTimeForDay> AddWorkTime(string storeId, string serviceId, DayOfWeek weekday, string periodStart, string periodEnd, int maxOrder, string tag);
         void Delete(DZService dz);
        IList<DZService> GetAll();
        ServiceDto GetOne(Guid serviceId);
        DZService GetOneByBusAndId(Business business, Guid svcId);
        IList<DZService> GetOtherServiceByBusiness(Guid businessId, Guid serviceId, int pageindex, int pagesize, out int totalRecords);
        ServiceDto GetService(Guid storeID, Guid serviceID);
        IList<DZService> GetServiceByBusiness(Guid businessId, int pageindex, int pagesize, out int totalRecords);
        IList<ServiceDto> GetServices(TraitFilter filter, Guid typeId, string strName, string introduce, decimal startAt, Guid storeID);
        long GetServicesCount(Guid typeId, string strName, string introduce, decimal startAt, Guid storeID);
        IList<DZTag> GetServiceTags(Guid serviceId);
        IList<ServiceType> GetServiceTypeListByBusiness(Guid businessId);
        int GetSumByBusiness(Business business);
        ServiceOpenTimeDto GetTimeDto(Guid serviceId, DateTime targetTime);
        void Save(DZService service);
        void SaveOrUpdate(DZService service, out ValidationResult validationResult);
        IList<DZService> SearchService(string name, decimal priceMin, decimal priceMax, Guid typeId, DateTime datetime, double lng, double lat, int pageIndex, int pagesize, out int total);
        void Update(DZService service);
        /// <summary>
        /// 获取一个服务的工作时间项
        /// </summary>
        /// <param name="storeID">商家</param>
        /// <param name="serviceID"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <returns></returns>
        IList<ServiceOpenTimeForDay> GetWorkTimes(string storeID, string serviceID, DayOfWeek? dayOfWeek, string timeBegin, string timeEnd);
        /// <summary>
        /// todo:refactor: opentime和 opentimeforday应该是 valueobject 不应该有id.必须通过 root dzservice来操作.
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <returns></returns>
        ServiceOpenTimeForDay GetWorkitem(string storeID, string serviceID, string workTimeID);



    }
}