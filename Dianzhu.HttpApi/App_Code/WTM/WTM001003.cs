using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using System.Collections.Specialized;
using PHSuit;
using Dianzhu.BLL.Validator;
using Newtonsoft.Json;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

/// <summary>
/// 修改员工信息
/// </summary>
public class ResponseWTM001003 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseWTM001003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataWTM001003 requestData = this.request.ReqData.ToObject<ReqDataWTM001003>();

        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
        IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
        IDZServiceService dzServiceService = Bootstrap.Container.Resolve<IDZServiceService>();
      
        IDZTagService tagService= Bootstrap.Container.Resolve<IDZTagService>();
        IServiceTypeService typeService= Bootstrap.Container.Resolve<IServiceTypeService>();
     
       
        
        try
        {
            string raw_id = requestData.merchantID;

            string workTime_id = requestData.workTimeObj.workTimeID;
            string tag = requestData.workTimeObj.tag;
            string startTime = requestData.workTimeObj.startTime;
            string endTime = requestData.workTimeObj.endTime;
            string week = requestData.workTimeObj.week;
            string open = requestData.workTimeObj.open ;
            bool isOpen = open == "Y" ? true : false;
            int maxOrder =Convert.ToInt32( requestData.workTimeObj.maxOrder);
            string serviceId = requestData.svcID;
            dzServiceService.ModifyWorkTimeDay(serviceId, (DayOfWeek) Convert.ToInt32(week), workTime_id, startTime,
                endTime, maxOrder, isOpen, tag);
            
                RespDataWTM_workTimeObj workTimeObj = new RespDataWTM_workTimeObj();

                //if (tag != null) { sotForDay.tag = tag; workTimeObj.name = "Y"; }
                if (startTime != null) {  workTimeObj.startTime = "Y"; }
                if (endTime != null) {  workTimeObj.endTime = "Y"; }
                if (week != null) {   workTimeObj.week = "Y"; }
                if (open != null) {  workTimeObj.open = "Y "; }
                if (maxOrder != null) {   workTimeObj.maxOrder = "Y"; }

                
                RespDataWTM001003 respData = new RespDataWTM001003(workTime_id );
                this.state_CODE = Dicts.StateCode[0];
                workTimeObj.workTimeID = workTime_id;
                respData.workTimeObj = workTimeObj;
                this.RespData = respData.workTimeObj;
          
            
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }
    }

    private DayOfWeek StrToWeek(string week)
    {
        DayOfWeek day = new DayOfWeek();
        switch (week)
        {
            case "1": day = DayOfWeek.Monday; break;
            case "2": day = DayOfWeek.Tuesday; break;
            case "3": day = DayOfWeek.Wednesday; break;
            case "4": day = DayOfWeek.Thursday; break;
            case "5": day = DayOfWeek.Friday; break;
            case "6": day = DayOfWeek.Saturday; break;
            case "7": day = DayOfWeek.Sunday; break;
            default: break;
        }
        return day;
    }

    public override string BuildJsonResponse()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}


