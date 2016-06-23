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
public class ResponseASN001005 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN001005 requestData = this.request.ReqData.ToObject<ReqDataASN001005>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();

        //20160623_longphui_modify
        //BLLStaff bllStaff = new BLLStaff();
        BLLStaff bllStaff = Bootstrap.Container.Resolve<BLLStaff>();

        try
        {
            string merchant_id = requestData.merchantID;
            string user_id = requestData.userID;

            Guid merchantID,userID;
            bool isStoreId = Guid.TryParse(merchant_id, out merchantID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "merchantID格式有误";
                return;
            }

            bool isUserId = Guid.TryParse(user_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
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
                Staff staff = bllStaff.GetOne(userID);
                if (staff == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该员工不存在！";
                    return;
                }

                if (staff.Belongto.Owner.Id != merchantID)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该商户没有该员工！";
                    return;
                }

                RespDataASN_staffObj staffObj = new RespDataASN_staffObj().Adapt(staff);

                RespDataASN001005 respData = new RespDataASN001005();
                respData.userObj = staffObj;

                this.state_CODE = Dicts.StateCode[0];
                this.RespData = respData.userObj;
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


