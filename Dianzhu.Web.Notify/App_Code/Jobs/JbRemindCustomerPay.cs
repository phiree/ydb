using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.Application;
using Ydb.InstantMessage.Application;
public class JbRemindCustomerPay : IJob
{
    IServiceOrderService orderService;

    IInstantMessage im;
    public JbRemindCustomerPay(IServiceOrderService orderService, IInstantMessage im )
    {

        this.orderService = orderService;

        this.im = im;
    }
    log4net.ILog log = log4net.LogManager.GetLogger("Ydb.Task.JObs.JbCancelAfter10M");

    public void Execute(IJobExecutionContext context)

    {

        log.Debug("开始执行");
        string errMsg;
        try
        {
         //   IInstantMessage im = (IInstantMessage)System.Web.HttpContext.Current.Application["IM"];
            string orderId = context.JobDetail.JobDataMap["orderId"].ToString();

            var order = orderService.GetOne(new Guid(orderId));
            im.SendMessageText(Guid.NewGuid(), "您有一份订单还未支付,请尽快处理", order.CustomerId, "Ydb_Customer", orderId);

            log.Info("执行成功");

        }
        catch (Exception ex)
        {
            errMsg = "任务执行失败." + ex.ToString();
            log.Error(errMsg);
            throw new JobExecutionException(errMsg);
        }
    }




}
