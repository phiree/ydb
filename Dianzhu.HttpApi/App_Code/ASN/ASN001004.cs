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
public class ResponseASN001004 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN001004(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN001004 requestData = this.request.ReqData.ToObject<ReqDataASN001004>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLBusiness bllBusiness = new BLLBusiness();
        BLLStaff bllStaff = new BLLStaff();

        try
        {
            string raw_id = requestData.storeID;

            Guid storeID;
            bool isStoreId = Guid.TryParse(raw_id, out storeID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "storeId格式有误";
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
                string sum = bllStaff.GetEnableSum(store).ToString();

                RespDataASN001004 respData = new RespDataASN001004();
                respData.sum = sum;

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


