using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.YTask.Triggers
{
   public class TgrFactory
    {

        public ITrigger Create(int intervalSeconds,int repeattime)
        {
            ITrigger tgr = null;
            switch (tgrType)
            {
                case TriggerType.Delay10M:
                    tgr = (ISimpleTrigger)TriggerBuilder.Create()
                       .WithIdentity(tgr.ToString())
                       .StartAt(DateTime.Now.AddMinutes(10)) // some Date 
                       .Build();
                    break;
                case TriggerType.Delay15M_Delay30M:
                    tgr = (ISimpleTrigger)TriggerBuilder.Create()
                       .WithIdentity(tgr.ToString())
                       .StartAt(DateTime.Now.AddMinutes(10)) // some Date 
                       
                       .Build();
                    break;
                case TriggerType.Delay10M:
                    tgr = (ISimpleTrigger)TriggerBuilder.Create()
                       .WithIdentity(tgr.ToString())
                       .StartAt(DateTime.Now.AddMinutes(10)) // some Date 
                       .Build();
                    break;
                case TriggerType.Delay10M:
                    tgr = (ISimpleTrigger)TriggerBuilder.Create()
                       .WithIdentity(tgr.ToString())
                       .StartAt(DateTime.Now.AddMinutes(10)) // some Date 
                       .Build();
                    break;
                case TriggerType.Delay10M:
                    tgr = (ISimpleTrigger)TriggerBuilder.Create()
                       .WithIdentity(tgr.ToString())
                       .StartAt(DateTime.Now.AddMinutes(10)) // some Date 
                       .Build();
                    break;
            }
            return tgr;
        }

    }
    public enum TriggerType
    {
        



    }
}
