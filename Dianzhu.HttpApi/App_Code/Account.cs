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
/// 用户验证统一处理
/// </summary>
public class Account
{
    DZMembershipProvider bllMember = new DZMembershipProvider();
    public Account()
    { bllMember = new DZMembershipProvider(); }
    public Account(DZMembershipProvider bllMember)
    {
        this.bllMember = bllMember;
    }

    public bool ValidateUser(Guid userId, string password, BaseResponse response, out DZMembership member)
    {

        member = bllMember.GetUserById( userId);
        return ValidateUser(member, password, response);
    }
    public bool ValidateUser(string userName, string password, BaseResponse response, out DZMembership member)
    {

        member = bllMember.GetUserByName(userName);
        return ValidateUser(member, password, response);
       
    }
    private bool ValidateUser(DZMembership member,string password, BaseResponse response)
    {
       
        if (member == null)
        {
            response.state_CODE = Dicts.StateCode[8];
            response.err_Msg = "用户不存在,可能是传入的uid有误";
            return false;
        }
        //验证用户的密码
        if (member.Password != FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"))
        {
            response.state_CODE = Dicts.StateCode[9];
            response.err_Msg = "用户密码错误";
            return false;
        }
        return true;
    }


}
