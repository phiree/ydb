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
using Dianzhu.BLL.Validator;
using Newtonsoft.Json;

/// <summary>
/// 修改员工信息
/// </summary>
public class ResponseASN001003 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN001003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN001003 requestData = this.request.ReqData.ToObject<ReqDataASN001003>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Installer.Container.Resolve<BLLBusiness>(); BLLStaff bllStaff = new BLLStaff();

        try
        {
            string raw_id = requestData.merchantID;
            string user_id = requestData.userID;

            string alias = requestData.alias;
            string email = requestData.email;
            string phone = requestData.phone;
            string address = requestData.address;
            string imgUrl = requestData.imgUrl;

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

                Staff staffOriginal = new Staff();
                staff.CopyTo(staffOriginal);

                RespDataASN_staffObj staffObj = new RespDataASN_staffObj();

                if (alias != null) { staff.NickName = alias; staffObj.alias = "Y"; }
                if (email != null) { staff.Email = email; staffObj.email = "Y"; }
                if (phone != null) { staff.Phone = phone; staffObj.phone = "Y"; }
                if (address != null) { staff.Address = address; staffObj.address = "Y"; }
                if (imgUrl != null) { staff.Photo = DownloadToMediaserver(imgUrl); staffObj.imgUrl = "Y"; }

                ValidatorStaff vd_staff = new ValidatorStaff();
                FluentValidation.Results.ValidationResult result = vd_staff.Validate(staff);
                foreach (FluentValidation.Results.ValidationFailure f in result.Errors)
                {
                    switch (f.PropertyName.ToLower())
                    {
                        //只有不为null的菜需要
                        case "alias":
                            if (staffObj.alias != null) { staffObj.alias = "N"; staff.NickName = staffOriginal.NickName; }
                            break;
                        case "email":
                            if (staffObj.email != null) { staffObj.email = "N"; staff.Email = staffOriginal.Email; }
                            break;
                        case "phone":
                            if (staffObj.phone != null) { staffObj.phone = "N"; staff.Phone = staffOriginal.Phone; }
                            break;
                        case "address":
                            if (staffObj.address != null) { staffObj.address = "N"; staff.Address = staffOriginal.Address; }
                            break;
                        default: break;
                    }
                }

                bllStaff.Update(staff);

                RespDataASN001003 respData = new RespDataASN001003(userID.ToString());
                this.state_CODE = Dicts.StateCode[0];
                staffObj.userID = respData.staffObj.userID;
                respData.staffObj = staffObj;
                this.RespData = respData.staffObj;
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

    public override string BuildJsonResponse()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}


