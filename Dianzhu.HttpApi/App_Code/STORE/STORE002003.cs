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
/// 新增店铺
/// </summary>
public class ResponseSTORE002003 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseSTORE002003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSTORE002003 requestData = this.request.ReqData.ToObject<ReqDataSTORE002003>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
        BLLBusinessImage bllBusinessImage = Bootstrap.Container.Resolve<BLLBusinessImage>();

        try
        {
            string raw_id = requestData.merchantID;
            string store_id = requestData.storeID;
            string imageName = requestData.imgUrl;

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

                bool isDelete = bllBusinessImage.DeleteBusImageByName(imageName.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"),""));

                if (isDelete)
                {
                    this.state_CODE = Dicts.StateCode[0];
                }
                else
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "删除失败！";
                    return;
                }
                
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


