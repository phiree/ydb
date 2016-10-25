using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common.Domain;

namespace Ydb.Finance.Application
{
    public class ServiceTypePointDto : Entity<Guid>
    {
        public virtual string ServiceTypeId { get; set; }
        public virtual decimal Point { get; set; }
    }
}
