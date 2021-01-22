using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Castle.Windsor;

/// <summary>
/// IocJobFactory 的摘要说明
/// </summary>
public class IocJobFactory:IJobFactory
{
    IWindsorContainer container;
    public IocJobFactory(IWindsorContainer container)
    {
        this.container = container;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        IJobDetail jobDetail = bundle.JobDetail;
        Type jobType = jobDetail.JobType;

        // Return job that is registrated in container
        return (IJob)container.Resolve(jobType);
    }

    public void ReturnJob(IJob job)
    {
        //var disposable = job as IDisposable;
        //if (disposable != null)
        //{
        //    disposable.Dispose();
        //}
    }
}