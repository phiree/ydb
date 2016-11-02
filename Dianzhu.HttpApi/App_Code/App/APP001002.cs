using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using System.Net;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseAPP001002:BaseResponse
{
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.HttpApi.ResponseApp001002");
    public ResponseAPP001002(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataAPP001002 requestData = this.request.ReqData.ToObject<ReqDataAPP001002>();

 
        BLLDeviceBind bllDeviceBind = Bootstrap.Container.Resolve<BLLDeviceBind>();
 

        Guid uuId;

        try
        {
            bool uuidisGuid = Guid.TryParse(requestData.appUUID, out uuId);
            if (!uuidisGuid)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "UUId格式有误";
                return;
            }

            DeviceBind obj = bllDeviceBind.getDevBindByUUID(uuId);
            if (obj == null)
            {
                log.Debug("设备不存在,uuid:" + uuId);

                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "设备不存在";
                return;
            }
            obj.PushAmount = 0;
            bllDeviceBind.Update(obj);

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
}