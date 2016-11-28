using System;
using System.Collections.Generic;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.BusinessResource.Application
{
    public interface IServiceTypeService
    {
        IList<ServiceType> GetAll();
        IList<ServiceType> GetAllServiceTypes(Guid guidSuperID);
        ServiceType GetOne(Guid id);
        ServiceType GetOneByName(string name, int level);
        IList<ServiceType> GetTopList();
        void Save(ServiceType serviceType);
        void Update(ServiceType serviceType);
        void SaveList(List<ServiceType> list);
        void SaveOrUpdateList(List<ServiceType> list);
    }
}