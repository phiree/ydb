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
        string raw_id = requestData.userID;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
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
                
               

                RespDataORM002001 respData = new RespDataORM002001();

                DZMembership assignedCustomerService = BLLReceptionStatus.Assign(member,null);
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
        this.alias = customerService.NickName;
        this.userName = customerService.UserName;
        return this;
    }
}

 


