using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
namespace Dianzhu.Model
{
    public class ServiceSnapShotForOrder
    {
        public virtual string ServiceName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsCompensationAdvance { get; set; }
        public virtual decimal MinPrice { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual enum_ChargeUnit ChargeUnit { get; set; }

        public virtual decimal DepositAmount { get; set; }
        public virtual decimal CancelCompensation { get; set; }
        public virtual int OverTimeForCancel { get; set; }
        public virtual enum_ServiceMode ServiceMode { get; set; }

        public virtual string ServiceBusinessId { get; set; }
        public virtual string ServiceBusinessOwnerId { get; set; }
        public virtual string ServiceBusinessName { get; set; }
        public virtual string ServiceBusinessPhone { get; set; }

        public virtual string ServiceTypeName { get; set; }
    }
}
