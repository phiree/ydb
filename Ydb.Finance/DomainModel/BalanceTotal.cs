using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.DomainModel
{
   internal class BalanceTotal : Entity<Guid>
    {
        public virtual string UserId { get; set; }
        public virtual decimal Total { get; set; }
    }
}
