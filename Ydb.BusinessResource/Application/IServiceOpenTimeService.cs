using System;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.BusinessResource.Application
{
    public interface IServiceOpenTimeService
    {
        ServiceOpenTime GetOne(Guid id);
        void Update(ServiceOpenTime sot);
    }
}