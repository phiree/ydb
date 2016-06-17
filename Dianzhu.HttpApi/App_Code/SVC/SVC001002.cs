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
public class ResponseSVC001002 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseSVC001002(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVC001002 requestData = this.request.ReqData.ToObject<ReqDataSVC001002>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();

        BLLDZService bllDZService = new BLLDZService();
        BLLServiceType bllServiceType = Bootstrap.Container.Resolve < BLLServiceType>();
        BLLDZTag bllDZTag = new BLLDZTag();

        try
        {
            string raw_id = requestData.merchantID;
            string svc_id = requestData.svcID;

            Guid userID,svcID;
            bool isUserId = Guid.TryParse(raw_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isSvcId = Guid.TryParse(svc_id, out svcID);
            if (!isUserId)
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
                if (service.Business.Owner != member)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "商户没有该服务！";
                    return;
                }

                if (service.IsDeleted)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该服务已删除！";
                    return;
                }

                ValidationResult vResult = new ValidationResult();
                service.IsDeleted = false;
                bllDZService.SaveOrUpdate(service, out vResult);
                if (!vResult.IsValid)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    foreach(ValidationFailure vr in vResult.Errors)
                    {
                        this.err_Msg += vr.ErrorCode + ":" + vr.ErrorMessage + "\n";
                    }
                    return;
                }
                
                this.state_CODE = Dicts.StateCode[0];
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


