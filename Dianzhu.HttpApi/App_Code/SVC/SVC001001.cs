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
public class ResponseSVC001001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseSVC001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVC001001 requestData = this.request.ReqData.ToObject<ReqDataSVC001001>();

        //todo:用户验证的复用.
       IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
 
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
        BLLDZService bllDZService = Bootstrap.Container.Resolve<BLLDZService>();
      
        BLLDZTag bllDZTag = Bootstrap.Container.Resolve<BLLDZTag>();
 
        
        
        BLLServiceType bllServiceType = Bootstrap.Container.Resolve < BLLServiceType>();
 

        try
        {
            string raw_id = requestData.merchantID;
            string store_id = requestData.storeID;
            RespDataSVC_svcObj service_Req = requestData.svcObj;

            Guid userID,storeID;
            bool isUserId = Guid.TryParse(raw_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
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
                bool isStoreId = Guid.TryParse(store_id, out storeID);
                if (!isStoreId)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "storeId格式有误";
                    return;
                }

                Business business = bllBusiness.GetBusinessByIdAndOwner(storeID, userID);
                if (business == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺不存在！";
                    return;
                }

                string[] typeList = service_Req.type.Split('>');
                int typeLevel = typeList.Count() > 0 ? typeList.Count() - 1 : 0;
                ServiceType sType = bllServiceType.GetOneByName(typeList[typeLevel], typeLevel);
                if (sType == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "服务类型有误";
                    return;
                }
                
                DZService service = new DZService
                {
                    Business = bllBusiness.GetBusinessByIdAndOwner(storeID, userID),
                    Name = service_Req.name,
                    ServiceType = sType,
                    Description = service_Req.introduce,
                    BusinessAreaCode = service_Req.area,
                    MinPrice = decimal.Parse(service_Req.startAt),
                    UnitPrice = decimal.Parse(service_Req.unitPrice),
                    OrderDelay = Int32.Parse(service_Req.appointmentTime),
                    ServiceMode = service_Req.doorService == "Y" ? enum_ServiceMode.ToHouse : enum_ServiceMode.NotToHouse,
                    IsForBusiness = service_Req.serviceObject == "all" ? false : true,
                    AllowedPayType = (enum_PayType)Enum.Parse(typeof(enum_PayType), service_Req.payWay),
                    Enabled = service_Req.open == "Y" ? true : false,
                };

                ValidationResult validationResult = new ValidationResult();
                bllDZService.SaveOrUpdate(service, out validationResult);

                string[] tagList = service_Req.tag.Split(',');
                for (int i = 0; i < tagList.Count(); i++)
                {
                    bllDZTag.AddTag(tagList[i], service.Id.ToString(), service.Business.Id.ToString(), service.ServiceType.Id.ToString());
                }

                RespDataSVC001001 respData = new RespDataSVC001001();
                respData.svcID = service.Id.ToString();
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
}


