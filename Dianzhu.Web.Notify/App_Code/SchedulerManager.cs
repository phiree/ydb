using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Ydb.Order.Application;
 
    public class YdbJobManager
    {
        static IScheduler scheduler = Bootstrap.Container.Resolve<IScheduler>();
        IServiceOrderService orderService;
         

        public YdbJobManager(IServiceOrderService orderService)
        {
            this.orderService = orderService;
            scheduler.JobFactory  = Bootstrap.Container.Resolve<IJobFactory>();
        }
        public IList<IJobExecutionContext> CreateJob(string orderid,string type)
        {
            IJobDetail job = null;
        Type jobType = Type.GetType(type);    

                    job = JobBuilder.Create(jobType)
                        .SetJobData(new JobDataMap {   { "orderId",orderid} })
                      
   
                         .Build();
        
            //TriggerFiredBundle tfb=n

            scheduler.ScheduleJob(job, CreateOneTimeTrigger(10));
            return scheduler.GetCurrentlyExecutingJobs();
        }
        private   ITrigger CreateOneTimeTrigger(int delaySeconds)
        {
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()

            .StartAt(DateTime.Now.AddSeconds(delaySeconds))// some Date 
            .Build();
            return trigger;

        }
       
       
        public IList<JobDto> GetAllJobs()
        {
            var jobGroups = scheduler.GetJobGroupNames();
            IList<JobDto> allJobs = new List<JobDto>();
            foreach (string group in jobGroups)
            {
                var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                var jobKeys = scheduler.GetJobKeys(groupMatcher);
                foreach (var jobKey in jobKeys)
                {
                   
                    var detail = scheduler.GetJobDetail(jobKey);
                    var triggers = scheduler.GetTriggersOfJob(jobKey);


                    foreach (ITrigger trigger in triggers)
                    {
                        JobDto dto = new JobDto();

                        dto.StartTime = trigger.StartTimeUtc.LocalDateTime;
                        dto.GroupName = group;
                        dto.JobName  = jobKey.Name;
                         dto.JobDescription  = detail.Description;
                         dto.TriggerName = trigger.Key.Name;
                        
                         dto.TriggerGroupName  = trigger.Key.Group;
                         dto.TriggerType  = trigger.GetType().Name;
                         dto.TriggerState  = scheduler.GetTriggerState(trigger.Key).ToString();
                        DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
                        if (nextFireTime.HasValue)
                        {
                            dto.NextFireTime  = TimeZone.CurrentTimeZone.ToLocalTime(nextFireTime.Value.DateTime);
                        }

                        DateTimeOffset? previousFireTime = trigger.GetPreviousFireTimeUtc();
                        if (previousFireTime.HasValue)
                        {
                            dto.PreviousFireTime  = TimeZone.CurrentTimeZone.ToLocalTime(previousFireTime.Value.DateTime);
                        }

                        allJobs.Add(dto);
                    }
                }
            }
            return allJobs;
        }
    }
    public enum JobType
    {
        Cancel_10M
    }
 