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
public class ResponseSVC001004 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseSVC001004(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVC001004 requestData = this.request.ReqData.ToObject<ReqDataSVC001004>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
        BLLDZService bllDZService = new BLLDZService();

        try
        {
            string raw_id = requestData.merchantID;
            string store_id = requestData.storeID;

            Guid userID,storeID;
            bool isUserId = Guid.TryParse(raw_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
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

                int sum = bllDZService.GetSumByBusiness(business);

                RespDataSVC001004 respData = new RespDataSVC001004();
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
}


