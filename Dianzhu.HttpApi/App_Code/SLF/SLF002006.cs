using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
/// <summary>
/// 服务 时间段内 订单详情.
/// </summary>
public class ResponseSLF002006:BaseResponse
{
    
    public ResponseSLF002006(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        try
        {
            ReqDataSLF002006 requestData = this.request.ReqData.ToObject<ReqDataSLF002006>();

            bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();

            string service_Id = requestData.serviceId;

            Guid serviceId;

            bool isServiceId = Guid.TryParse(service_Id, out serviceId);
            if (!isServiceId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "serviceId格式有误";
                return;
            }

            //todo: 使用 ninject,注入依赖.
            BLLDZService bllService = new BLLDZService();
            DZService service = bllService.GetOne(serviceId);

            if (service == null)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "服务不存在！";
                return;
            }

            DateTime paramDate = DateTime.Parse(requestData.date);
            //获取当天的时间段
            ServiceOpenTime openDay = service.OpenTimes.Single(x => x.DayOfWeek == paramDate.DayOfWeek);
            IList<ServiceOpenTimeForDay> openTimes = openDay.OpenTimeForDay;
            //获取当前时间段的订单数量
            IList<ServiceOrder> orderList = bllServiceOrder.GetOrderListByDate(service, paramDate);
            IList<RespDataSLF00206_arrayData> result = new List<RespDataSLF00206_arrayData>();
            foreach (ServiceOpenTimeForDay openTime in openTimes)
            {
                IList<ServiceOrder> orderListForOpenTime = orderList.Where(x => x.OrderCreated >= paramDate.AddMinutes(openTime.PeriodStart)
                  && x.OrderCreated <= paramDate.AddMinutes(openTime.PeriodEnd)).ToList();
                RespDataSLF00206_arrayData arrData = new RespDataSLF00206_arrayData();
                arrData = arrData.Adap(paramDate, openTime, orderListForOpenTime);
                result.Add(arrData);
            }

            RespDataSLF002006 respData = new RespDataSLF002006 { arrayData = result };
            this.RespData = respData;
            this.state_CODE = Dicts.StateCode[0];
            return;
        }
        catch (Exception ex)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = ex.Message;
            return;
        }
    }
}

