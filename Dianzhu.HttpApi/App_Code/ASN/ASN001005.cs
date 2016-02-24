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
        DZMembershipProvider p = new DZMembershipProvider();
        BLLBusiness bllBusiness = new BLLBusiness();
        BLLStaff bllStaff = new BLLStaff();

        try
        {
            string store_id = requestData.storeID;
            string user_id = requestData.userID;

            Guid storeID,userID;
            bool isStoreId = Guid.TryParse(store_id, out storeID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "storeId格式有误";
                return;
            }

            bool isUserId = Guid.TryParse(user_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            Business store = bllBusiness.GetOne(storeID);
            if (store == null)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "该店铺不存在！";
                return;
            }

            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(store.Owner.Id, requestData.pWord, this, out member);
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


