using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
/// <summary>
/// 获取某一个服务  一周7天的 简要信息
/// </summary>
public class ResponseSLF001007:BaseResponse
{
    
    public ResponseSLF001007(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public IBLLServiceOrder bllOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataSLF001007 requestData = this.request.ReqData.ToObject<ReqDataSLF001007>();

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

        //计算传入Date所在的周,以及该周内其余日期.
        List<DateTime> datesOfdate = new List<DateTime>();
        DateTime currentDate =DateTime.Parse(requestData.date);
        DayOfWeek dw = currentDate.DayOfWeek;
        //周一的日期
        for (int i = 1-(int)dw; i <= 7-(int)dw; i++)
        { 
            DateTime dateOfWeek = currentDate.AddDays(i);
            datesOfdate.Add(dateOfWeek);
        }
        //根据传入的 开始时间,结束时间, 计算返回的 opentimes
        double daySpan =( datesOfdate.First() - datesOfdate.Last()).TotalDays;
        IList<RespDataSLF00107_Obj> openTimes = new List<RespDataSLF00107_Obj>();
        foreach (DateTime dt in datesOfdate)
        {
            ServiceOpenTime op = service.OpenTimes.Single(x => x.DayOfWeek == dt.DayOfWeek);

            RespDataSLF00107_Obj obj = new RespDataSLF00107_Obj().Adapt(dt, op);
            obj.dayDoneOrder=   bllOrder.GetOrderListByDate(service, dt).Count;
            openTimes.Add(obj);
        }

        
        
        try
        {
            RespDataSLF001007 respData = new RespDataSLF001007 { arrayData=openTimes };
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