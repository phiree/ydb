using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using System.Collections.Specialized;
using PHSuit;
using FluentValidation.Results;

/// <summary>
/// 新增店铺
/// </summary>
public class ResponseWTM001001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseWTM001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataWTM001001 requestData = this.request.ReqData.ToObject<ReqDataWTM001001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();

        BLLDZService bllDZService = Bootstrap.Container.Resolve<BLLDZService>();

        try
        {
            string raw_id = requestData.merchantID;
            string svc_id = requestData.svcID;
            string repeat = requestData.repeat;
            RespDataWTM_workTimeObj workTimeObj = requestData.workTimeObj;

            if (workTimeObj.startTime == workTimeObj.endTime)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "服务开始时间不能等于结束时间！";
                return;
            }

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

            DZMembership member = null;
            if (request.NeedAuthenticate)
            {                
                bool validated = new Account(p).ValidateUser(userID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = p.GetUserById(userID);
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

                if (service.Business.Owner.Id != userID)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该商户没有该服务！";
                    return;
                }
                
                if (repeat != null)
                {
                    string[] repeatList = repeat.Split(',');
                    for(int i = 0; i < repeatList.Count(); i++)
                    {
                        foreach(ServiceOpenTime sotObj in service.OpenTimes)
                        {
                            if (sotObj.DayOfWeek == StringToWeek(repeatList[i]))
                            {
                                ServiceOpenTimeForDay sotDay = new ServiceOpenTimeForDay();
                                sotDay.MaxOrderForOpenTime = Int32.Parse(workTimeObj.maxOrder);
                                sotDay.TimeStart = workTimeObj.startTime;
                                sotDay.TimeEnd = workTimeObj.endTime;
                                sotDay.Enabled = workTimeObj.open == "Y" ? true : false;
                                sotDay.ServiceOpenTime = sotObj;
                                sotObj.AddServicePeriod(sotDay);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    int week;
                    try
                    {
                        week = Int32.Parse(workTimeObj.week);
                    }
                    catch (Exception e)
                    {
                        ilog.Error("week中有非法字符！");
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "week中有非法字符！";
                        return;
                    }
                    if (Int32.Parse(workTimeObj.week) > 7 || Int32.Parse(workTimeObj.week) < 1)
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "week有误！";
                        return;
                    }
                    
                    foreach (ServiceOpenTime sotObj in service.OpenTimes)
                    {
                        if (sotObj.DayOfWeek == StringToWeek(week.ToString()))
                        {
                            ServiceOpenTimeForDay sotDay = new ServiceOpenTimeForDay();
                            sotDay.MaxOrderForOpenTime = Int32.Parse(workTimeObj.maxOrder);
                            sotDay.TimeStart = workTimeObj.startTime;
                            sotDay.TimeEnd = workTimeObj.endTime;
                            sotDay.Enabled = workTimeObj.open == "Y" ? true : false;
                            sotDay.ServiceOpenTime = sotObj;
                            sotObj.AddServicePeriod(sotDay);
                            break;
                        }
                    }
                }

                ValidationResult vResult = new ValidationResult();
                bllDZService.SaveOrUpdate(service, out vResult);
                NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

                if (!vResult.IsValid)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    foreach (ValidationFailure vr in vResult.Errors)
                    {
                        this.err_Msg += vr.ErrorCode + ":" + vr.ErrorMessage + "\n";
                    }
                    return;
                }

                RespDataWTM001001 respData = new RespDataWTM001001();
                respData.arrayData = new List<string>();
                foreach (ServiceOpenTime sotObj in service.OpenTimes)
                {
                    foreach(ServiceOpenTimeForDay sotDayObj in sotObj.OpenTimeForDay)
                    {
                        if(sotDayObj.TimeStart== workTimeObj.startTime&&sotDayObj.TimeEnd== workTimeObj.endTime)
                        {
                            respData.arrayData.Add(sotDayObj.Id.ToString());
                            break;
                        }                        
                    }
                }
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


