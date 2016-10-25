using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Ydb.Common.Repository;
using Ydb.Finance.DomainModel;

namespace Ydb.Finance.DomainModel
{
    internal interface IRepositoryBalanceTotal : IRepository<BalanceTotal, Guid>
    {
        BalanceTotal GetOneByUserId(string UserId);

        IList<BalanceTotal> GetAll();
    }
}
