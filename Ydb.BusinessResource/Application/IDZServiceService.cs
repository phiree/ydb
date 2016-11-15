using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;

namespace Ydb.BusinessResource.Application
{
    public interface IDZServiceService
    {
        void Delete(DZService dz);
        IList<DZService> GetAll();
        ServiceDto GetOne(Guid serviceId);
        DZService GetOneByBusAndId(Business business, Guid svcId);
        IList<DZService> GetOtherServiceByBusiness(Guid businessId, Guid serviceId, int pageindex, int pagesize, out int totalRecords);
        DZService GetService(Guid storeID, Guid serviceID);
        IList<DZService> GetServiceByBusiness(Guid businessId, int pageindex, int pagesize, out int totalRecords);
        IList<DZService> GetServices(TraitFilter filter, Guid typeId, string strName, string introduce, decimal startAt, Guid storeID);
        long GetServicesCount(Guid typeId, string strName, string introduce, decimal startAt, Guid storeID);
        IList<DZTag> GetServiceTags(DZService service);
        IList<ServiceType> GetServiceTypeListByBusiness(Guid businessId);
        int GetSumByBusiness(Business business);
        ServiceOpenTimeDto GetTimeDto(Guid serviceId, DateTime targetTime);
        void Save(DZService service);
        void SaveOrUpdate(DZService service, out ValidationResult validationResult);
        IList<DZService> SearchService(string name, decimal priceMin, decimal priceMax, Guid typeId, DateTime datetime, double lng, double lat, int pageIndex, int pagesize, out int total);
        void Update(DZService service);
    }
}