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
        DZMembershipProvider p = new DZMembershipProvider();
        BLLBusiness bllBusiness = new BLLBusiness();
        BLLStaff bllStaff = new BLLStaff();

        try
        {
            string raw_id = requestData.storeID;
            RespDataUSM_userObj userObj = requestData.userObj;

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

            DZMembership member;
            bool validated = new Account(p).ValidateUser(store.Owner.Id, requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }
            try
            {
                Staff staff = new Staff();
                staff.Belongto = store;
                staff.Name = staff.NickName = userObj.alias;
                staff.Email = userObj.email;
                staff.Phone = userObj.phone;
                staff.Address = userObj.address;
                staff.Photo = DownloadToMediaserver(userObj.imgUrl);

                bllStaff.Save(staff);

                RespDataASN001001 respData = new RespDataASN001001().Adapt(staff);

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


