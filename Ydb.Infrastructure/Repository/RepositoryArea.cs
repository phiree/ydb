using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ydb.Common.Domain;
using Ydb.Common.Repository;

using NHibernate;

namespace Ydb.Infrastructure.Repository
{
    public class RepositoryArea : NHRepositoryBase<Area, int>, IRepositoryArea
    { 
    }
}
