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
/// 服务信息总数获取.
/// </summary>
public class ResponseSVC001001 : BaseResponse
{
    public ResponseSVC001001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataSVC001001 requestData = this.request.ReqData.ToObject<ReqDataSVC001001>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        string raw_id = requestData.userID;

        try
        {
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
            {   //old svc001001
                //string srvTarget = requestData.srvTarget;
                //enum_OrderSearchType searchType = (enum_OrderSearchType)Enum.Parse(typeof(enum_OrderSearchType), srvTarget);
               
                //int rowCount = bllServiceOrder.GetServiceOrderCount(uid,searchType);
                //RespDataSVC001001 respData=new RespDataSVC001001{ sum=rowCount.ToString()};
                //this.RespData =  respData ;
                //this.state_CODE = Dicts.StateCode[0];
                //创建一项服务.
                
            }
            catch (Exception ex)
            { 
                this.state_CODE=Dicts.StateCode[2];
                this.err_Msg = ex.Message;
            }
           
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }

    }
    public override string BuildJsonResponse()
    {

        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}
 
 