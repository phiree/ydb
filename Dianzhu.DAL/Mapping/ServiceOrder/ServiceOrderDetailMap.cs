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

            Component<WorkTimeSnapshot>(x => x.ServiceOpentimeSnapshot,
                m => {
                    m.Map(x => x.MaxOrderForWorkDay);
                    m.Map(x => x.MaxOrderForWorkTime);
                    m.Map(x => x.Enabled);
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
