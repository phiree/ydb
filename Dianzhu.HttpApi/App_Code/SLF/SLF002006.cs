﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
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
    protected override void BuildRespData()
    {
        try
        {
             ReqDataSLF002006 requestData = this.request.ReqData.ToObject<ReqDataSLF002006>();
         
        //todo: 使用 ninject,注入依赖.
        BLLDZService bllService = new BLLDZService();
        DZService service = bllService.GetOne(new Guid(requestData.serviceId));
        DateTime paramDate =DateTime.Parse( requestData.date);
        //获取当天的时间段
        ServiceOpenTime openDay = service.OpenTimes.Single(x => x.DayOfWeek == paramDate.DayOfWeek);
        IList<ServiceOpenTimeForDay> openTimes = openDay.OpenTimeForDay;
        //获取当前时间段的订单数量
        BLLServiceOrder bllOrder = new BLLServiceOrder();
        IList<ServiceOrder> orderList= bllOrder.GetOrderListByDate(service, paramDate);
            IList<RespDataSLF00206_arrayData> result = new List<RespDataSLF00206_arrayData>();
        foreach (ServiceOpenTimeForDay openTime in openTimes)
        {
            IList<ServiceOrder> orderListForOpenTime = orderList.Where(x => x.OrderCreated >= paramDate.AddMinutes(openTime.PeriodStart)
              && x.OrderCreated <= paramDate.AddMinutes(openTime.PeriodEnd)).ToList();
                RespDataSLF00206_arrayData arrData = new RespDataSLF00206_arrayData();
                arrData =arrData.Adap(paramDate, openTime,orderListForOpenTime);
                result.Add(arrData);
        }
        

        
        
      
            RespDataSLF002006 respData = new RespDataSLF002006 { arrayData= result };
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
public class ReqDataSLF002006
{
    public string serviceId { get; set; }
    public string date { get; set; }
}
public class RespDataSLF002006
{
    public IList<RespDataSLF00206_arrayData> arrayData { get; set; }


}
public class RespDataSLF00206_arrayData
{
    public RespDataSLF00206_arrayData()
    {
        arrayOrders = new List<RespDataSLF00206_arrayOrder>();
    }
    public string date { get; set; }
    public string timeStart { get; set; }
    public string timeEnd { get; set; }
    public int maxNum { get; set; }
    public int doneNum { get; set; }
    public bool enable { get; set; }
    public RespDataSLF00206_arrayData Adap(DateTime date, ServiceOpenTimeForDay openTime,IList<ServiceOrder> orders)
    {
        this.date = date.ToShortDateString();
        this.timeEnd =date.AddMinutes( openTime.PeriodEnd).ToString("yyyy-MM-dd hh:mm:ss");
        this.timeStart=date.AddMinutes(openTime.PeriodStart).ToString("yyyy-MM-dd hh:mm:ss");
        this.maxNum = openTime.MaxOrderForOpenTime;
        this.enable = openTime.Enabled;
       
        foreach (ServiceOrder order in orders)
        {
            arrayOrders.Add(new RespDataSLF00206_arrayOrder().Adap(order));
        }
        
        this.doneNum = arrayOrders.Count;
        return this;

    }
    public IList<RespDataSLF00206_arrayOrder> arrayOrders { get; set; }
    
}
public class RespDataSLF00206_arrayOrder
{
    public string orderId { get; set; }
    public string status { get; set; }
    public string createdTime { get; set; }
    public RespDataSLF00206_arrayOrder Adap(ServiceOrder order)
    {
        this.orderId = order.Id.ToString();
        this.status = order.OrderStatus.ToString();
        this.createdTime = order.OrderCreated.ToString("yyyy-MM-dd hh:mm:ss");
            return this;
    }
}
