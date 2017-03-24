using System;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
/// <summary>
/// 用户验证统一处理
/// </summary>
public class Account
{
    IDZMembershipService memberService= Bootstrap.Container.Resolve<IDZMembershipService>();
    public Account()
    {  }
    public Account(IDZMembershipService memberService)
    {
        this.memberService = memberService;
    }

    public bool ValidateUser(Guid userId, string password, BaseResponse response, out MemberDto member)
    {

        return ValidateUser(userId.ToString(), password, response, out member) ;
    }
    public bool ValidateUser(string userName, string password, BaseResponse response, out MemberDto member)
    {
        
      ValidateResult result = memberService.ValidateUser(userName,password,false);
        member = result.ValidatedMember;
        return result.IsValidated;
       
    }
     


}
