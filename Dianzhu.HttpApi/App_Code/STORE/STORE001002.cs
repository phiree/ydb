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

/// <summary>
/// 删除店铺
/// </summary>
public class ResponseSTORE001002 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseSTORE001002(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSTORE001002 requestData = this.request.ReqData.ToObject<ReqDataSTORE001002>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Installer.Container.Resolve<BLLBusiness>();
        try
        {
            string raw_id = requestData.merchantID;
            string store_id = requestData.storeID;

            Guid userID,storeID;
            bool isUserId = Guid.TryParse(raw_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "merchantID格式有误";
                return;
            }

            bool isStoreId = Guid.TryParse(store_id, out storeID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "storeId格式有误";
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
                Business b = bllBusiness.GetBusinessByIdAndOwner(storeID, userID);
                if (b == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺不存在！";
                    return;
                }

                if (b.Enabled == false)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺已删除，无法再次删除！";
                    return;
                }

                b.Enabled = false;
                bllBusiness.Update(b);

                if (b.Enabled)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "数据库删除失败！";
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


