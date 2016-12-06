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
            Component<ServiceSnapShot>(x => x.ServiceSnapShot,m=> {
               m. Map(x => x.ChargeUnit);
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
                m.Map(x => x.  BusinessName);
                m.Map(x => x. BusinessPhone);
                m.Map(x => x.IsForBusiness);
                m.Map(x => x.Enabled);
                m.Map(x => x.AllowedPayType);
                m.Map(x => x.OrderDelay);
                m.Map(x => x.Scope);
            });

            Component<WorkTimeSnapshot>(x => x.ServiceOpentimeSnapshot,
                m => {
                    m.Map(x => x.MaxOrderForWorkDay);
                    m.Map(x => x.MaxOrderForWorkTime);
                    m.Map(x => x.Enabled).Column("WorkTimeEnabled");
                    m.Component(x => x.TimePeriod, ms => {
                        ms.Component(x => x.StartTime, mss => {
                            mss.Map(x => x.Hour).Column("StartHour");
                            mss.Map(x => x.Minute).Column("StartMinute");
                        });
                        ms.Component(x => x.EndTime, mss => {
                            mss.Map(x => x.Hour).Column("EndHour");
                            mss.Map(x => x.Minute).Column("EndMinute");
                        });
                    });
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
