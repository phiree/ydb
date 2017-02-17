using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using System.Net;
using Ydb.Common;
using Dianzhu.Api.Model;
using Ydb.Push.DomainModel;
using Ydb.Push.Application;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseAPP001001:BaseResponse
{
    IDZMembershipService bllMembership = Bootstrap.Container.Resolve<IDZMembershipService>();
 
    MemberDto member;

    public ResponseAPP001001(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataAPP001001 requestData = this.request.ReqData.ToObject<ReqDataAPP001001>();

 
       IDeviceBindService deviceBindService   = Bootstrap.Container.Resolve<IDeviceBindService>();
        
 

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

            if (requestData.appObj.userId != "")
            {
                bool isGuid = Guid.TryParse(requestData.appObj.userId, out userId);
                if (!isGuid)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "UserId格式有误";
                    return;
                }

                member = bllMembership.GetUserById( requestData.appObj.userId);
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
                DeviceBind uuid = deviceBindService.getDevBindByUUID(devB.AppUUID);
                if (uuid != null)
                {
                    uuid.DZMembershipId = devB.DZMembershipId;
                    uuid.AppName = devB.AppName;
                    uuid.AppToken = devB.AppToken;
                    uuid.BindChangedTime = DateTime.Now;
                    deviceBindService.Update(uuid);
                }
                else
                {
                    devB.IsBinding = true;
                    devB.BindChangedTime = DateTime.Now;
                    devB.SaveTime = DateTime.Now;
                    deviceBindService.Save(devB);
                }
            }
            else if (requestData.mark == "N")
            {
                DeviceBind uuid = deviceBindService.getDevBindByUUID(devB.AppUUID);
                if (uuid != null)
                {
                    uuid.IsBinding = false;
                    uuid.BindChangedTime = DateTime.Now;
                    deviceBindService.Update(uuid);
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
        result.DZMembershipId = member.Id.ToString();        
        result.AppName = app.appObj.appName;
        result.AppUUID = new Guid(app.appObj.appUUID);
        result.AppToken = app.appObj.appToken;

        return result;
    }
}