﻿using System;
using System.Collections.Generic;
using Ydb.Common.Application;
using Ydb.Common.Domain;
using Ydb.Common.Specification;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.DomainModel.Enums;

namespace Ydb.Membership.Application
{
    public interface IDZMembershipService
    {
        /// <summary>
        ///     商户注册
        /// </summary>
        /// <param name="registerName">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="confirmPassword"></param>
        /// <param name="hostInMail">验证邮件中链接的主机名称或者ip地址</param>
        /// <returns></returns>
        RegisterResult RegisterBusinessUser(string registerName, string password, string confirmPassword,
            string hostInMail);

        RegisterResult RegisterCustomerService(string registerName, string password, string confirmPassword,
            string hostInMail);

        RegisterResult RegisterMember(string registerName, string password, string confirmPassword, string userType,
            string hostInMail);

        RegisterResult RegisterMember(Guid Id, string registerName, string password, string confirmPassword,
            string userType,
            string hostInMail);

        RegisterResult RegisterStaff(string registerName, string password, string confirmPassword, string hostInMail);

        /// <summary>
        ///     重新发送注册验证邮件
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hostInEmail"></param>
        /// <returns></returns>
        ActionResult ResendVerifyEmail(string username, string hostInEmail);

        /// <summary>
        ///     验证注册代码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="verifyCode">注册验证码</param>
        /// <returns></returns>
        ActionResult VerifyRegisterCode(string verifyCode, string userid);

        /// <summary>
        ///     根据用户名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        MemberDto GetUserByName(string userName);

        /// <summary>
        ///     根据用户名id获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        MemberDto GetUserById(string id);

        IList<MemberDto> GetAllCustomer(int currentPageIndex, int pageSize, out long totalRecord);

        /// <summary>
        ///     用户验证
        /// </summary>
        /// <param name="userNameOrUserId">用户登录名或者ID</param>
        /// <param name="password"></param>
        /// <returns>验证结果,如果验证通过则包含memberdto</returns>
        ValidateResult ValidateUser(string userNameOrUserId, string password, bool isLogin);

        ValidateResult Login(string userNameOrUserId, string password);

        MemberDto Login3rd(string platForm, string code, string appName, string userType);

        /// <summary>
        ///     申请重置密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="hostInMail">验证邮件中链接的主机名称或者ip地址</param>
        /// <returns></returns>
        ActionResult ApplyRecovery(string userName, string hostInMail);

        ActionResult ChangePassword(string userName, string oldPassword, string newPassword);

        ActionResult RecoveryPasswordByPhone(string phone, string newPassword);

        ActionResult ChangeUserCity(Guid memberId, string cityCode, string longitude, string latitude, string areaId);

        ActionResult RecoveryPassword(string recoveryString, string newPassword);

        ActionResult ChangePhone(string userId, string newPhone);

        ActionResult ChangeEmail(string userId, string newEmail);

        ActionResult ChangeAlias(string userId, string neAlias);

        ActionResult ChangeAddress(string userId, string newAddress);

        ActionResult ChangeAvatar(string userId, string newAvatar);

        /// <summary>
        ///     更新用户所属区域
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        ActionResult UpdateArea(string userId, string areaId);

        IList<MemberDto> GetUsers(TraitFilter filter, string name, string email, string phone, string loginType,
            string userType);

        long GetUsersCount(string name, string email, string phone, string loginType, string userType);

        /// <summary>
        ///     昨日新增用户
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        long GetCountOfNewMembershipsYesterdayByArea(IList<string> areaList, UserType userType);

        /// <summary>
        ///     当前用户总量
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        long GetCountOfAllMembershipsByArea(IList<string> areaList, UserType userType);

        /// <summary>
        ///     上月用户在线活跃度（数量）
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        long GetCountOfLoginMembershipsLastMonthByArea(IList<string> areaList, UserType userType);

        /// <summary>
        /// 统计用户每日或每时新增数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsNewMembershipsCountListByTime(IList<string> areaList, DateTime beginTime,
            DateTime endTime, UserType userType);

        /// <summary>
        /// 统计用户每日或每时累计数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllMembershipsCountListByTime(IList<string> areaList, DateTime beginTime,
           DateTime endTime, UserType userType);

        /// <summary>
        /// 统计用户每日或每时在线活跃度（数量）列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsLoginCountListByTime(IList<string> areaList, DateTime beginTime,
            DateTime endTime, UserType userType);

        /// <summary>
        ///     根据用户手机系统统计用户数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllMembershipsCountListByAppName(IList<string> areaList, UserType userType);

        /// <summary>
        ///     根据用户性别统计用户数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllMembershipsCountListBySex(IList<string> areaList, UserType userType);

        /// <summary>
        ///     根据用户所在子区域统计用户数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        StatisticsInfo GetStatisticsAllMembershipsCountListByArea(IList<Area> areaList, UserType userType);

        /// <summary>
        ///     根据用户所在子区域统计用户列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        IList<MemberDto> GetMembershipsByArea(IList<string> areaList, UserType userType);

        ActionResult<MemberDto> GetAreaAgent(string city);

        /// <summary>
        ///     用户信息补全
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        ActionResult CompleteDZMembership(MemberDto membershipDto);

        /// <summary>
        ///     申请助理
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="realName"></param>
        /// <param name="personalID"></param>
        /// <param name="phone"></param>
        /// <param name="certificatesImageList"></param>
        /// <param name="diplomaImage"></param>
        /// <returns></returns>
        ActionResult ApplyDZMembershipCustomerService(string membershipId, string realName, string personalID,
            string phone, IList<DZMembershipImageDto> certificatesImageList, DZMembershipImageDto diplomaImage);

        /// <summary>
        ///     认证审核助理
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        ActionResult VerifyDZMembershipCustomerService(string membershipId, bool isVerified, string strMemo);

        /// <summary>
        /// 封停/解封
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        ActionResult LockDZMembership(string membershipId, bool isLocked, string strMemo);

        /// <summary>
        ///     根据用户名获取客服
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        DZMembershipCustomerServiceDto GetDZMembershipCustomerServiceByName(string userName);

        /// <summary>
        ///     根据用户Id获取客服
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        DZMembershipCustomerServiceDto GetDZMembershipCustomerServiceById(string id);

        /// <summary>
        ///     根据代理区域获取其助理的验证信息
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>>
            GetVerifiedDZMembershipCustomerServiceByArea(IList<Area> areaList);

        /// <summary>
        /// 根据代理区域获取其助理的信息列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        IDictionary<Enum_LockCustomerServiceType, IList<DZMembershipCustomerServiceDto>> GetLockDZMembershipCustomerServiceByArea(IList<Area> areaList);

        /// <summary>
        /// 根据代理区域获取其封锁的用户信息列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        IDictionary<Enum_LockMemberType, IList<MemberDto>> GetLockDZMembershipByArea(IList<Area> areaList, UserType userType);


        /// <summary>
        ///     根据代理区域获取一条为验证的客服信息
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        DZMembershipCustomerServiceDto GetOneNotVerifiedDZMembershipCustomerServiceByArea(IList<string> areaList);

        /// <summary>
        /// 根据代理区域获取客服信息
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        IList<DZMembershipCustomerServiceDto> GetDZMembershipCustomerServiceByArea(IList<string> areaIdList);

        IList<MemberDto> GetUsersByIdList(IList<string> memberIdList);
        IList<MemberDto> GetUsersByIdList(IList<string> memberIdList, IList<string> areaIdList);
    }
}