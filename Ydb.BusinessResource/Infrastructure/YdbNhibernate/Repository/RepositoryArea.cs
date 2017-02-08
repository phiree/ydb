using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ydb.BusinessResource.DomainModel;

using NHibernate;

namespace Ydb.BusinessResource.Infrastructure.YdbNHibernate.Repository
{
    public class RepositoryArea : NHRepositoryBase<Area, int>, IRepositoryArea
    { 
    }
}
