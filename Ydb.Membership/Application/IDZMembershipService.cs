using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
namespace Ydb.Membership.Application
{
  public interface  IDZMembershipService
    {
        /// <summary>
        /// 商户注册
        /// </summary>
        /// <param name="registerName">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        Dto.RegisterResult RegisterBusinessUser(string registerName, string password,string confirmPassword);
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Dto.MemberDto GetUserByName(string userName);

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="userNameOrUserId">用户登录名或者ID</param>
        /// <param name="password"></param>
        /// <returns>验证结果,如果验证通过则包含memberdto</returns>
        Dto.ValidateResult  ValidateUser(string userNameOrUserId, string password,bool isLogin);
        Dto.ValidateResult Login(string userNameOrUserId, string password);


    }
}
