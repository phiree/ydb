using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
/// <summary>
///apiRequest 的摘要说明
/// </summary>
public class apiRequest
{
    string protocal_CODE { get; set; }
    string stamp_TIMES { get; set; }
    string serial_NUMBER { get; set; }
    string appToken { get; set; }
    string appName { get; set; }
    string Ver { get; set; }
    string requestBody { get; set; }
	public apiRequest()
	{
		 
        
	}
    
    public void BuildResponse()
    {
        if (protocal_CODE.ToLower() == "usm001001")
        {

            RequestUSM001001 requestData = (RequestUSM001001)JsonConvert.DeserializeObject(requestBody);
            DZMembershipProvider p = new DZMembershipProvider();
            bool result=p.ValidateUser(requestData.userPhone, requestData.userPWord);
           // p.GetUser(requestData.userPhone);
            ResponseUSM001001 resp = new ResponseUSM001001();
           // resp.Adapt(

        }
    }

}
public class RequestUSM001001
{
    public string userEmail { get; set; }
    public string userPhone { get; set; }
    public string userPWord { get; set; }
    

}
 
public class apiResponse
{
    public string protocol_CODE { get; set; }
    public string state_CODE { get; set; }
  
    public string err_Msg { get; set; }
    public string stamp_TIMES { get; set; }
    public string serial_NUMBER { get; set; }
}
 

public class ResponseUSM001001
{
    public string uid { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string imgurl { get; set; }
    public string address { get; set; }
    public ResponseUSM001001 Adapt(DZMembership membership)
    {
        this.uid = membership.Id.ToString();
        this.alias = membership.UserName;
        this.email = membership.Email;
        this.phone = membership.Phone;
        this.imgurl = "";
        this.address = "";
        return this;

    }
}