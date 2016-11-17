using System;
using Dianzhu.Model;using Ydb.Membership.Application;using Ydb.Membership.Application.Dto;
using Dianzhu.BLL;
using Ydb.Common;
using System.Collections.Generic;
using System.Configuration;
using Dianzhu.Api.Model;

/// <summary>
/// 实时汇报用户的状态
/// </summary>
public class ResponseOFP001001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi.OFP001001");

    public ResponseOFP001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataOFP001001 requestData = this.request.ReqData.ToObject<ReqDataOFP001001>();

        Guid userId;
        string ofIp = "";
        string clientName = "";

        try
        {
            if (requestData.jid != null)
            {
                string uid = "";
                string rest = "";
                var jidList = requestData.jid.Split('@');
                if (jidList.Length > 0)
                {
                    uid = jidList[0];
                    rest = jidList[1];
                }                

                bool uidisGuid = Guid.TryParse(uid, out userId);
                if (!uidisGuid)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "用户Id格式有误";
                    return;
                }

                var restList = rest.Split('/');
                if (restList.Length > 1)
                {
                    ofIp = restList[0];
                    clientName = restList[1];
                }
                else if (restList.Length > 0)
                {
                    ofIp = restList[0];
                }

            }
            else
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "JId为空";
                return;
            }

            BLLIMUserStatus bllIMUserStatus = Bootstrap.Container.Resolve<BLLIMUserStatus>();
            BLLIMUserStatusArchieve bllIMUserStatusArchieve = Bootstrap.Container.Resolve<BLLIMUserStatusArchieve>();
            IMUserStatus currentIM = reqDataToImData(requestData);
        
            IDZMembershipService bllMember = Bootstrap.Container.Resolve<IDZMembershipService>();

            enum_UserStatus status;
            if( !Enum.TryParse(requestData.status,out status))
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户status格式有误";
                return;
            }

            //先执行存档
            IMUserStatusArchieve imUSA = new IMUserStatusArchieve();
            imUSA.UserIdRaw = requestData.jid;
            imUSA.UserID = userId;
            imUSA.Status = status;
            imUSA.IpAddress = requestData.ipaddress;
            imUSA.OFIpAddress = ofIp;
            imUSA.ClientName = clientName;
            bllIMUserStatusArchieve.Save(imUSA);

            //IMUserStatus imOld = bllIMUserStatus.GetIMUSByUserId(userId);
            //if (imOld != null)//有数据执行更新操作
            //{
            //    //先执行存档
            //    IMUserStatusArchieve imUSA = new IMUserStatusArchieve();
            //    imUSA.UserIdRaw = imOld.UserIdRaw;
            //    imUSA.UserID = imOld.UserID;
            //    imUSA.Status = imOld.Status;
            //    imUSA.IpAddress = imOld.IpAddress;
            //    imUSA.OFIpAddress = imOld.OFIpAddress;
            //    imUSA.ClientName = imOld.ClientName;
            //    bllIMUserStatusArchieve.Save(imUSA);

            //    //更新用户状态
            //    imOld.Status = currentIM.Status;
            //    imOld.IpAddress = currentIM.IpAddress;
            //    imOld.OFIpAddress = ofIp;
            //    imOld.ClientName = clientName;
            //    imOld.LastModifyTime = DateTime.Now;
            //    bllIMUserStatus.Update(imOld);
            //}
            //else
            //{
            //    //直接存储用户状态
            //    currentIM.UserID = userId;
            //    currentIM.OFIpAddress = ofIp;
            //    currentIM.ClientName = clientName;
            //    currentIM.LastModifyTime = DateTime.Now;
            //    bllIMUserStatus.Save(currentIM);
            //}

            MemberDto member = bllMember.GetUserById(userId.ToString());
            if (member == null)
            {
                ilog.Error("该用户不存在！访问参数UserId为：" + userId);
                this.state_CODE = Dicts.StateCode[4];
                this.err_Msg = "该用户不存在";
                return;
            }

            switch (currentIM.Status)
            {
                case enum_UserStatus.available:
                    string imServerAPIInvokeUrl = string.Empty;
                    if (member.UserType == enum_UserType.customerservice.ToString())
                    {
                        //客服上线，通知点点
                        imServerAPIInvokeUrl = "type=cslogin&userId=" + userId;
                        VisitIMServerApi(imServerAPIInvokeUrl);
                    }
                    break;
                case enum_UserStatus.unavailable:
                    string imServerAPIInvokeUrlUn = string.Empty;
                    if (member.UserType == enum_UserType.customerservice.ToString())
                    {
                        //客服下线后，将正在接待的用户转到其他客服或者点点
                        imServerAPIInvokeUrlUn = "type=cslogoff&userId=" + userId;
                        VisitIMServerApi(imServerAPIInvokeUrlUn);
                    }                    
                    break;
                default:
                    break;
            }

            this.state_CODE = Dicts.StateCode[0];
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            Log.Error(e.ToString());
            return;
        }
    }
    
    public void VisitIMServerApi(string url)
    {
        System.Net.WebClient wc = new System.Net.WebClient();
        string notifyServer = Dianzhu.Config.Config.GetAppSetting("NotifyServer");
        Uri uri = new Uri(notifyServer + url);

        System.IO.Stream returnData = wc.OpenRead(uri);
        System.IO.StreamReader reader = new System.IO.StreamReader(returnData);
        string result = reader.ReadToEnd();
    }

    public IMUserStatus reqDataToImData(ReqDataOFP001001 reqData)
    {
        IMUserStatus im = new IMUserStatus();

        im.UserIdRaw = reqData.jid;
        switch (reqData.status)
        {
            case "available":
                im.Status = enum_UserStatus.available;
                break;
            case "unavailable":
                im.Status = enum_UserStatus.unavailable;
                break;
        }
        im.IpAddress = reqData.ipaddress;

        return im;
    }
}