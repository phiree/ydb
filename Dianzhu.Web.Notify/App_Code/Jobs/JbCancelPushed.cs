using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.Application;
 
    public class JbCancelPushed : IJob
    {
    IServiceOrderService orderService;
    public JbCancelPushed(IServiceOrderService orderService)
    {
        this.orderService = orderService;
    }
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.Task.JObs.JbCancelAfter10M");
       
        public void Execute(IJobExecutionContext context)

        {
        
            log.Debug("开始执行");
            string errMsg;
            try
            {
                string orderId = context.JobDetail.JobDataMap["orderId"].ToString();
                  var result= orderService.OrderFlow_Canceled(new Guid(orderId));
            if (!result)
            {
                errMsg = "任务执行失败.";// + "order:" + orderId;
                log.Error(errMsg);
                throw new JobExecutionException(errMsg);

            }
            else
            {
                log.Info("执行成功");
            }
            }
            catch (Exception ex)
            {
                errMsg = "任务执行失败." + ex.ToString();
                log.Error(errMsg);
                throw new JobExecutionException(errMsg);
            }
        }
 
    

   
}
