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
/// <summary>
/// 用户设备认证
/// </summary>
public class ResponseUSM001007 : BaseResponse
{
    public ResponseUSM001007(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001007 requestData = this.request.ReqData.ToObject<ReqDataUSM001007>();

        DZMembershipProvider p = new DZMembershipProvider();
        string raw_id = requestData.userID;

        try
        {
           
            
             DZMembership member;
             bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }
            try
            {
                this.state_CODE = Dicts.StateCode[0];
                RespDataUSM001007 respData = new RespDataUSM001007();
                respData.userID = requestData.userID;
               
                string savedFileName = MediaServer.HttpUploader.Upload(ConfigurationManager.AppSettings["MediaUploadUrl"],
                   requestData.imgData, string.Empty, "UserAvatar"
                   ,"image");
                respData.imgUrl = ConfigurationManager.AppSettings["MediaGetUrl"] + savedFileName;
                ////上传图片.
                ////bllDeviceBind.UpdateDeviceBindStatus(member, requestData.appToken, requestData.appName);
                //string fileName = Guid.NewGuid() + ".png";
                //string relativePath = System.Configuration.ConfigurationManager.AppSettings["business_image_root"];
                //string filePath = HttpContext.Current.Server.MapPath(relativePath);
                //PHSuit.IOHelper.SaveFileFromBase64(requestData.imgData, filePath+fileName);
                
                //respData.imgUrl =ConfigurationManager.AppSettings["media_server"]+"imagehandler.ashx?imagename="+fileName;
                member.AvatarUrl = savedFileName;
                p.UpdateDZMembership(member);
                this.RespData = respData;
                
            }
            catch (Exception ex)
            { 
                this.state_CODE=ex.Message;   
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

        return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}

public class ReqDataUSM001007
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string imgData { get; set; }
 
}
public class RespDataUSM001007
{
    public string userID { get; set; }
    public string imgUrl { get; set; }
}

 