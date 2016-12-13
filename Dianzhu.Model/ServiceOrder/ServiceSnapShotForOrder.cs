using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace Dianzhu.Model
{
    public class ServiceSnapShot
    {

        public virtual string  Name { get; set; }
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
        public virtual string ServiceTypeId { get; set; }

        public virtual string BusinessId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string  BusinessOwnerId { get; set; }

        public virtual string   BusinessName { get; set; }
        public virtual string  BusinessPhone { get; set; }
        public virtual bool IsForBusiness { get; set; }

        public virtual bool Enabled { get; set; }
        public virtual string AllowedPayType { get; set; }
        public virtual int OrderDelay { get; set; }
        public virtual string Scope { get; set; }
        public virtual IList<string> MaxOrdersPerDay { get; set; }
    }
}
