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
            References<DZService>(x => x.OriginalService);
            //screenshot of the service
            Component<ServiceSnapShotForOrder>(x => x.ServieSnapShot,m=> {
               m. Map(x => x.ChargeUnit);
                m.Map(x => x.DepositAmount);
                m.Map(x => x.CancelCompensation);
                m.Map(x => x.Description);
                m.Map(x => x.IsCompensationAdvance);
                m.Map(x => x.MinPrice);
                m.Map(x => x.OverTimeForCancel);
                m.Map(x => x.ServiceMode);
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

            /// <summary>
            /// 购买数量
            /// </summary>
            Map(x => x.UnitAmount);
            /// <summary>
            /// 客户要求的服务地址
            /// </summary>
            Map(x => x.TargetAddress);
            Map(x => x.TargetTime);
            #endregion
            /// <summary>
            /// 该服务分配的员工.
            /// </summary>
            //HasMany(x => x.Staff);//员工不能和订单进行直接绑定
        }
    }
}
