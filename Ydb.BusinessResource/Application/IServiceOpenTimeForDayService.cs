using System;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.BusinessResource.Application
{
    public interface IServiceOpenTimeForDayService
    {
        void Delete(ServiceOpenTimeForDay sotForDay);
        ServiceOpenTimeForDay GetOne(Guid id);
        void Update(ServiceOpenTimeForDay sotForDay);
    }
}