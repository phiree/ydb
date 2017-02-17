using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Ydb.Order.DomainModel;
namespace Ydb.Order.Infrastructure.Repository.NHibernate.Mapping
{
    public class ServiceOrderPushedServiceMap : ClassMap<ServiceOrderPushedService>
    {
        public ServiceOrderPushedServiceMap()
        {
            Id(x => x.Id);
            Map(x => x.OriginalServiceId);
            Component<ServiceSnapShot>(x => x.ServiceSnapShot, m => {
                m.Map(x => x.ChargeUnit);
                m.Map(x => x.DepositAmount);
                m.Map(x => x.CancelCompensation);
                m.Map(x => x.Description);
                m.Map(x => x.IsCompensationAdvance);
                m.Map(x => x.MinPrice);
                m.Map(x => x.OverTimeForCancel);
                m.Map(x => x.ServiceMode);
                m.Map(x => x.Name);
                m.Map(x => x.UnitPrice);

                m.Map(x => x.ServiceTypeName);
                m.Map(x => x.ServiceTypeId);
                m.Map(x => x.BusinessId);
                m.Map(x => x. BusinessOwnerId);
                m.Map(x => x. BusinessName);
                m.Map(x => x. BusinessPhone);
                m.Map(x => x.IsForBusiness);
                m.Map(x => x.Enabled);
                m.Map(x => x.AllowedPayType);
                m.Map(x => x.OrderDelay);
                m.Map(x => x.Scope);
            });
            //screenshot of the service
           
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
