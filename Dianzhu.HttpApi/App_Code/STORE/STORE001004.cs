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
public class ResponseSTORE001004 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseSTORE001004(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSTORE001004 requestData = this.request.ReqData.ToObject<ReqDataSTORE001004>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Installer.Container.Resolve<BLLBusiness>();
        try
        {
            string raw_id = requestData.merchantID;

            Guid ownerID;
            bool isStoreId = Guid.TryParse(raw_id, out ownerID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "storeId格式有误";
                return;
            }

            DZMembership member = null;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(ownerID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = p.GetUserById(ownerID);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该商户！";
                    return;
                }
            }
            try
            {
                string sum = bllBusiness.GetEnableSum(member).ToString();

                RespDataSTORE001004 respData = new RespDataSTORE001004();
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


