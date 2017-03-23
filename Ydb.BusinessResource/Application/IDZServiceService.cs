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
        ActionResult<ServiceOpenTimeForDay> AddWorkTime(string storeId, string serviceId, DayOfWeek weekday, string timeBegin, string timeEnd, int maxOrder, string tag);
        void Delete(DZService dz);
        IList<DZService> GetAll();
        ServiceDto GetOne(Guid serviceId);
    
        [Obsolete("为了兼容性,暂时返回领域对象.")]
        DZService GetOne2(Guid serviceId);
        DZService GetOneByBusAndId(Business business, Guid svcId);
        IList<DZService> GetOtherServiceByBusiness(Guid businessId, Guid serviceId, int pageindex, int pagesize, out int totalRecords);
        ServiceDto GetService(Guid storeID, Guid serviceID);
        IList<DZService> GetServiceByBusiness(Guid businessId, int pageindex, int pagesize, out int totalRecords);
        IList<ServiceDto> GetServices(TraitFilter filter, Guid typeId, string strName, string introduce, decimal startAt, Guid storeID);
        long GetServicesCount(Guid typeId, string strName, string introduce, decimal startAt, Guid storeID);
        IList<DZTag> GetServiceTags(Guid serviceId);
        IList<ServiceType> GetServiceTypeListByBusiness(Guid businessId);
        int GetSumByBusiness(Business business);
        ServiceOpenTimeForDay GetWorkTime(Guid serviceId, DateTime targetTime);
        ServiceOpenTimeForDay GetWorkitem(string storeID, string serviceID, string workTimeID);
        ServiceOpenTimeForDay GetWorkitem( string workTimeID);
        IList<ServiceOpenTimeForDay> GetWorkTimes(string storeID, string serviceID, DayOfWeek? dayOfWeek, string timeBegin, string timeEnd);

        ActionResult<ServiceOpenTimeForDay> ModifyWorkTimeDay(string serviceId, DayOfWeek dayOfWeek, string workTimeId,
            string timeBegin, string endtime, int? maxOrder, bool? isOpen, string tag);
        void DeleteWorkTime(string serviceId, string workTimeId);
       void Save(DZService service);
        void SaveOrUpdate(DZService service, out ValidationResult validationResult);

        IList<DZService> SearchService(string name, decimal priceMin, decimal priceMax, Guid typeId, DateTime datetime,
            double lng, double lat, int pageIndex, int pagesize, out int total);
        void Update(DZService service);

        /// <summary>
        /// 封停/解封服务
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="enable"></param>
        /// <param name="memo"></param>
        void EnabledDZService(string serviceId, bool enable, string memo);

        /// <summary>
        /// 获取代理所在区域的服务区分是否封停
        /// </summary>
        /// <param name="areaIdList"></param>
        /// <returns></returns>
        IList<ServiceDto> GetServicesByArea(IList<string> areaIdList);

        /// <summary>
        /// 获取该服务每天的工作时间
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        ServiceOpenTimeDto GetOpenTimeByWeek(Guid serviceId, DayOfWeek dayOfWeek);

        /// <summary>
        /// 封停/解封店铺
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="enable"></param>
        /// <param name="memo"></param>
        void EnabledDZService(Guid dzServiceId, bool enable, string memo);
    }
}