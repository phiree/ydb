using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.Api.Model;

/// <summary>
/// 删除员工
/// </summary>
public class ResponseASN001006 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN001006(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN001006 requestData = this.request.ReqData.ToObject<ReqDataASN001006>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();

        BLLStaff bllStaff = Bootstrap.Container.Resolve<BLLStaff>();

        try
        {
            string merchant_id = requestData.merchantID;
            string store_id = requestData.storeID;

            Guid merchantID,storeID;
            bool isMerchantId = Guid.TryParse(merchant_id, out merchantID);
            if (!isMerchantId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "merchantID格式有误";
                return;
            }

            bool isStoreId = Guid.TryParse(store_id, out storeID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "storeID格式有误";
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
                Business store = bllBusiness.GetOne(storeID);
                if (store == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺不存在！";
                    return;
                }

                if (store.Owner.Id != merchantID)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "您没有该店铺！";
                    return;
                }

                IList<Staff> staffList = bllStaff.GetAllListByBusiness(store);

                RespDataASN001006 respData = new RespDataASN001006().AdaptList(staffList);

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


