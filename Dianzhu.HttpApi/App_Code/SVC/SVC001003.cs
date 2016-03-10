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
public class ResponseSVC001003 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseSVC001003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVC001003 requestData = this.request.ReqData.ToObject<ReqDataSVC001003>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLBusiness bllBusiness = new BLLBusiness();
        BLLDZService bllDZService = new BLLDZService();
        BLLServiceType bllServiceType = new BLLServiceType();
        BLLDZTag bllDZTag = new BLLDZTag();
        BLLServiceOpenTime bllServiceOpenTime = new BLLServiceOpenTime();

        try
        {
            string raw_id = requestData.merchantID;

            string svc_id = requestData.svcObj.svcID;
            string name = requestData.svcObj.name;
            string type = requestData.svcObj.type;
            string introduce = requestData.svcObj.introduce;
            string area = requestData.svcObj.area;
            string startAt = requestData.svcObj.startAt;
            string unitPrice = requestData.svcObj.unitPrice;
            string deposit = requestData.svcObj.deposit;
            string appointmentTime = requestData.svcObj.appointmentTime;
            string doorService = requestData.svcObj.doorService;
            string serviceObject = requestData.svcObj.serviceObject;
            string payWay = requestData.svcObj.payWay;
            string tag = requestData.svcObj.tag;
            string open = requestData.svcObj.open;
            string maxOrderString = requestData.svcObj.maxOrderString;

            Guid merchantID,svcID;
            bool isStoreId = Guid.TryParse(raw_id, out merchantID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "merchantID格式有误";
                return;
            }

            bool isUserId = Guid.TryParse(svc_id, out svcID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "svcID格式有误";
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
                DZService service = bllDZService.GetOne(svcID);
                if (service == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "服务不存在！";
                    return;
                }

                if (service.IsDeleted)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "服务已删除！";
                    return;
                }

                DZService serviceoriginal = new DZService();
                service.CopyTo(serviceoriginal);
                
                RespDataSVC_svcObj svcObj = new RespDataSVC_svcObj();

                if (name != null) { service.Name = name; svcObj.name = "Y"; }
                if (type != null)
                {
                    string[] typeList = type.Split('>');
                    int typeLevel = typeList.Count() > 0 ? typeList.Count() - 1 : 0;
                    ServiceType sType = bllServiceType.GetOneByName(typeList[typeLevel], typeLevel);
                    if (sType == null)
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "服务类型有误";
                        return;
                    }

                    service.ServiceType = sType;
                    svcObj.type = "Y";
                }
                if (introduce != null) { service.Description = introduce; svcObj.introduce = "Y"; }
                if (area != null) { service.BusinessAreaCode = area; svcObj.area = "Y"; }
                if (startAt != null) { service.MinPrice = decimal.Parse(startAt); svcObj.startAt = "Y "; }
                if (unitPrice != null) { service.UnitPrice = decimal.Parse(unitPrice); svcObj.unitPrice = "Y"; }
                if (deposit != null) { service.DepositAmount = decimal.Parse(deposit); svcObj.deposit = "Y"; }
                if (appointmentTime != null) { service.OrderDelay = Int32.Parse(appointmentTime); svcObj.appointmentTime = "Y"; }
                if (doorService != null) { service.ServiceMode = doorService == "Y" ? enum_ServiceMode.ToHouse : enum_ServiceMode.NotToHouse; svcObj.doorService = "Y"; }
                if (serviceObject != null) { service.IsForBusiness = serviceObject == "company" ? true : false; svcObj.serviceObject = "Y"; }
                if (payWay != null) { service.AllowedPayType = (enum_PayType)Enum.Parse(typeof(enum_PayType), payWay); svcObj.payWay = "Y"; }
                if (open != null) { service.Enabled = open == "Y" ? true : false; svcObj.open = "Y"; }
                if (tag != null)
                {
                    bllDZTag.DeleteByServiceId(service.Id);

                    string[] tagList = tag.Split(',');
                    for (int i = 0; i < tagList.Count(); i++)
                    {
                        bllDZTag.AddTag(tagList[i], service.Id.ToString(), service.Business.Id.ToString(), service.ServiceType.Id.ToString());
                    }
                    svcObj.tag = "Y";
                }
                if (maxOrderString != null)
                {
                    string[] maxList = maxOrderString.Split(',');
                    int count = maxList.Count();
                    if (count != 7)
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "最大接单量天数必须为7";
                        return;
                    }

                    foreach (ServiceOpenTime sot in service.OpenTimes)
                    {
                        switch (sot.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                sot.MaxOrderForDay = Int32.Parse(maxList[0]);
                                break;
                            case DayOfWeek.Tuesday:
                                sot.MaxOrderForDay = Int32.Parse(maxList[1]);
                                break;
                            case DayOfWeek.Wednesday:
                                sot.MaxOrderForDay = Int32.Parse(maxList[2]);
                                break;
                            case DayOfWeek.Thursday:
                                sot.MaxOrderForDay = Int32.Parse(maxList[3]);
                                break;
                            case DayOfWeek.Friday:
                                sot.MaxOrderForDay = Int32.Parse(maxList[4]);
                                break;
                            case DayOfWeek.Saturday:
                                sot.MaxOrderForDay = Int32.Parse(maxList[5]);
                                break;
                            case DayOfWeek.Sunday:
                                sot.MaxOrderForDay = Int32.Parse(maxList[6]);
                                break;
                            default:
                                break;
                        }
                        bllServiceOpenTime.SaveOrUpdate(sot);
                    }
                    svcObj.maxOrderString = "Y";

                }
                
                ValidatorDZService vs_service = new ValidatorDZService();
                FluentValidation.Results.ValidationResult result = vs_service.Validate(service);
                foreach (FluentValidation.Results.ValidationFailure f in result.Errors)
                {
                    switch (f.PropertyName.ToLower())
                    {
                        //只有不为null的菜需要
                        case "name":
                            if (svcObj.name != null) { svcObj.name = "N"; service.Name = serviceoriginal.Name; }
                            break;
                        case "type":
                            if (svcObj.type != null) { svcObj.type = "N"; service.ServiceType = serviceoriginal.ServiceType; }
                            break;
                        case "introduce":
                            if (svcObj.introduce != null) { svcObj.introduce = "N"; service.Description = serviceoriginal.Description; }
                            break;
                        case "area":
                            if (svcObj.area != null) { svcObj.area = "N"; service.BusinessAreaCode = serviceoriginal.BusinessAreaCode; }
                            break;
                        case "startAt":
                            if (svcObj.startAt != null) { svcObj.startAt = "N"; service.MinPrice = serviceoriginal.MinPrice; }
                            break;
                        case "unitPrice":
                            if (svcObj.unitPrice != null) { svcObj.unitPrice = "N";service.UnitPrice = serviceoriginal.UnitPrice; }
                            break;
                        case "deposit":
                            if (svcObj.deposit != null) { svcObj.deposit = "N";service.DepositAmount = serviceoriginal.DepositAmount; }
                            break;
                        case "appointmentTime":
                            if (svcObj.appointmentTime != null) { svcObj.appointmentTime = "N";service.OrderDelay = serviceoriginal.OrderDelay; }
                            break;
                        case "doorService":
                            if (svcObj.doorService != null) { svcObj.doorService = "N";service.ServiceMode = serviceoriginal.ServiceMode; }
                            break;
                        case "serviceObject":
                            if (svcObj.serviceObject != null) { svcObj.serviceObject = "N";service.IsForBusiness = serviceoriginal.IsForBusiness; }
                            break;
                        case "payWay":
                            if (svcObj.payWay != null) { svcObj.payWay = "N";service.AllowedPayType = serviceoriginal.AllowedPayType; }
                            break;
                        case "open":
                            if (svcObj.open != null) { svcObj.open = "N";service.Enabled = serviceoriginal.Enabled; }
                            break;
                        default: break;
                    }
                }
                

                bllDZService.Update(service);

                RespDataSVC001003 respData = new RespDataSVC001003(svcID.ToString());
                this.state_CODE = Dicts.StateCode[0];
                svcObj.svcID = respData.svcObj.svcID;
                respData.svcObj = svcObj;
                this.RespData = respData.svcObj;
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
    public override string BuildJsonResponse()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}


