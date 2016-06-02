using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Dianzhu.Api.Model;
/// <summary>
/// 用户设备认证
/// </summary>
public class ResponseUSM001008 : BaseResponse
{
    public ResponseUSM001008(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001008 requestData = this.request.ReqData.ToObject<ReqDataUSM001008>();

        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        string raw_id = requestData.userID;

        try
        {
            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            try
            {
                //上传图片.
                //bllDeviceBind.UpdateDeviceBindStatus(member, requestData.appToken, requestData.appName);
                string ext = string.Empty;
                string domainType = string.Empty;
                switch (requestData.FileType)
                {
                    case USM001008UploadedFileType.image:
                        ext = ".png";
                        domainType = "ChatImage";
                        break;

                    case USM001008UploadedFileType.video:
                        ext = ".mp4";
                        domainType = "ChatVideo";
                        break;
                    case USM001008UploadedFileType.voice:
                        ext = ".mp3";

                        domainType = "ChatAudio";
                        break;
                }
                RespDataUSM001008 respData = new RespDataUSM001008();
                respData.userID = requestData.userID;
                string resourceUrl = string.Empty;
                this.state_CODE = Dicts.StateCode[0];
                string savedFileName = MediaServer.HttpUploader.Upload(Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl"),
                    requestData.Resource, domainType, requestData.FileType == USM001008UploadedFileType.voice ? "voice" : requestData.FileType.ToString());
                resourceUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + savedFileName;


                //string fileName = Guid.NewGuid() + ext;
                //string relativePath = Dianzhu.Config.Config.GetAppSetting("business_image_root");
                //string filePath = HttpContext.Current.Server.MapPath(relativePath);
                //PHSuit.IOHelper.SaveFileFromBase64(requestData.Resource, filePath+fileName);



                //if(requestData.FileType== USM001008UploadedFileType.image)
                //{
                //    resourceUrl = Dianzhu.Config.Config.GetAppSetting("media_server")+"imagehandler.ashx?imagename="+fileName;
                //}
                //else
                //{
                //    resourceUrl = Dianzhu.Config.Config.GetAppSetting("media_server") + Dianzhu.Config.Config.GetAppSetting("business_image_root") + fileName;
                //}
                // member.AvatarUrl = fileName;
                // p.UpdateDZMembership(member);
                respData.ResourceUrl = resourceUrl;
                this.RespData = respData;

            }
            catch (Exception ex)
            {
                this.state_CODE = ex.Message;
            }

        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;

        }

    }
    public override string BuildJsonResponse()
    {

        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}

