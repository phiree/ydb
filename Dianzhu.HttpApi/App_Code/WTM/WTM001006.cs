using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Ydb.Common;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using System.Collections.Specialized;
using PHSuit;
using FluentValidation.Results;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;

/// <summary>
/// 新增店铺
/// </summary>
public class ResponseWTM001006 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseWTM001006(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataWTM001006 requestData = this.request.ReqData.ToObject<ReqDataWTM001006>();

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
                
                IList<RespDataWTM_workTimeObj> arrayList = null;
                if (string.IsNullOrEmpty(week))
                {
                    arrayList = new List<RespDataWTM_workTimeObj>();
                    RespDataWTM_workTimeObj workObj = null;
                    foreach (ServiceOpenTime sot in service.OpenTimes)
                    {
                        if (sot.OpenTimeForDay.Count > 0)
                        {
                            foreach (ServiceOpenTimeForDay sotForDay in sot.OpenTimeForDay)
                            {
                                workObj = new RespDataWTM_workTimeObj().Adapt(sotForDay);
                                arrayList.Add(workObj);
                            }
                        }
                    }
                }
                else
                {
                    arrayList = new List<RespDataWTM_workTimeObj>();
                    RespDataWTM_workTimeObj workObj = null;
                    string[] weekList = week.Split(',');
                    for(int i = 0; i < weekList.Count(); i++)
                    {
                        foreach (ServiceOpenTime sot in service.OpenTimes)
                        {
                            if (StringToWeek(weekList[i]) == sot.DayOfWeek && sot.OpenTimeForDay.Count > 0)
                            {
                                foreach (ServiceOpenTimeForDay sotForDay in sot.OpenTimeForDay)
                                {
                                    workObj = new RespDataWTM_workTimeObj().Adapt(sotForDay);
                                    arrayList.Add(workObj);
                                }
                                break;
                            }
                        }
                    }
                }

                RespDataWTM001006 respData = new RespDataWTM001006();
                respData.arrayData = arrayList;

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


