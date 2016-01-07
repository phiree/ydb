using System;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.Model.Enums;
using System.Collections.Generic;
using System.Configuration;

/// <summary>
/// 实时汇报用户的状态
/// </summary>
public class ResponseOFP001001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("dz");

    public ResponseOFP001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataOFP001001 requestData = this.request.ReqData.ToObject<ReqDataOFP001001>();

        Guid userId;
        string ofIp;
        string clientName;

        try
        {
            if (requestData.jid != null)
            {
                string uid = requestData.jid.Split('@')[0];
                string rest = requestData.jid.Split('@')[1];

                bool uidisGuid = Guid.TryParse(uid, out userId);
                if (!uidisGuid)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "用户Id格式有误";
                    return;
                }

                ofIp = rest.Split('/')[0];
                clientName = rest.Split('/')[1];
            }
            else
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "JId为空";
                return;
            }

            BLLIMUserStatus bllIMUserStatus = new BLLIMUserStatus();
            BLLIMUserStatusArchieve bllIMUserStatusArchieve = new BLLIMUserStatusArchieve();
            IMUserStatus currentIM = reqDataToImData(requestData);
            BLLReceptionStatus bllReceptionStatus = new BLLReceptionStatus();

            IMUserStatus imOld = bllIMUserStatus.GetIMUSByUserId(userId);
            if (imOld != null)//有数据执行更新操作
            {
                //先执行存档
                IMUserStatusArchieve imUSA = new IMUserStatusArchieve();
                imUSA.UserIdRaw = imOld.UserIdRaw;
                imUSA.UserID = imOld.UserID;
                imUSA.Status = imOld.Status;
                imUSA.ArchieveTime = DateTime.Now;
                imUSA.IpAddress = imOld.IpAddress;
                imUSA.OFIpAddress = imOld.OFIpAddress;
                imUSA.ClientName = imOld.ClientName;
                bllIMUserStatusArchieve.SaveOrUpdate(imUSA);

                //更新用户状态
                imOld.Status = currentIM.Status;
                imOld.IpAddress = currentIM.IpAddress;
                imOld.OFIpAddress = ofIp;
                imOld.ClientName = clientName;
                imOld.LastModifyTime = DateTime.Now;
                bllIMUserStatus.SaveOrUpdate(imOld);
            }
            else
            {
                //直接存储用户状态
                currentIM.UserID = userId;
                currentIM.OFIpAddress = ofIp;
                currentIM.ClientName = clientName;
                currentIM.LastModifyTime = DateTime.Now;
                bllIMUserStatus.SaveOrUpdate(currentIM);
            }

            //更新当前接待类
            bool isCustom = false;//是否为用户
            ReceptionStatus rs = bllReceptionStatus.GetOneByCustomer(userId);
            DZMembership cs = bllReceptionStatus.GetCustomListByCSId(userId);
            if (rs != null && cs != null)
            {
                //log
                ilog.Error("访问参数UserId为：" + userId + "，作为用户对应的ReceptionStatusId为：" + rs.Id + "，作为客服对应的其中一条ReceptionStatusId为：" + cs.Id);
                this.state_CODE = Dicts.StateCode[4];
                this.err_Msg = "同时存在客服和用户数据！";
                return;
            }
            else if (rs == null && cs ==null)
            {
                //log
                ilog.Error("访问参数UserId为：" + userId);
                this.state_CODE = Dicts.StateCode[4];
                this.err_Msg = "没有客服或用户数据！";
                return;
            }
            else if (rs != null && cs == null)
            {
                isCustom = true;
            }
            else if (rs == null && cs != null)
            {
                isCustom = false;
            }

            switch (currentIM.Status)
            {
                case enum_UserStatus.available:
                    
                    break;
                case enum_UserStatus.unavailable:
                    string imServerAPIInvokeUrl = string.Empty;
                    if (isCustom)
                    {                        
                        //用户下线后，通知客服工具
                        imServerAPIInvokeUrl= "IMServerAPI.ashx?type=customlogoff&userId=" + userId;
                        VisitIMServerApi(imServerAPIInvokeUrl);

                        //删掉接待关系
                        bllReceptionStatus.Delete(rs);
                    }
                    else
                    {
                        //客服下线后，将正在接待的用户转到其他客服或者点点
                        imServerAPIInvokeUrl = "IMServerAPI.ashx?type=cslogoff&userId=" + userId;
                        VisitIMServerApi(imServerAPIInvokeUrl);
                    }                    
                    break;
                default:
                    break;
            }

            try
            {
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

public class ReqDataOFP001001
{
    public string jid { get; set; }
    public string status { get; set; }
    public string ipaddress { get; set; }
}