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
using Dianzhu.BLL.Validator;
using Newtonsoft.Json;

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
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
        BLLDZService bllDZService = new BLLDZService();
        BLLServiceType bllServiceType = Bootstrap.Container.Resolve < BLLServiceType>();
        BLLDZTag bllDZTag = new BLLDZTag();
        BLLServiceOpenTime bllServiceOpenTime = Bootstrap.Container.Resolve < BLLServiceOpenTime>();
        BLLServiceOpenTimeForDay bllServiceOpenTimeForDay = new BLLServiceOpenTimeForDay();

        try
        {
            string raw_id = requestData.merchantID;

            string workTime_id = requestData.workTimeObj.workTimeID;
            string tag = requestData.workTimeObj.tag;
            string startTime = requestData.workTimeObj.startTime;
            string endTime = requestData.workTimeObj.endTime;
            string week = requestData.workTimeObj.week;
            string open = requestData.workTimeObj.open;
            string maxOrder = requestData.workTimeObj.maxOrder;

            Guid merchantID, workTimeID;
            bool isStoreId = Guid.TryParse(raw_id, out merchantID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "merchantID格式有误";
                return;
            }

            bool isUserId = Guid.TryParse(workTime_id, out workTimeID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "workTimeID格式有误";
                return;
            }

            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(merchantID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            try
            {
                ServiceOpenTimeForDay sotForDay = bllServiceOpenTimeForDay.GetOne(workTimeID);
                if (sotForDay == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "服务时段不存在！";
                    return;
                }

                //if (sotForDay.Enabled)
                //{
                //    this.state_CODE = Dicts.StateCode[1];
                //    this.err_Msg = "服务已删除！";
                //    return;
                //}

                ServiceOpenTimeForDay sotForDayOriginal = new ServiceOpenTimeForDay();
                sotForDay.CopyTo(sotForDayOriginal);

                RespDataWTM_workTimeObj workTimeObj = new RespDataWTM_workTimeObj();

                //if (tag != null) { sotForDay.tag = tag; workTimeObj.name = "Y"; }
                if (startTime != null) { sotForDay.TimeStart = startTime; workTimeObj.startTime = "Y"; }
                if (endTime != null) { sotForDay.TimeEnd = endTime; workTimeObj.endTime = "Y"; }
                if (week != null) { sotForDay.ServiceOpenTime.DayOfWeek = StrToWeek(week); workTimeObj.week = "Y"; }
                if (open != null) { sotForDay.Enabled = open == "Y" ? true : false; workTimeObj.open = "Y "; }
                if (maxOrder != null) { sotForDay.MaxOrderForOpenTime = Int32.Parse(maxOrder); workTimeObj.maxOrder = "Y"; }

                ValidatorServiceOpenTimeForDay vs_service = new ValidatorServiceOpenTimeForDay();
                FluentValidation.Results.ValidationResult result = vs_service.Validate(sotForDay);
                foreach (FluentValidation.Results.ValidationFailure f in result.Errors)
                {
                    switch (f.PropertyName.ToLower())
                    {
                        //只有不为null的才需要
                        //case "tag":
                        //    if (workTimeObj.tag != null) { workTimeObj.tag = "N"; sotForDay.Tag = sotForDayOriginal.Tag; }
                        //    break;
                        case "starttime":
                            if (workTimeObj.startTime != null) { workTimeObj.startTime = "N"; sotForDay.TimeStart = sotForDayOriginal.TimeStart; }
                            break;
                        case "endtime":
                            if (workTimeObj.endTime != null) { workTimeObj.endTime = "N"; sotForDay.TimeEnd = sotForDayOriginal.TimeEnd; }
                            break;
                        case "week":
                            if (workTimeObj.week != null) { workTimeObj.week = "N"; sotForDay.ServiceOpenTime.DayOfWeek = sotForDayOriginal.ServiceOpenTime.DayOfWeek; }
                            break;
                        case "open":
                            if (workTimeObj.open != null) { workTimeObj.open = "N"; sotForDay.Enabled = sotForDayOriginal.Enabled; }
                            break;
                        case "maxorder":
                            if (workTimeObj.maxOrder != null) { workTimeObj.maxOrder = "N"; sotForDay.MaxOrderForOpenTime = sotForDayOriginal.MaxOrderForOpenTime; }
                            break;
                        default: break;
                    }
                }

                bllServiceOpenTimeForDay.Update(sotForDay);

                RespDataWTM001003 respData = new RespDataWTM001003(workTimeID.ToString());
                this.state_CODE = Dicts.StateCode[0];
                workTimeObj.workTimeID = workTime_id;
                respData.workTimeObj = workTimeObj;
                this.RespData = respData.workTimeObj;
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


