using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
   public class ServiceSnapShotForOrderMap: ClassMap<ServiceSnapShotForOrder>
    {
       public ServiceSnapShotForOrderMap()
       {
  

            Map(x => x.ChargeUnit);
            Map(x => x.DepositAmount);
            Map(x => x.CancelCompensation);
            Map(x => x.Description);
            Map(x => x.IsCompensationAdvance);
            Map(x => x.MinPrice);
            Map(x => x.OverTimeForCancel);
            Map(x => x.ServiceMode);
            Map(x => x.ServiceName);
            Map(x => x.UnitPrice);



        }
        /*
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
        */
    }
}
