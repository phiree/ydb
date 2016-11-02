using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Dianzhu.Api.Model;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
/// <summary>
/// 用户设备认证
/// </summary>
public class ResponseUSM001007 : BaseResponse
{
    public ResponseUSM001007(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001007 requestData = this.request.ReqData.ToObject<ReqDataUSM001007>();

        IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

        string raw_id = requestData.userID;

        try
        {


            MemberDto member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(memberService).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = memberService.GetUserById( raw_id);
            }
            try
            {
                this.state_CODE = Dicts.StateCode[0];
                RespDataUSM001007 respData = new RespDataUSM001007();
                respData.userID = requestData.userID;

                string savedFileName = MediaServer.HttpUploader.Upload(Dianzhu.Config.Config.GetAppSetting("MediaUploadUrl"),
                   requestData.imgData, "UserAvatar", "image");
                respData.imgUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + savedFileName;

                memberService.ChangeAvatar(raw_id, savedFileName);
                
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

