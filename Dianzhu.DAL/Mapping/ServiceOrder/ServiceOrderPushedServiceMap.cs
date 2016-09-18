using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class ServiceOrderPushedServiceMap : ClassMap<ServiceOrderPushedService>
    {
        public ServiceOrderPushedServiceMap()
        {
            Id(x => x.Id);
            References<DZService>(x => x.OriginalService);
            //screenshot of the service
            Map(x => x.ServiceName);
            Map(x => x.Description);
            Map(x => x.IsCompensationAdvance);
            Map(x => x.MinPrice);
            Map(x => x.UnitPrice);
            Map(x => x.ChargeUnit);

            Map(x => x.DepositAmount);
            Map(x => x.CancelCompensation);
            Map(x => x.OverTimeForCancel);
            Map(x => x.ServiceMode);
            References<ServiceOrder>(x => x.ServiceOrder);

            #region 服务项需求
            Map(x => x.UnitAmount);
            Map(x => x.TargetCustomerName);
            Map(x => x.TargetCustomerPhone);
            Map(x => x.TargetAddress);
            Map(x => x.TargetTime);
            Map(x => x.Memo);
            #endregion
           
        }
    }
}
