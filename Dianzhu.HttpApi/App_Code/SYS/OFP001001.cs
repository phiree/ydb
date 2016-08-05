using System;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.Model.Enums;
using System.Collections.Generic;
using System.Configuration;
using Dianzhu.Api.Model;

/// <summary>
/// 实时汇报用户的状态
/// </summary>
public class ResponseOFP001001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

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
 
            BLLReceptionStatus bllReceptionStatus = Bootstrap.Container.Resolve<BLLReceptionStatus>(); 
            BLLReceptionStatusArchieve bllReceptionStatusArchieve = Bootstrap.Container.Resolve<BLLReceptionStatusArchieve>();
 
           
        
            DZMembershipProvider bllMember = Bootstrap.Container.Resolve<DZMembershipProvider>();
 

            IMUserStatus imOld = bllIMUserStatus.GetIMUSByUserId(userId);
            if (imOld != null)//有数据执行更新操作
            {
                //先执行存档
                IMUserStatusArchieve imUSA = new IMUserStatusArchieve();
                imUSA.UserIdRaw = imOld.UserIdRaw;
                imUSA.UserID = imOld.UserID;
                imUSA.Status = imOld.Status;
                imUSA.IpAddress = imOld.IpAddress;
                imUSA.OFIpAddress = imOld.OFIpAddress;
                imUSA.ClientName = imOld.ClientName;
                bllIMUserStatusArchieve.Save(imUSA);

                //更新用户状态
                imOld.Status = currentIM.Status;
                imOld.IpAddress = currentIM.IpAddress;
                imOld.OFIpAddress = ofIp;
                imOld.ClientName = clientName;
                imOld.LastModifyTime = DateTime.Now;
                bllIMUserStatus.Update(imOld);
            }
            else
            {
                //直接存储用户状态
                currentIM.UserID = userId;
                currentIM.OFIpAddress = ofIp;
                currentIM.ClientName = clientName;
                currentIM.LastModifyTime = DateTime.Now;
                bllIMUserStatus.Save(currentIM);
            }

            //更新当前接待类
            //bool isCustom = false;//是否为用户

            DZMembership member = bllMember.GetUserById(userId);
            if (member == null)
            {
                ilog.Error("该用户不存在！访问参数UserId为：" + userId);
                this.state_CODE = Dicts.StateCode[4];
                this.err_Msg = "该用户不存在";
                return;
            }

            //ReceptionStatus rs = bllReceptionStatus.GetOneByCustomer(userId);
            //DZMembership cs = bllReceptionStatus.GetCustomListByCSId(userId);
            //if (rs != null && cs != null)
            //{
            //    //log
            //    ilog.Error("同时存在客服和用户数据！访问参数UserId为：" + userId + "，作为用户对应的ReceptionStatusId为：" + rs.Id + "，作为客服对应的其中一条ReceptionStatusId为：" + cs.Id);
            //    this.state_CODE = Dicts.StateCode[4];
            //    this.err_Msg = "同时存在客服和用户数据！";
            //    return;
            //}
            //else if (rs == null && cs ==null)
            //{
            //    //log
            //    ilog.Error("没有客服或用户数据！访问参数UserId为：" + userId);
            //    this.state_CODE = Dicts.StateCode[4];
            //    this.err_Msg = "没有客服或用户数据！";
            //    return;
            //}
            //else if (rs != null && cs == null)
            //{
            //    isCustom = true;
            //}
            //else if (rs == null && cs != null)
            //{
            //    isCustom = false;
            //}

            switch (currentIM.Status)
            {
                case enum_UserStatus.available:
                    string imServerAPIInvokeUrl = string.Empty;
                    if (member.UserType == enum_UserType.customer)
                    {
                        //用户上线后，通知客服工具
                        imServerAPIInvokeUrl = "type=customlogin&userId=" + userId;
                        VisitIMServerApi(imServerAPIInvokeUrl);
                    }
                    else
                    {
                        //客服上线，通知点点
                        imServerAPIInvokeUrl = "type=cslogin&userId=" + userId;
                        VisitIMServerApi(imServerAPIInvokeUrl);
                    }
                    break;
                case enum_UserStatus.unavailable:
                    string imServerAPIInvokeUrlUn = string.Empty;
                    if (member.UserType == enum_UserType.customer)
                    {
                        //用户下线后，通知客服工具
                        imServerAPIInvokeUrlUn = "type=customlogoff&userId=" + userId;
                        VisitIMServerApi(imServerAPIInvokeUrlUn);

                        //接待关系存档
                        ReceptionStatus rs = bllReceptionStatus.GetOneByCustomer(userId);
                        if (rs == null) return;
                        bllReceptionStatusArchieve.Save(RSToRsa(rs));

                        //删掉接待关系
                        //bllReceptionStatus.Delete(rs);
                    }
                    else
                    {
                        //客服下线后，将正在接待的用户转到其他客服或者点点
                        imServerAPIInvokeUrlUn = "type=cslogoff&userId=" + userId;
                        VisitIMServerApi(imServerAPIInvokeUrlUn);
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

    public ReceptionStatusArchieve RSToRsa(ReceptionStatus rs)
    {
        ReceptionStatusArchieve rsa = new ReceptionStatusArchieve();
        rsa.Customer = rs.Customer;
        rsa.CustomerService = rs.CustomerService;
        rsa.Order = rs.Order;

        return rsa;
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