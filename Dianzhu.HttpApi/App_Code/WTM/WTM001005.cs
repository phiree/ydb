using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Ydb.Common;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using System.Collections.Specialized;
using PHSuit;
using FluentValidation.Results;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

/// <summary>
/// 新增店铺
/// </summary>
public class ResponseWTM001005 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Ydb.HttpApi");

    public ResponseWTM001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataWTM001005 requestData = this.request.ReqData.ToObject<ReqDataWTM001005>();

        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
        IBusinessService bllBusiness = Bootstrap.Container.Resolve<IBusinessService>();

        IServiceOpenTimeForDayService bllServiceOpenTimeForDay = Bootstrap.Container.Resolve<IServiceOpenTimeForDayService>();

        try
        {
            string workTime_id = requestData.workTimeID;

            Guid workTimeID;
            bool isWTId = Guid.TryParse(workTime_id, out workTimeID);
            if (!isWTId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "workTimeID格式有误";
                return;
            }

            //MemberDto member = null;
            //if (request.NeedAuthenticate)
            //{                
            //    bool validated = new Account(memberService).ValidateUser(userID, requestData.pWord, this, out member);
            //    if (!validated)
            //    {
            //        return;
            //    } 
            //}
            //else
            //{
            //    member = memberService.GetUserById(userID.ToString());
            //    if (member == null)
            //    {
            //        this.state_CODE = Dicts.StateCode[1];
            //        this.err_Msg = "不存在该商户！";
            //        return;
            //    }
            //}
            try
            {
                ServiceOpenTimeForDay sotForDay = bllServiceOpenTimeForDay.GetOne(workTimeID);
                if (sotForDay == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该服务时段！";
                    return;
                }

                RespDataWTM_workTimeObj workTimeObj = new RespDataWTM_workTimeObj().Adapt(sotForDay);

                RespDataWTM001005 respData = new RespDataWTM001005();
                respData.workTimeObj = workTimeObj;
                this.state_CODE = Dicts.StateCode[0];
                this.RespData = respData;
            }
            catch (Exception ex)
            {
                ilog.Error(ex.Message);
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }

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
            case "1": week = DayOfWeek.Monday; break;
            case "2": week = DayOfWeek.Tuesday; break;
            case "3": week = DayOfWeek.Wednesday; break;
            case "4": week = DayOfWeek.Thursday; break;
            case "5": week = DayOfWeek.Friday; break;
            case "6": week = DayOfWeek.Saturday; break;
            case "7": week = DayOfWeek.Sunday; break;
            default: break; ;
        }
        return week;
    }
}


