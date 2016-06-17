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
public class ResponseASN001002 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN001002(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN001002 requestData = this.request.ReqData.ToObject<ReqDataASN001002>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
        BLLStaff bllStaff = new BLLStaff();

        try
        {
            string raw_id = requestData.merchantID;
            string user_id = requestData.userID;

            Guid merchantID,userID;
            bool isStoreId = Guid.TryParse(raw_id, out merchantID);
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

                staff.Enable = false;
                bllStaff.Update(staff);

                string result = "N";
                if (!staff.Enable)
                {
                    result = "Y";
                }

                RespDataASN001002 respData = new RespDataASN001002();
                respData.result = result;

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


