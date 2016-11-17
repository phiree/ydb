using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.BusinessResource.Application
{
    public class ServiceDto
    {
        public virtual string ServiceName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsCompensationAdvance { get; set; }
        public virtual decimal MinPrice { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual string ChargeUnitType { get; set; }

        public virtual decimal DepositAmount { get; set; }
        public virtual decimal CancelCompensation { get; set; }
        public virtual int OverTimeForCancel { get; set; }
        public virtual string ServiceModeType { get; set; }

        public virtual string ServiceTypeName { get; set; }

        public virtual string ServiceBusinessId { get; set; }

        public virtual string ServiceBusinessOwnerId { get; set;}

        public virtual string ServiceBusinessName { get; set; }
        public virtual string ServiceBusinessPhone { get; set; }
        public virtual bool IsForBusiness { get; set; }

    }
}
