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
/// <summary>
/// 获取用户的服务订单列表
/// </summary>
public class ResponseORM002001 : BaseResponse
{
    public ResponseORM002001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM002001 requestData = this.request.ReqData.ToObject<ReqDataORM002001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLReceptionStatus BLLReceptionStatus = new BLLReceptionStatus();
        BLLServiceOrder bllOrder = new BLLServiceOrder();
        string raw_id = requestData.userID;

        try
        {
            DZMembership member;
            bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }
            try
            {
    
                RespDataORM002001 respData = new RespDataORM002001();

                DZMembership assignedCustomerService = BLLReceptionStatus.Assign(member,null);
                if (assignedCustomerService == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有在线客服";
                    return;
                }

                string reqOrderId = requestData.orderID;
                Guid orderId;
                if (Guid.TryParse(reqOrderId, out orderId))
                {
                    var reqOrder = bllOrder.GetOne(orderId);
                    if(reqOrder!=null)
                    {
                        if (reqOrder.Customer.Id.ToString() == raw_id)
                        {
                            respData.orderID = reqOrderId;
                        }
                        else {
                            
                        }
                    }
                }

                RespDataORM002001_cerObj cerObj = new RespDataORM002001_cerObj().Adap(assignedCustomerService);
                respData.cerObj = cerObj;
                this.RespData = respData;
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

public class ReqDataORM002001
{
    
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
    

}
public class RespDataORM002001
{
    public RespDataORM002001()
    {
        orderID = string.Empty;
    }
    public string orderID { get; set; }
    public  RespDataORM002001_cerObj cerObj{ get; set; }
 
}
public class RespDataORM002001_cerObj
{
    public string userID { get; set; }
    public string alias { get; set; }
    public string userName { get; set; }
    public string imgUrl { get; set; }
    public RespDataORM002001_cerObj Adap(DZMembership customerService)
    {
        this.userID = customerService.Id.ToString();
        this.imgUrl = string.Empty;
        this.alias = customerService.DisplayName;
        this.userName = customerService.UserName;
        return this;
    }
}

 


