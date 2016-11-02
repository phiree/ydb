using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using System.Collections.Specialized;
using PHSuit;
using FluentValidation.Results;

/// <summary>
/// 新增店铺
/// </summary>
public class ResponseWTM001004 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseWTM001004(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataWTM001004 requestData = this.request.ReqData.ToObject<ReqDataWTM001004>();

        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
        BLLDZService bllDZService = Bootstrap.Container.Resolve<BLLDZService>();

        try
        {
            string raw_id = requestData.merchantID;
            string svc_id = requestData.svcID;
            string week = requestData.week;

            Guid userID,svcID;
            bool isUserId = Guid.TryParse(raw_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isSvcId = Guid.TryParse(svc_id, out svcID);
            if (!isSvcId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "svcId格式有误";
                return;
            }

 MemberDto member = null;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(memberService).ValidateUser(userID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = memberService.GetUserById(userID.ToString());
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该商户！";
                    return;
                }
            }
            try
            {
                DZService service = bllDZService.GetOne(svcID);
                if (service == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该服务！";
                    return;
                }

                int sum = 0;
                if (string.IsNullOrEmpty(week))
                {
                    foreach(ServiceOpenTime sot in service.OpenTimes)
                    {
                        sum += sot.OpenTimeForDay.Count;
                    }
                }
                else
                {
                    string[] weekList = week.Split(',');
                    for(int i = 0; i < weekList.Count(); i++)
                    {
                        foreach (ServiceOpenTime sot in service.OpenTimes)
                        {
                            if (StringToWeek(weekList[i]) == sot.DayOfWeek)
                            {
                                sum += sot.OpenTimeForDay.Count;
                            }
                        }
                    }
                }

                RespDataWTM001004 respData = new RespDataWTM001004();
                respData.sum = sum.ToString();
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


