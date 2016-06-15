﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Dianzhu.Api.Model;

/// <summary>
/// 获取一条服务信息的详情
/// </summary>
public class ResponseCLM001001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseCLM001001(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataCLM001001 requestData = this.request.ReqData.ToObject<ReqDataCLM001001>();

        bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLComplaint bllComplaint = new BLLComplaint();
        string raw_id = requestData.userID;

        try
        {
            Guid userId,orderId;
            bool isUserId = Guid.TryParse(requestData.userID, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(requestData.orderID, out orderId);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户orderId格式有误";
                return;
            }

            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            try
            {
                ServiceOrder order = bllServiceOrder.GetOne(orderId);
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的订单,请检查传入的orderID";
                    return;
                }

                Complaint complaint = new Complaint();
                complaint.Order = order;
                complaint.Target = (enum_ChatTarget)Enum.Parse(typeof(enum_ChatTarget), requestData.target);
                complaint.Context = requestData.context;
                complaint.ResourcesUrl = requestData.resourcesUrl;
                complaint.Operator = order.Customer;
                bllComplaint.SaveOrUpdate(complaint);

                this.state_CODE = Dicts.StateCode[0];
            }
            catch (Exception ex)
            {
                PHSuit.ExceptionLoger.ExceptionLog(ilog,ex);
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


