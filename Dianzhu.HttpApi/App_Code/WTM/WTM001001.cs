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
using FluentValidation.Results;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Application;
/// <summary>
/// 增加一個工作時間, 如果已經存在則返回該工作時間的Id
/// todo:refactor: 功能可能缺失.
/// </summary>
public class ResponseWTM001001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Ydb.HttpApi");

    public ResponseWTM001001(BaseRequest request) : base(request) {
        
    }
    protected override void BuildRespData()
    {
        ReqDataWTM001001 requestData = this.request.ReqData.ToObject<ReqDataWTM001001>();

        //todo:用户验证的复用.
        IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

        IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
        IDZServiceService dzServiceService = Bootstrap.Container.Resolve<IDZServiceService>();
        try
        {
            string raw_id = requestData.merchantID;
            string svc_id = requestData.svcID;
            string repeat = requestData.repeat;
            RespDataWTM_workTimeObj workTimeObj = requestData.workTimeObj;
            

         ActionResult<ServiceOpenTimeForDay> workTime=  dzServiceService.AddWorkTime(raw_id, svc_id, (DayOfWeek)Enum.Parse(typeof(DayOfWeek), workTimeObj.week), workTimeObj.startTime
                , workTimeObj.endTime,
               Convert.ToInt32(workTimeObj.maxOrder), workTimeObj.tag);
            RespDataWTM001001 respData = new RespDataWTM001001();
            respData.arrayData = new List<string>();
            respData.arrayData.Add(workTime.ResultObject.Id.ToString());
            this.state_CODE = Dicts.StateCode[0];
            this.RespData = respData;
          ;
           
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }
    }

    private DayOfWeek StringToWeek(string str)
    {
        DayOfWeek week = new DayOfWeek();
        switch (str)
        {
            case "1":
                week = DayOfWeek.Monday;
                break;
            case "2":
                week = DayOfWeek.Tuesday;
                break;
            case "3":
                week = DayOfWeek.Wednesday;
                break;
            case "4":
                week = DayOfWeek.Thursday;
                break;
            case "5":
                week = DayOfWeek.Friday;
                break;
            case "6":
                week = DayOfWeek.Saturday;
                break;
            case "7":
                week = DayOfWeek.Sunday;
                break;
            default:
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "repeat中有非法字符！";
                break; ;
        }
        return week;
    }
}


