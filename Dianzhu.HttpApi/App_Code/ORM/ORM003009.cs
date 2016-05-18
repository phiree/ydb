using System;
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
public class ResponseORM003009 : BaseResponse
{
    public ResponseORM003009(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM003009 requestData = this.request.ReqData.ToObject<ReqDataORM003009>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLServiceOrderAppraise bllServiceOrderAppraise = new BLLServiceOrderAppraise();

        string user_ID = requestData.userID;
        string order_ID = requestData.orderID;
        string targetStr = requestData.target;
        string appraiseValueStr = requestData.appraiseValue;
        string appraiseDocs = requestData.appraiseDocs;

        try
        {
            Guid userID, orderID;
            decimal appraiseValue = 0;
            bool isUserId = Guid.TryParse(user_ID, out userID);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(order_ID, out orderID);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户orderId格式有误";
                return;
            }

            string[] targetList = targetStr.Split('|');
            string[] valueList = appraiseValueStr.Split('|');

            IList<ReqDataORM003009_item> itemList = new List<ReqDataORM003009_item>();
            ReqDataORM003009_item item;
            if (targetList.Count() == 2 && valueList.Count() == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    enum_ChatTarget target;
                    if(!Enum.TryParse(targetList[i],out target))
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "请传入正确的评价对象";
                        return;
                    }

                    decimal appValue;
                    if (!decimal.TryParse(valueList[0], out appValue))
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "请输入正确价格";
                        return;
                    }
                    if (appValue < 0 || appValue > 5 || appValue % 5 != 0)
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "请输入正确价格";
                        return;
                    }

                    item = new ReqDataORM003009_item();
                    item.target = target;
                    item.value = appValue;
                    itemList.Add(item);
                }
            }
            else
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "target或appraiseValue有误";
                return;
            }

            //bool isAppraiseValue = decimal.TryParse(appraise_Value, out appraiseValue);
            //if (!isAppraiseValue)
            //{
            //    this.state_CODE = Dicts.StateCode[1];
            //    this.err_Msg = "请输入正确价格!";
            //    return;
            //}

            //if (appraiseValue < 0 || appraiseValue > 5)
            //{
            //    this.state_CODE = Dicts.StateCode[1];
            //    this.err_Msg = "评价值应在(0-5)之间!";
            //    return;
            //}

            //if (appraiseValue % 5 != 0)
            //{
            //    this.state_CODE = Dicts.StateCode[1];
            //    this.err_Msg = "评价值有误!";
            //    return;
            //}

            DZMembership member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(userID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = p.GetUserById(userID);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "用户不存在!";
                    return;
                }
            }
            try
            {
                ServiceOrder order = bllServiceOrder.GetOrderByIdAndCustomer(orderID, member);
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的订单!";
                    return;
                }

                ServiceOrderAppraise appraise;
                foreach (ReqDataORM003009_item obj in itemList)
                {
                    appraise = new ServiceOrderAppraise(order, obj.target, obj.value, appraiseDocs);
                    bllServiceOrderAppraise.Save(appraise);
                }

                bllServiceOrder.OrderFlow_CustomerAppraise(order);

                string resultStatus = order.OrderStatus.ToString();

                RespDataORM003009 respData = new RespDataORM003009();
                respData.resultStatus = resultStatus;

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


