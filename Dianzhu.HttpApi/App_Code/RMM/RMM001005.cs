using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dianzhu.Api.Model;
/// <summary>
/// 获取一条服务信息的详情
/// </summary>
public class ResponseRMM001005 : BaseResponse
{
    public ResponseRMM001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

        ReqDataRMM001005 requestData = this.request.ReqData.ToObject<ReqDataRMM001005>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();
        BLLServiceOrderRemind bllServcieOrderRemind = new BLLServiceOrderRemind();
        string user_id = requestData.userID;
        string remind_id = requestData.remindID;

        try
        {
            Guid userId, remingId;
            bool isUserId = Guid.TryParse(user_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误!";
                return;
            }

            bool isRemindId = Guid.TryParse(remind_id, out remingId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误!";
                return;
            }

            DZMembership member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(userId, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = p.GetUserById(userId);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "用户不存在!";
                    return;
                }
            }
            try
            {
                ServiceOrderRemind remind = bllServcieOrderRemind.GetOneByIdAndUserId(remingId, userId);
                if (remind == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该提醒不存在!";
                    return;
                }

                RespDataRMM_remindObj remindObj = new RespDataRMM_remindObj().Adapt(remind);

                RespDataRMM001005 respData = new RespDataRMM001005();
                respData.remindObj = remindObj;
                this.state_CODE = Dicts.StateCode[0];
                this.RespData = respData;
            }
            catch (Exception ex)
            {
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

}

