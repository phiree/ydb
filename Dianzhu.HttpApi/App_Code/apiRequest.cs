using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class apiRequestChooser { 

}
/// <summary>
///apiRequest 的摘要说明
/// </summary>
public class apiRequest
{
    public string protocol_CODE { get; set; }
    public string stamp_TIMES { get; set; }
    public string serial_NUMBER { get; set; }
    public string appToken { get; set; }
    public string appName { get; set; }
    public string Ver { get; set; }
    public JObject ReqData { get; set; }
}


public class apiResponse
{

    public string protocol_CODE { get; set; }
    public string state_CODE { get; set; }
    public string RespData { get; set; }
    public string err_Msg { get; set; }
    public string stamp_TIMES { get; set; }
    public string serial_NUMBER { get; set; }
    
    public apiResponse(apiRequest request)
    {  
        
        this.protocol_CODE = request.protocol_CODE;
        this.serial_NUMBER = request.serial_NUMBER;
        BuildResponse(request);
        this.stamp_TIMES = GetStampTimes();
    }
    //不利于单元测试.
    public void BuildResponse(apiRequest request)
    {
        
        switch (request.protocol_CODE.ToLower())
        {
            case "usm001001":
                RequestUSM001001 requestData =  request.ReqData.ToObject<RequestUSM001001>();
                DZMembershipProvider p = new DZMembershipProvider();
                string userName = requestData.userPhone ?? requestData.userEmail;
                bool result = p.ValidateUser(userName, requestData.userPWord);
                if (!result)
                {
                    this.state_CODE = Dicts.ErrCode[9];
                    this.err_Msg = "用户名或者密码有误";
                    return;
                }
                this.state_CODE = Dicts.ErrCode[0];
                DZMembership member = p.GetUser(userName);
                ResponseUSM001001 resp = new ResponseUSM001001().Adapt(member);
                this.RespData = JsonConvert.SerializeObject(resp);
                break;
            case "usm001002": break;
        }
    }

    private string GetStampTimes()
    { return (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString(); }
}

public class RequestUSM001001
{
    public string userEmail { get; set; }
    public string userPhone { get; set; }
    public string userPWord { get; set; }

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
        this.uid = membership.Id.ToString().Replace("-", string.Empty).ToUpper();
        this.alias = membership.UserName;
        this.email = membership.Email;
        this.phone = membership.Phone;
        this.imgurl = "";
        this.address = "";
        return this;

    }
}

public static class Dicts
{
    public readonly static string[] ProtocolCode = {
                                                   "USM001001",
                                                   "USM001002",
                                                   "USM001003",
                                                   "SVM001001",
                                                   "SVM001002",
                                                   "SVM001003",
                                                   "SVM002001",
                                                   "VCM001001",
                                                   "VCM001002",
                                                   "VCM001003"

                                                   };
    public readonly static string[] ErrCode = { 
                                            "009000",//正常 
                                            "009001",//未知数据类型
                                            "009002",//数据库访问错误
                                            "009003",//违反数据唯一性约束
                                            "009004",// 5 数据库返回值 数量错误

                                            "009005",//数据资源忙
                                            "009006",//数据超出范围
                                            "009007",//8 提交过于频繁

                                            "001001",//用户认证错误
                                            "001002",//用户密码错误
                                            "001003",//密码错误次数超过限定被锁定
                                            "001004",//12 外部系统IP被拒绝
                                            };
}
