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
/// 修改店铺信息
/// </summary>
public class ResponseSTORE001003 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");
    DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();
    BLLBusiness bllBusiness = Installer.Container.Resolve<BLLBusiness>();
    BLLBusinessImage bllBusinessImage = Installer.Container.Resolve<BLLBusinessImage>();
    BLLArea bllArea = Installer.Container.Resolve<BLLArea>();

    public ResponseSTORE001003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSTORE001003 requestData = this.request.ReqData.ToObject<ReqDataSTORE001003>();

        //todo:用户验证的复用.
    
        try
        {
            string raw_id = requestData.merchantID;
            RespDataSTORE_storeObj storeObj_ReqData = requestData.storeObj;
            

            Guid storeID,userID;
            bool isUserId = Guid.TryParse(raw_id, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "merchantID格式有误";
                return;
            }

            bool isStoreId = Guid.TryParse(storeObj_ReqData.userID, out storeID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
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
                Business store = bllBusiness.GetBusinessByIdAndOwner(storeID, userID);
                if (store == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺不存在！";
                    return;
                }

                if (store.Enabled == false)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺已删除，无法修改信息！";
                    return;
                }

                Business storeOriginal = new Business();
                store.CopyTo(storeOriginal);
                
                RespDataSTORE_storeObj storeObj = new RespDataSTORE_storeObj();

                if (storeObj_ReqData.alias != null) { store.Name = storeObj_ReqData.alias; storeObj.alias = "Y"; }
                if (storeObj_ReqData.area != null)
                {
                    Area a = bllArea.GetAreaByAreaname(storeObj_ReqData.area);
                    if (a != null)
                    {
                        store.AreaBelongTo = a;
                        storeObj.area = "Y";
                    }
                }

                if (storeObj_ReqData.doc != null) { store.Description = storeObj_ReqData.doc; storeObj.doc = "Y"; }

                if(storeObj_ReqData.imgUrl != null)
                {
                    BusinessImage bImage = new BusinessImage();
                    bImage.ImageName = storeObj_ReqData.imgUrl.Replace(Dianzhu.Config.Config.GetAppSetting("MediaGetUrl"),"");
                    store.BusinessAvatar = bImage;
                    storeObj.imgUrl = "Y";
                }

                if (storeObj_ReqData.linkMan != null) { store.Description = storeObj_ReqData.linkMan; storeObj.linkMan = "Y"; }
                if (storeObj_ReqData.address != null) { store.Address = storeObj_ReqData.address; storeObj.address = "Y"; }
                if (storeObj_ReqData.phone != null) { store.Phone = storeObj_ReqData.phone; storeObj.phone = "Y"; }

                if (storeObj_ReqData.showImgUrls != null)
                {
                    string[] imageList = storeObj_ReqData.showImgUrls.Split(',');
                    BusinessImage bImageObj = new BusinessImage();
                    IList<BusinessImage> bImageList = new List<BusinessImage>();
                    for (int i = 0; i < imageList.Count(); i++)
                    {
                        bImageObj = new BusinessImage();
                        bImageObj.ImageName = imageList[0];
                        bImageList.Add(bImageObj);

                        storeObj.showImgUrls = "Y";
                    }
                }

                ValidatorBusiness vd_staff = new ValidatorBusiness();
                FluentValidation.Results.ValidationResult result = vd_staff.Validate(store);
                foreach (FluentValidation.Results.ValidationFailure f in result.Errors)
                {
                    switch (f.PropertyName.ToLower())
                    {
                        //只有不为null的才需要
                        case "alias":
                            if (storeObj.alias != null) { storeObj.alias = "N"; store.Name = storeOriginal.Name; }
                            break;
                        case "area":
                            if (storeObj.area != null) { storeObj.area = "N"; store.AreaBelongTo = storeOriginal.AreaBelongTo; }
                            break;
                        case "doc":
                            if (storeObj.doc != null) { storeObj.doc = "N"; store.Description = storeOriginal.Description; }
                            break;
                        case "imgurl":
                            if (storeObj.imgUrl != null) { storeObj.imgUrl = "N"; store.BusinessAvatar = storeOriginal.BusinessAvatar; }
                            break;
                        case "linkman":
                            if (storeObj.linkMan != null) { storeObj.linkMan = "N"; store.Contact = storeOriginal.Contact; }
                            break;
                        case "address":
                            if (storeObj.address != null) { storeObj.address = "N"; store.Address = storeOriginal.Address; }
                            break;
                        case "phone":
                            if (storeObj.phone != null) { storeObj.phone = "N"; store.Phone = storeOriginal.Phone; }
                            break;
                        case "showimgurls":
                            if (storeObj.showImgUrls != null) { storeObj.showImgUrls = "N"; store.BusinessImages = storeOriginal.BusinessImages; }
                            break;
                        default: break;
                    }
                }

                //bllStaff.Update(staff);
                bllBusiness.Update(store);

                RespDataSTORE001003 respData = new RespDataSTORE001003(storeID.ToString());
                this.state_CODE = Dicts.StateCode[0];
                storeObj.userID = respData.storeObj.userID;
                respData.storeObj = storeObj;
                this.RespData = respData.storeObj;
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

    //private string DownloadToMediaserver(string fileUrl)
    //{
    //    string url = Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl");
    //    var respData = new NameValueCollection();
    //    respData.Add("fileUrl", HttpUtility.UrlEncode(fileUrl));
    //    respData.Add("originalName", string.Empty);
    //    respData.Add("domainType", "StaffAvatar");
    //    respData.Add("fileType", "image");

    //    return HttpHelper.CreateHttpRequest(url.ToString(), "post", respData);
    //}

    public override string BuildJsonResponse()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}


