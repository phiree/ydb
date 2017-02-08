using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ydb.BusinessResource.DomainModel;

using NHibernate;

namespace Ydb.BusinessResource.Infrastructure.YdbNHibernate.Repository
{
    public class RepositoryServiceOpenTime : NHRepositoryBase<ServiceOpenTime, Guid>, IRepositoryServiceOpenTime
    {       
        
    }
    public class RepositoryServiceOpenTimeForDay: NHRepositoryBase<ServiceOpenTimeForDay, Guid>, IRepositoryServiceOpenTimeForDay
    {

    }
}
