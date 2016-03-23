using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
   public class ServiceOpenTimeForDaySnapShotForOrderMap : ClassMap<ServiceOpenTimeForDaySnapShotForOrder>
    {
       public ServiceOpenTimeForDaySnapShotForOrderMap()
       {
            Map(x => x.Date);
            Map(x => x.MaxOrder);
            Map(x => x.PeriodBegin);
            Map(x => x.PeriodEnd);

        }
        /*
         public int MaxOrder { get; protected set; }
        public DateTime Date { get; protected set; }
        public int PeriodBegin { get; protected set; }
        public int PeriodEnd { get; protected set; }
        */
    }
}
