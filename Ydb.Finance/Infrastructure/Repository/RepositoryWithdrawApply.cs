using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using NHibernate;
using Ydb.Common.Repository;

namespace Ydb.Finance.Infrastructure.Repository
{
    public class RepositoryWithdrawApply : NHRepositoryBase<WithdrawApply, Guid>, IRepositoryWithdrawApply
    {

    }
}
