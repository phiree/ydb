using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.User
{
    public interface IUserService
    {
        /// <summary>
        /// 验证用户名密码验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ValidateUser(string username, string password);

        /// <summary>
        /// 根据userID获取user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        customerObj GetUserById(string userID, string userType);

        /// <summary>
        /// 根据用户信息获取user
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="userFilter"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        IList<customerObj> GetUsers(common_Trait_Filtering filter, common_Trait_UserFiltering userFilter, string userType);

        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="userBody"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        object PostUser(Common_Body userBody, string userType);

        /// <summary>
        /// 第三方用户注册
        /// </summary>
        /// <param name="u3rd_Model"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        object PostUser3rds(U3RD_Model u3rd_Model, string userType);

        /// <summary>
        /// 统计用户数量
        /// </summary>
        /// <param name="userFilter"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        countObj GetUsersCount(common_Trait_UserFiltering userFilter, string userType);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userChangeBody"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        object PatchUser(string userID, UserChangeBody userChangeBody, string userType);

        /// <summary>
        ///  读取客服信息(申请客服资源)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        applyCustomerServicesObj GetCustomerServices(Customer customer);


    }
}
