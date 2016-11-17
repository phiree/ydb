using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class ServiceOrderDetailMap : ClassMap<ServiceOrderDetail>
    {
        public ServiceOrderDetailMap()
        {
            Id(x => x.Id);
            Map(x => x.OriginalServiceId);
            //screenshot of the service
            Component<ServiceSnapShotForOrder>(x => x.ServiceSnapShot,m=> {
               m. Map(x => x.ChargeUnitType);
                m.Map(x => x.DepositAmount);
                m.Map(x => x.CancelCompensation);
                m.Map(x => x.Description);
                m.Map(x => x.IsCompensationAdvance);
                m.Map(x => x.MinPrice);
                m.Map(x => x.OverTimeForCancel);
                m.Map(x => x.ServiceModeType);
                m.Map(x => x.ServiceName);
                m.Map(x => x.UnitPrice);
            });
            Component<ServiceOpenTimeForDaySnapShotForOrder>(x => x.OpenTimeSnapShot
            ,m=>
            {
               m. Map(x => x.Date);
                m.Map(x => x.MaxOrder);
                m.Map(x => x.PeriodBegin);
                m.Map(x => x.PeriodEnd);
            }
            );
            Component<ServiceOpenTimeSnapshot>(x => x.ServiceOpentimeSnapshot, 
                m => {
                    m.Map(x => x.MaxOrderForDay);
            }
                );

            #region 服务项需求            
            Map(x => x.UnitAmount);
            Map(x => x.TargetAddress);
            Map(x => x.TargetCustomerName);
            Map(x => x.TargetCustomerPhone);
            Map(x => x.TargetTime);
            Map(x => x.Memo);
            #endregion
        }
    }
}
