using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.BusinessResource.Application
{
    public class ServiceDto
    {
        public virtual Guid id { get; set; }
        public virtual DateTime CreatedTime { get; set; }
        public virtual string ServiceName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsCompensationAdvance { get; set; }
        public virtual decimal MinPrice { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual string ChargeUnit { get; set; }

        public virtual decimal DepositAmount { get; set; }
        public virtual decimal CancelCompensation { get; set; }
        public virtual int OverTimeForCancel { get; set; }
        public virtual string ServiceMode { get; set; }

        public virtual string ServiceTypeName { get; set; }
        public virtual string ServiceTypeFullName { get; set; }
        public virtual string ServiceTypeId { get; set; }

        public virtual string ServiceBusinessId { get; set; }

        public virtual string ServiceBusinessOwnerId { get; set;}

        public virtual string ServiceBusinessOwnerName { get; set; }

        public virtual string ServiceBusinessName { get; set; }
        public virtual string ServiceBusinessPhone { get; set; }
        public virtual bool IsForBusiness { get; set; }

        public virtual bool Enabled { get; set; }
        public virtual DateTime EnabledTime { get; set; }
        public virtual string EnabledMemo { get; set; }
        public virtual string AllowedPayType { get; set; }
        public virtual int OrderDelay { get; set; }
        public virtual string Scope { get; set; }
        public virtual IList<string> MaxOrdersPerDay { get; set; }

    }
}
