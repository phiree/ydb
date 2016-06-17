using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Net;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseAPP001001:BaseResponse
{
    DZMembershipProvider bllMembership = Bootstrap.Container.Resolve<DZMembershipProvider>();
    
    DZMembership member;

    public ResponseAPP001001(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataAPP001001 requestData = this.request.ReqData.ToObject<ReqDataAPP001001>();

 
        BLLDeviceBind bllDeviceBind = Bootstrap.Container.Resolve<BLLDeviceBind>();
        
 

        Guid uuId;
        Guid userId;

        try
        {
            bool uuidisGuid = Guid.TryParse(requestData.appObj.appUUID, out uuId);
            if (!uuidisGuid)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "UUId格式有误";
                return;
            }

            if (requestData.appObj.appToken.Length > 64)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "Token长度超过64";
                return;
            }
            else if (requestData.appObj.appToken.Length < 64)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "Token长度不够64";
                return;
            }

            if (requestData.appObj.userId != "")
            {
                bool isGuid = Guid.TryParse(requestData.appObj.userId, out userId);
                if (!isGuid)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "UserId格式有误";
                    return;
                }

                member = bllMembership.GetUserById(new Guid(requestData.appObj.userId));
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[8];
                    this.err_Msg = "用户不存在";
                    return;
                }
            }

            DeviceBind devB = reqDataToDevB(requestData);
            if (requestData.mark == "Y" || requestData.mark == null)
            {
                DeviceBind uuid = bllDeviceBind.getDevBindByUUID(devB.AppUUID);
                if (uuid != null)
                {
                    uuid.DZMembership = devB.DZMembership;
                    uuid.AppName = devB.AppName;
                    uuid.AppToken = devB.AppToken;
                    uuid.BindChangedTime = DateTime.Now;
                    bllDeviceBind.Save(uuid);
                }
                else
                {
                    devB.IsBinding = true;
                    devB.BindChangedTime = DateTime.Now;
                    devB.SaveTime = DateTime.Now;
                    bllDeviceBind.Update(devB);
                }
            }
            else if (requestData.mark == "N")
            {
                DeviceBind uuid = bllDeviceBind.getDevBindByUUID(devB.AppUUID);
                if (uuid != null)
                {
                    uuid.IsBinding = false;
                    uuid.BindChangedTime = DateTime.Now;
                    bllDeviceBind.Save(uuid);
                }
                else
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "设备不存在";
                    return;
                }
            }

            this.state_CODE = Dicts.StateCode[0];
            return;
        }
        catch (Exception ex)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = ex.Message;
            return;
        }
    }

    public DeviceBind reqDataToDevB(ReqDataAPP001001 app)
    {
        DeviceBind result = new DeviceBind();
        result.DZMembership = member;        
        result.AppName = app.appObj.appName;
        result.AppUUID = new Guid(app.appObj.appUUID);
        result.AppToken = app.appObj.appToken;

        return result;
    }
}