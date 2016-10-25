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
    internal class RepositoryBalanceTotal : NHRepositoryBase<BalanceTotal, Guid>, IRepositoryBalanceTotal
    {
        public RepositoryBalanceTotal(ISession session) : base(session)
        {

        }

        public BalanceTotal GetOneByUserId(string UserId)
        {
            var result = FindOne(x => x.UserId == UserId);
            return result;
        }

        public IList<BalanceTotal> GetAll()
        {
            return Find(x => true);
        }

        
    }
}
