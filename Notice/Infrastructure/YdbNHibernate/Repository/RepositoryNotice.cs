using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Notice.DomainModel.Repository;
using Ydb.Common.Repository;
using M = Ydb.Notice.DomainModel;

namespace Ydb.Notice.Infrastructure.YdbNHibernate.Repository
{
    public class RepositoryNotice : YdbNHibernate.Repository.NHRepositoryBase<M.Notice, Guid>, IRepositoryNotice
    {
    }
}