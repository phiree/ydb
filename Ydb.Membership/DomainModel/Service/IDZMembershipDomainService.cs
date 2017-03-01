
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ydb.Common.Domain;
using Ydb.Common.Specification;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Membership.DomainModel.Repository;
namespace Ydb.Membership.DomainModel
{

    public interface IDZMembershipDomainService
    {

        bool ChangePassword(string username, string oldPassword, string newPassword, out string errMsg);

        DZMembership GetUser(string username, bool userIsOnline);

        DZMembership ValidateUser(string username, string password, bool isLogin, out string errMsg);

        DZMembership CreateUser(string loginName, string password, UserType userType, out string errMsg);

        void CreateUserForU3rd(DZMembership member);

        void UpdateUserForU3rd(DZMembership member);

        DZMembership GetUserByWechatOpenId(string openid);

        DZMembership GetUserByQQOpenId(string openid);

        DZMembership GetUserBySinaWeiboUId(long uid);

        DZMembership GetUserByName(string name);

        DZMembership GetUserById(Guid id);

        IList<DZMembership> GetAllDZMembers(int pageIndex, int pageSize, out long totalRecords);

        void UpdateDZMembership(DZMembership member);

        DZMembership GetUserByEmail(string email);

        DZMembership GetUserByPhone(string phone);

        /// <summary>
        /// 根据用户信息获取user
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="platform"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        IList<DZMembership> GetUsers(TraitFilter filter, string name, string email, string phone, string platform, string userType) ;

        /// <summary>
        /// 统计用户数量
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="platform">用户的登录平台(微信,qq,微博,原生)</param>
        /// <param name="userType">用户类型:商户,助理,客户....</param>
        /// <returns></returns>
        long GetUsersCount(string name, string email, string phone, string platform, string userType);

        IList<DZMembership> GetAllCustomer(int pageIndex, int pageSize, out long totalRecords);

        DZMembership CreateCustomerService(string loginName, string password, out string errMsg);

        DZMembershipCustomerService GetDZMembershipCustomerServiceByName(string userName);

        DZMembershipCustomerService GetDZMembershipCustomerServiceById(string id);

        IList<DZMembershipCustomerService> GetDZMembershipCustomerServiceByArea(IList<string> areaIdList);
    }
}
