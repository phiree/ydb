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
public class ResponseRMM001003 : BaseResponse
{
    public ResponseRMM001003(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

        ReqDataRMM001003 requestData = this.request.ReqData.ToObject<ReqDataRMM001003>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();

        //20150616_longphui_modify
        //BLLServiceOrderRemind bllServcieOrderRemind = new BLLServiceOrderRemind();
        BLLServiceOrderRemind bllServiceOrderRemind = Bootstrap.Container.Resolve<BLLServiceOrderRemind>();

        string user_id = requestData.userID;
        string remind_id = requestData.remindID;
        string open = requestData.open;

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
                ServiceOrderRemind remind = bllServiceOrderRemind.GetOneByIdAndUserId(remingId, userId);
                if (remind == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该提醒不存在!";
                    return;
                }

                bool isOpen = true;
                switch (open.ToLower())
                {
                    case "y":
                        isOpen = true;
                        break;
                    case "n":
                        isOpen = false;
                        break;
                    default:
                        ilog.Error("请求的open：" + open + "有误！");
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "请求的open有误!";
                        return;
                }

                if (isOpen == remind.Open)
                {
                    this.state_CODE = Dicts.StateCode[0];
                    return;
                }

                remind.Open = isOpen;
                bllServiceOrderRemind.SaveOrUpdate(remind);

                this.state_CODE = Dicts.StateCode[0];
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

