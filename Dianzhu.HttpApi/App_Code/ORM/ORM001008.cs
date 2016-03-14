﻿using System;
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
/// 获取用户的服务订单列表
/// </summary>
public class ResponseORM001008 : BaseResponse
{
    public ResponseORM001008(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM001008 requestData = this.request.ReqData.ToObject<ReqDataORM001008>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLDZService bllDZService = new BLLDZService();


        try
        {
            string raw_id = requestData.userID;
            string order_id = requestData.orderID;
            string svc_id = requestData.svcID;

            Guid uid, orderID,svcID;
            bool isUserId = Guid.TryParse(raw_id, out uid);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(order_id, out orderID);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isSvcId = Guid.TryParse(svc_id, out svcID);
            if (!isSvcId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "svcId格式有误";
                return;
            }

            DZMembership member = null;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(uid, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = p.GetUserById(uid);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "不存在该用户！";
                    return;
                }
            }
            try
            {
                ServiceOrder order = bllServiceOrder.GetOne(orderID);

                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单不存在！";
                    return;
                }

                DZService service = bllDZService.GetOne(svcID);
                if (service == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该服务不存在！";
                    return;
                }

                foreach (ServiceOrderDetail detail in order.Details)
                {
                    detail.Selected = detail.OriginalService == service;
                }
                bllServiceOrder.SaveOrUpdate(order);
                RespDataORM_orderObj orderObj = new RespDataORM_orderObj().Adap(order);

                RespDataORM001008 respData = new RespDataORM001008();
                respData.orderObj = orderObj;

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



