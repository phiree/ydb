using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using Quartz.Logging;
namespace Ydb.YTask
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgram().GetAwaiter().GetResult();
        }
        private static async Task RunProgram()
        {
            try
            {
                LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
                NameValueCollection props = new NameValueCollection {
                    { "quartz.serializer.type","binary"}
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                IJobDetail job2 = JobBuilder.Create<ClockJob>()
                   .WithIdentity("job1", "group2")
                   .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(3)
                    .RepeatForever()
                    )
                    .Build();
                ITrigger trigger2 = TriggerBuilder.Create()
                  .WithIdentity("trigger", "group2")
                  .StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(5)
                  .RepeatForever()
                  )
                  .Build();
                await scheduler.ScheduleJob(job, trigger);
                await scheduler.ScheduleJob(job2, trigger2);

                await Task.Delay(TimeSpan.FromSeconds(60));
                await scheduler.Shutdown();

            }
            catch (SchedulerException e)
            {
                await Console.Error.WriteLineAsync(e.ToString());
            }
        }
    }
    public class Job : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greeting from hello job");
        }
    }
    public class ClockJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("time is :" + "second:" + DateTime.Now.Second + "__" + DateTime.Now);

        }
    }

    public class ConsoleLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("log");

            return (level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info && func != null)
                {
                    Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                }
                return true;
            };
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }
    }
}
