using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;

namespace Ydb.Finance.DomainModel
{
    public interface IRepositoryWithdrawApply : IRepository<WithdrawApply, Guid>
    {

    }
}
