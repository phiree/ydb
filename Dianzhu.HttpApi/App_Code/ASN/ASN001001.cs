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
/// 新增员工
/// </summary>
public class ResponseASN001001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN001001 requestData = this.request.ReqData.ToObject<ReqDataASN001001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();
       BLLBusiness bllBusiness = Installer.Container.Resolve<BLLBusiness>();
        BLLStaff bllStaff = new BLLStaff();

        try
        {
            string merchant_id = requestData.merchantID;
            string store_id = requestData.storeID;
            RespDataASN_staffObj userObj = requestData.userObj;

            Guid merchantID, storeID;
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

                Staff staff = new Staff();
                staff.Belongto = store;
                staff.Name = staff.NickName = userObj.alias;
                staff.Email = userObj.email;
                staff.Phone = userObj.phone;
                staff.Address = userObj.address;
                staff.Photo = DownloadToMediaserver(userObj.imgUrl);

                bllStaff.Save(staff);
                RespDataASN_staffObj staffObj = new RespDataASN_staffObj().Adapt(staff);

                RespDataASN001001 respData = new RespDataASN001001();
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

    private string DownloadToMediaserver(string fileUrl)
    {
        string url = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
        var respData = new NameValueCollection();
        respData.Add("fileUrl", HttpUtility.UrlEncode(fileUrl));
        respData.Add("originalName", string.Empty);
        respData.Add("domainType", "StaffAvatar");
        respData.Add("fileType", "image");

        return HttpHelper.CreateHttpRequest(url.ToString(), "post", respData);
    }
}


