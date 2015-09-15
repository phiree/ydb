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
/// 获取一条服务信息的详情
/// </summary>
public class ResponseORM001005 : BaseResponse
{
    public ResponseORM001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM001005 requestData = this.request.ReqData.ToObject<ReqDataORM001005>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        

        try
        {
           
            DZMembership member = p.GetUserById(new Guid(requestData.userID));
            if (member == null)
            {
                this.state_CODE = Dicts.StateCode[8];
                this.err_Msg = "用户不存在,可能是传入的uid有误";
                return;
            }
            //验证用户的密码
            if (member.Password != FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.pWord, "MD5"))
            {
                this.state_CODE = Dicts.StateCode[9];
                this.err_Msg = "用户密码错误";
                return;
            }
            try
            {
              
                 ServiceOrder order = bllServiceOrder.GetOne(new Guid(requestData.orderID));

                 if (order == null)
                 {
                     this.state_CODE = Dicts.StateCode[4];
                     this.err_Msg ="没有对应的服务,请检查传入的orderID";
                     return;
                 }
                 RespDataORM_Order respData = new RespDataORM_Order().Adap(order);
                this.RespData =  respData ;
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

public class ReqDataORM001005
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
}


