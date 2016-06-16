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
public class ResponseRMM001004 : BaseResponse
{
    public ResponseRMM001004(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

        ReqDataRMM001004 requestData = this.request.ReqData.ToObject<ReqDataRMM001004>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();

        //20150616_longphui_modify
        //BLLServiceOrderRemind bllServcieOrderRemind = new BLLServiceOrderRemind();
        BLLServiceOrderRemind bllServiceOrderRemind = Bootstrap.Container.Resolve<BLLServiceOrderRemind>();


        string user_id = requestData.userID;
        string start_time = requestData.startTime;
        string end_time = requestData.endTime;

        try
        {
            Guid userId;
            bool isUserId = Guid.TryParse(user_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误!";
                return;
            }

            DateTime startTime, endTime;
            try
            {
                string stime= DateTime.ParseExact(start_time, "yyyyMMdd", null).ToString("yyyy-MM-dd HH:mm:ss");
                startTime = DateTime.Parse(stime);
            }
            catch (Exception e)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "startTime格式有误!";
                return;
            }

            try
            {
                string etime= DateTime.ParseExact(end_time, "yyyyMMdd", null).ToString("yyyy-MM-dd HH:mm:ss");
                endTime = DateTime.Parse(etime);
            }
            catch (Exception e)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "endTime格式有误!";
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
                int sum = bllServiceOrderRemind.GetSumByUserIdAndDatetime(userId, startTime, endTime);

                RespDataRMM001004 respData = new RespDataRMM001004();
                respData.sum = sum.ToString();
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

