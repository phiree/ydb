using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using log4net;
using Ydb.Common;
using Ydb.Common.Application;
using Ydb.Common.Domain;
using Ydb.Common.Enums;
using Ydb.Common.Infrastructure;
using Ydb.Common.Specification;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.DataStatistics;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.DomainModel.Service;
using Ydb.Membership.Infrastructure;

//using Ydb.Membership.Infrastructure.UnitOfWork;

namespace Ydb.Membership.Application
{
    public class DZMembershipService : IDZMembershipService
    {
        private readonly IDZMembershipDomainService dzmembershipDomainService;
        private readonly IEmailService emailService;
        private readonly IEncryptService encryptService;
        private readonly ILog log = LogManager.GetLogger(" Ydb.Membership.Application.DZMembershipService");
        private readonly ILogin3rd login3rdService;
        private readonly IRepositoryDZMembership repositoryMembership;

        private readonly IRepositoryMembershipLoginLog repositoryMembershipLoginLog;
        private readonly IRepositoryUserToken repositoryUserToken;
        private readonly IStatisticsMembershipCount statisticsMembershipCount;

        public DZMembershipService(IDZMembershipDomainService dzmembershipDomainService,
            IRepositoryDZMembership repositoryMembership,
            IEmailService emailService, IEncryptService encryptService,
            ILogin3rd login3rdService, IRepositoryUserToken repositoryUserToken,
            IRepositoryMembershipLoginLog repositoryMembershipLoginLog,
            IStatisticsMembershipCount statisticsMembershipCount)
        {
            this.dzmembershipDomainService = dzmembershipDomainService;
            // Bootstrap.Container.Resolve<IDZMembershipDomainService>();
            this.login3rdService = login3rdService; // Bootstrap.Container.Resolve<ILogin3rd>();
            this.emailService = emailService;
            this.repositoryMembership = repositoryMembership; // Bootstrap.Container.Resolve<IRepositoryDZMembership>();
            this.encryptService = encryptService;
            this.repositoryUserToken = repositoryUserToken;
            this.repositoryMembershipLoginLog = repositoryMembershipLoginLog;
            this.statisticsMembershipCount = statisticsMembershipCount;
        }

        

        [UnitOfWork]
        public RegisterResult RegisterCustomerService(string registerName, string password, string confirmPassword,
            string hostInMail)
        {
            //return RegisterMember(registerName, password, confirmPassword, UserType.customerservice.ToString(), hostInMail);

            var registerResult = new RegisterResult();
            if (password != confirmPassword)
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = "密码不匹配";

                return registerResult;
            }
            string errMsg;
            var createdUser = dzmembershipDomainService.CreateCustomerService(registerName, password, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = errMsg;
            }
            if (!string.IsNullOrEmpty(createdUser.Email))
                try
                {
                    emailService.SendEmail(createdUser.Email, "一点办注册验证邮件",
                        createdUser.BuildRegisterValidationContent(hostInMail)
                    );
                }
                catch (Exception ex)
                {
                    registerResult.SendEmailSuccess = false;
                    registerResult.SendEmailErrMsg = "邮件发送失败";

                    log.Warn("注册邮件发送失败.注册用户" + createdUser.Id
                             + Environment.NewLine + ex);
                }
            var registeredUser = Mapper.Map<MemberDto>(createdUser);
            registerResult.ResultObject = registeredUser;
            return registerResult;
        }

        [UnitOfWork]
        public RegisterResult RegisterBusinessUser(string registerName, string password, string confirmPassword,
            string hostInMail)
        {
            return RegisterMember(registerName, password, confirmPassword, UserType.business.ToString(), hostInMail);
        }

        [UnitOfWork]
        public ActionResult ResendVerifyEmail(string username, string hostInEmail)
        {
            var result = new ActionResult();
            var member = repositoryMembership.GetMemberByName(username);
            try
            {
                emailService.SendEmail(member.Email, "一点办注册验证邮件",
                    member.BuildRegisterValidationContent(hostInEmail)
                );
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMsg = "邮件发送失败";

                log.Warn("注册邮件发送失败.注册用户" + member.Id
                         + Environment.NewLine + ex);
            }
            return result;
        }

        [UnitOfWork]
        public ActionResult VerifyRegisterCode(string verifyCode, string userid)
        {
            var result = new ActionResult();
            var member = repositoryMembership.GetMemberById(new Guid(userid));
            if (member == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "没有该用户";
                return result;
            }
            if (member.IsRegisterValidated)
            {
                result.IsSuccess = false;
                result.ErrMsg = "您已经通过了邮箱验证,无须再次验证.";
                return result;
            }
            if (member.RegisterValidateCode != verifyCode)
            {
                result.IsSuccess = false;
                result.ErrMsg = "注册验证码有误";
                return result;
            }
            member.IsRegisterValidated = true;
            return result;
        }

        [UnitOfWork]
        public MemberDto GetUserByName(string userName)
        {
            var membership = dzmembershipDomainService.GetUserByName(userName);
            if (membership == null) return null;

            var memberDto = Mapper.Map<DZMembership, MemberDto>(membership);
            //new Dto.MemberDto { Id = membership.Id, UserName = membership.UserName };
            return memberDto;
        }

        [UnitOfWork]
        public MemberDto GetUserById(string id)
        {
            var membership = repositoryMembership.GetMemberById(new Guid(id));
            if (membership == null) return null;

            var memberDto = Mapper.Map<DZMembership, MemberDto>(membership);
            //new Dto.MemberDto { Id = membership.Id, UserName = membership.UserName };
            return memberDto;
        }

        [UnitOfWork]
        public ValidateResult ValidateUser(string username, string password, bool isLogin)
        {
            var validateResult = new ValidateResult();
            string errMsg;

            var member = dzmembershipDomainService.ValidateUser(username, password, isLogin, out errMsg);
            if (member == null)
            {
                validateResult.IsValidated = false;
                validateResult.ValidateErrMsg = errMsg;
            }
            else
            {
                validateResult.ValidatedMember = Mapper.Map<DZMembership, MemberDto>(member);
            }
            return validateResult;
        }

        [UnitOfWork]
        public ValidateResult Login(string userNameOrUserId, string password)
        {
            return ValidateUser(userNameOrUserId, password, true);
        }

        /// <summary>
        ///     申请密码恢复
        /// </summary>
        /// <param name="userName"></param>
        [UnitOfWork]
        public ActionResult ApplyRecovery(string userName, string hostInMail)
        {
            var result = new ActionResult();
            var member = dzmembershipDomainService.GetUserByName(userName);
            if (member == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "不存在此用户";
            }
            else
            {
                try
                {
                    var body = member.BuildRecoveryContent(hostInMail);
                    var tilte = "一点办-密码重置";
                    emailService.SendEmail(member.Email, tilte, body);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrMsg = "发送邮件失败";
                }
            }

            return result;
        }

        [UnitOfWork]
        public ActionResult RecoveryPassword(string recoveryString, string newPassword)
        {
            var recoveryParameters = recoveryString.Split(new[] {Config.pwssword_recovery_spliter},
                StringSplitOptions.None);
            var userName = encryptService.Decrypt(recoveryParameters[0], false);
            var recoveryCode = recoveryParameters[1];
            var member = repositoryMembership.GetMemberByName(userName);
            repositoryUserToken.DeleteToken(member.Id.ToString());
            return member.RecoveryPassword(recoveryCode, newPassword, encryptService.GetMD5Hash(newPassword));
        }

        [UnitOfWork]
        public ActionResult RecoveryPasswordByPhone(string phone, string newPassword)
        {
            var result = new ActionResult();
            var member = repositoryMembership.GetMemberByName(phone);
            if (member == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "该手机用户不存在!";
                return result;
            }
            repositoryUserToken.DeleteToken(member.Id.ToString());
            return member.ChangePasswordByPhone(newPassword, encryptService.GetMD5Hash(newPassword));
        }

        [UnitOfWork]
        public ActionResult ChangeUserCity(Guid memberId, string cityCode, string longitude, string latitude,
            string areaId)
        {
            var result = new ActionResult();
            var member = repositoryMembership.GetMemberById(memberId);
            if (member == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "该用户不存在!";
            }
            if (!string.IsNullOrEmpty(cityCode))
                member.UserCity = cityCode;
            if (string.IsNullOrEmpty(longitude) ^ string.IsNullOrEmpty(latitude))
            {
                result.IsSuccess = false;
                result.ErrMsg = "经纬度不能只传一个!";
            }
            if (!string.IsNullOrEmpty(longitude))
                member.Longitude = longitude;
            if (!string.IsNullOrEmpty(latitude))
                member.Latitude = latitude;
            if (!string.IsNullOrEmpty(areaId))
                member.AreaId = areaId;
            repositoryMembership.Update(member);
            return result;
        }

        [UnitOfWork]
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var member = repositoryMembership.GetMemberByName(userName);
            var oldEncryptedPassword = encryptService.GetMD5Hash(oldPassword);
            var newEncryptedPassword = encryptService.GetMD5Hash(newPassword);
            repositoryUserToken.DeleteToken(member.Id.ToString());
            return member.ChangePassword(oldEncryptedPassword, newPassword, newEncryptedPassword);
        }

        [UnitOfWork]
        public ActionResult ChangePhone(string userId, string newPhone)
        {
            var result = new ActionResult();
            var member = repositoryMembership.GetMemberById(new Guid(userId));
            if (member == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "用户不存在";
                return result;
            }
            if (member.Phone == newPhone)
            {
                result.IsSuccess = false;
                result.ErrMsg = "新号码和旧号码一样,无需修改";
                return result;
            }
            member.Phone = newPhone;
            return result;
        }

        [UnitOfWork]
        public ActionResult ChangeEmail(string userId, string newEmail)
        {
            var result = new ActionResult();
            var member = repositoryMembership.GetMemberById(new Guid(userId));
            if (member == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "用户不存在";
                return result;
            }
            if (member.Email == newEmail)
            {
                result.IsSuccess = false;
                result.ErrMsg = "新邮箱和旧邮箱一样,无需修改";
                return result;
            }
            member.Email = newEmail;
            member.IsRegisterValidated = false;
            member.RegisterValidateCode = Guid.NewGuid().ToString();
            return result;
        }

        [Obsolete("尽快移除,用语义明确的方法代替")]
        [UnitOfWork]
        public IList<MemberDto> GetUsers(TraitFilter filter, string name, string email, string phone, string loginType,
            string userType)
        {
            //(UserType)Enum.Parse(typeof(UserType), userType)
            var lType = LoginType.None;
            Enum.TryParse(loginType, out lType);
            var memberList = repositoryMembership.GetUsers(filter, name, email, phone, lType,
                (UserType) Enum.Parse(typeof(UserType), userType));

            return Mapper.Map<IList<DZMembership>, IList<MemberDto>>(memberList);
        }

        [Obsolete("尽快移除")]
        [UnitOfWork]
        public long GetUsersCount(string name, string email, string phone, string loginType, string userType)
        {
            var lType = LoginType.None;
            Enum.TryParse(loginType, out lType);
            return repositoryMembership.GetUsersCount(name, email, phone, lType,
                (UserType) Enum.Parse(typeof(UserType), userType));
        }

        [UnitOfWork]
        public MemberDto Login3rd(string platform, string code, string appName, string userType)
        {
            var membership = login3rdService.Login(platform, code, appName, userType);

            return Mapper.Map<MemberDto>(membership);
        }

        [UnitOfWork]
        public ActionResult ChangeAlias(string userId, string neAlias)
        {
            var member = repositoryMembership.GetMemberById(new Guid(userId));

            member.NickName = neAlias;

            return new ActionResult();
        }

        [UnitOfWork]
        public ActionResult ChangeAddress(string userId, string newAddress)
        {
            var member = repositoryMembership.GetMemberById(new Guid(userId));

            member.Address = newAddress;

            return new ActionResult();
        }

        [UnitOfWork]
        public ActionResult ChangeAvatar(string userId, string newAvatar)
        {
            var member = repositoryMembership.GetMemberById(new Guid(userId));

            member.AvatarUrl = newAvatar;

            return new ActionResult();
        }

        [UnitOfWork]
        public RegisterResult RegisterStaff(string registerName, string password, string confirmPassword,
            string hostInMail)
        {
            return RegisterMember(registerName, password, confirmPassword, UserType.staff.ToString(), hostInMail);
        }

        [UnitOfWork]
        public IList<MemberDto> GetAllCustomer(int currentPageIndex, int pageSize, out long totalRecord)
        {
            var filter = new TraitFilter {pageNum = currentPageIndex, pageSize = pageSize};
            var memberList = repositoryMembership.GetUsers(filter, string.Empty, string.Empty, string.Empty,
                LoginType.None, UserType.customer);
            totalRecord = repositoryMembership.GetUsersCount(string.Empty, string.Empty, string.Empty, LoginType.None,
                UserType.customer);
            return Mapper.Map<IList<DZMembership>, IList<MemberDto>>(memberList);
        }

        /// <summary>
        ///     昨日新增用户
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public long GetCountOfNewMembershipsYesterdayByArea(IList<string> areaList, UserType userType)
        {
            var beginTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            return repositoryMembership.GetUsersCountByArea(areaList, beginTime, beginTime.AddDays(1), userType);
        }

        /// <summary>
        ///     当前用户总量
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public long GetCountOfAllMembershipsByArea(IList<string> areaList, UserType userType)
        {
            return repositoryMembership.GetUsersCountByArea(areaList, DateTime.MinValue, DateTime.MinValue, userType);
        }

        /// <summary>
        ///     上月用户在线活跃度（数量）
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public long GetCountOfLoginMembershipsLastMonthByArea(IList<string> areaList, UserType userType)
        {
            var memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue,
                userType);
            var baseTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            var loginList = repositoryMembershipLoginLog.GetMembershipLoginLogListByTime(baseTime.AddMonths(-1),
                baseTime);
            return statisticsMembershipCount.StatisticsLoginCountLastMonth(memberList, loginList);
        }

        /// <summary>
        /// 统计用户每日或每时新增数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsNewMembershipsCountListByTime(IList<string> areaList, DateTime beginTime,
            DateTime endTime, UserType userType)
        {
            var memberList = repositoryMembership.GetUsersByArea(areaList, beginTime, endTime, userType);
            var statisticsInfo = statisticsMembershipCount.StatisticsNewMembershipsCountListByTime(memberList, beginTime,
                endTime, beginTime.ToString("yyyyMMdd") == endTime.AddDays(-1).ToString("yyyyMMdd"));
            return statisticsInfo;
        }

        /// <summary>
        /// 统计用户每日或每时累计数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllMembershipsCountListByTime(IList<string> areaList, DateTime beginTime,
            DateTime endTime, UserType userType)
        {
            var memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue,
                userType);
            var statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListByTime(memberList, beginTime,
                endTime, beginTime.ToString("yyyyMMdd") == endTime.AddDays(-1).ToString("yyyyMMdd"));
            return statisticsInfo;
        }

        /// <summary>
        /// 统计用户每日或每时在线活跃度（数量）列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsLoginCountListByTime(IList<string> areaList, DateTime beginTime,
            DateTime endTime, UserType userType)
        {
            var memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue,
                userType);
            var loginList = repositoryMembershipLoginLog.GetMembershipLoginLogListByTime(beginTime, endTime);
            var statisticsInfo = statisticsMembershipCount.StatisticsLoginCountListByTime(memberList, loginList,
                beginTime, endTime, beginTime.ToString("yyyyMMdd") == endTime.AddDays(-1).ToString("yyyyMMdd"));
            return statisticsInfo;
        }

        /// <summary>
        ///     根据用户手机系统统计用户数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllMembershipsCountListByAppName(IList<string> areaList, UserType userType)
        {
            var memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue,
                userType);
            var loginList = repositoryMembershipLoginLog.GetMembershipLastLoginLog();
            var statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListByAppName(memberList,
                loginList);
            return statisticsInfo;
        }

        /// <summary>
        ///     根据用户性别统计用户数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllMembershipsCountListBySex(IList<string> areaList, UserType userType)
        {
            var memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue,
                userType);
            var statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListBySex(memberList);
            return statisticsInfo;
        }

        /// <summary>
        ///     根据用户所在子区域统计用户数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllMembershipsCountListByArea(IList<Area> areaList, UserType userType)
        {
            IList<string> AreaIdList = areaList.Select(x => x.Id.ToString()).ToList();
            var memberList = repositoryMembership.GetUsersByArea(AreaIdList, DateTime.MinValue, DateTime.MinValue,
                userType);
            var statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountGroupByArea(memberList, areaList);
            return statisticsInfo;
        }

        /// <summary>
        ///     根据用户所在子区域统计用户列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public IList<MemberDto> GetMembershipsByArea(IList<string> areaList, UserType userType)
        {
            var memberDtoList =
                Mapper.Map<IList<MemberDto>>(repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue,
                    DateTime.MinValue, userType));
            return memberDtoList;
        }

        public ActionResult<MemberDto> GetAreaAgent(string city)
        {
            var result = new ActionResult<MemberDto>();
            try
            {
                var agentForCity = repositoryMembership.FindOne(x => x.UserCity == city);
                result.ResultObject = Mapper.Map<MemberDto>(agentForCity);
            }
            catch
            {
                result.IsSuccess = false;
                result.ErrMsg = "错误.该城市出现多个代理:" + city;
            }
            return result;
        }

        [UnitOfWork]
        public ActionResult UpdateArea(string userId, string areaId)
        {
            var member = repositoryMembership.GetMemberById(new Guid(userId));

            member.AreaId = areaId;

            return new ActionResult();
        }

        /// <summary>
        ///     用户信息补全
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        [UnitOfWork]
        public ActionResult CompleteDZMembership(MemberDto membershipDto)
        {
            var result = new ActionResult();
            try
            {
                var dzMembership = Mapper.Map<DZMembership>(membershipDto);
                dzmembershipDomainService.UpdateDZMembership(dzMembership);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMsg = "补全用户信息出错:" + ex.Message;
            }
            return result;
        }

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
        [UnitOfWork]
        public ActionResult ApplyDZMembershipCustomerService(string membershipId, string realName, string personalID,
            string phone, IList<DZMembershipImageDto> certificatesImageList, DZMembershipImageDto diplomaImage)
        {
            var result = new ActionResult();
            try
            {
                if (string.IsNullOrEmpty(realName))
                    throw new Exception("真实姓名不能为空");
                if (string.IsNullOrEmpty(personalID))
                    throw new Exception("身份证件不能为空");
                if (string.IsNullOrEmpty(phone))
                    throw new Exception("手机不能为空");
                if (diplomaImage == null)
                    throw new Exception("学历证明不能为空");
                if (certificatesImageList == null || certificatesImageList.Count == 0)
                    throw new Exception("证件照片不能为空");
                var dzMembership = dzmembershipDomainService.GetDZMembershipCustomerServiceById(membershipId);
                dzMembership.RealName = realName;
                dzMembership.PersonalID = personalID;
                dzMembership.Phone = phone;
                dzMembership.DZMembershipCertificates = Mapper.Map<IList<DZMembershipImage>>(certificatesImageList);
                dzMembership.DZMembershipDiploma = Mapper.Map<DZMembershipImage>(diplomaImage);
                dzmembershipDomainService.UpdateDZMembership(dzMembership);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMsg = "助理申请出错:" + ex.Message;
            }
            return result;
        }

        /// <summary>
        ///     认证审核助理
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        [UnitOfWork]
        public ActionResult VerifyDZMembershipCustomerService(string membershipId, bool isVerified, string strMemo)
        {
            var result = new ActionResult();
            try
            {
                var dzMembership = dzmembershipDomainService.GetDZMembershipCustomerServiceById(membershipId);
                dzMembership.Verification(isVerified, strMemo);
                //dzmembershipDomainService.UpdateDZMembership(dzMembership);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMsg = "出错:" + ex.Message;
            }
            return result;
        }

        /// <summary>
        ///     封停/解封助理
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        [UnitOfWork]
        public ActionResult LockDZMembershipCustomerService(string membershipId, bool isLocked, string strMemo)
        {
            var result = new ActionResult();
            try
            {
                var dzMembership = dzmembershipDomainService.GetDZMembershipCustomerServiceById(membershipId);
                dzMembership.LockCustomerService(isLocked, strMemo);
                //dzmembershipDomainService.UpdateDZMembership(dzMembership);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMsg = "出错:" + ex.Message;
            }
            return result;
        }

        /// <summary>
        ///     根据用户名获取客服
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [UnitOfWork]
        public DZMembershipCustomerServiceDto GetDZMembershipCustomerServiceByName(string userName)
        {
            var membership = dzmembershipDomainService.GetDZMembershipCustomerServiceByName(userName);

            var memberDto = Mapper.Map<DZMembershipCustomerService, DZMembershipCustomerServiceDto>(membership);
            return memberDto;
        }

        /// <summary>
        ///     根据用户Id获取客服
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [UnitOfWork]
        public DZMembershipCustomerServiceDto GetDZMembershipCustomerServiceById(string id)
        {
            var membership = dzmembershipDomainService.GetDZMembershipCustomerServiceById(id);

            var memberDto = Mapper.Map<DZMembershipCustomerService, DZMembershipCustomerServiceDto>(membership);
            return memberDto;
        }

        /// <summary>
        /// 根据代理区域获取其助理的验证信息获取客服
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>>
            GetVerifiedDZMembershipCustomerServiceByArea(IList<Area> areaList)
        {
            IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto =
                new Dictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>>();
            IList<string> AreaIdList = areaList.Select(x => x.Id.ToString()).ToList();
            var memberList = repositoryMembership.GetUsersByArea(AreaIdList, DateTime.MinValue, DateTime.MinValue,
                UserType.customerservice);
            var dic = statisticsMembershipCount.StatisticsVerifiedCustomerServiceByArea(memberList, areaList,
                EnumberHelper.EnumNameToList<Enum_ValiedateCustomerServiceType>());
            foreach (var pair in dic)
                dicDto.Add(
                    (Enum_ValiedateCustomerServiceType) Enum.Parse(typeof(Enum_ValiedateCustomerServiceType), pair.Key),
                    Mapper.Map<IList<DZMembershipCustomerServiceDto>>(pair.Value));
            return dicDto;
        }

        /// <summary>
        /// 根据代理区域获取其助理的信息列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        [UnitOfWork]
        public IDictionary<Enum_LockCustomerServiceType, IList<DZMembershipCustomerServiceDto>> GetLockDZMembershipCustomerServiceByArea(IList<Area> areaList)
        {
            IDictionary<Enum_LockCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto = new Dictionary<Enum_LockCustomerServiceType, IList<DZMembershipCustomerServiceDto>>();
            IList<string> AreaIdList = areaList.Select(x => x.Id.ToString()).ToList();
            IList<DZMembership> memberList = repositoryMembership.GetUsersByArea(AreaIdList, DateTime.MinValue, DateTime.MinValue, UserType.customerservice);
            IDictionary<string, IList<DZMembershipCustomerService>> dic = statisticsMembershipCount.StatisticsVerifiedCustomerServiceByArea(memberList, areaList, EnumberHelper.EnumNameToList<Enum_LockCustomerServiceType>());
            foreach (KeyValuePair<string, IList<DZMembershipCustomerService>> pair in dic)
            {
                dicDto.Add((Enum_LockCustomerServiceType)Enum.Parse(typeof(Enum_LockCustomerServiceType), pair.Key), Mapper.Map<IList<DZMembershipCustomerServiceDto>>(pair.Value));
            }
            return dicDto;
        }

        /// <summary>
        /// 根据代理区域获取一条为验证的客服信息
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        public DZMembershipCustomerServiceDto GetOneNotVerifiedDZMembershipCustomerServiceByArea(IList<string> areaList)
        {
            return
                Mapper.Map<DZMembershipCustomerServiceDto>(
                    repositoryMembership.GetOneNotVerifiedDZMembershipCustomerServiceByArea(areaList));
        }
        public RegisterResult RegisterMember(string registerName, string password, string confirmPassword, string strUserType, string hostInMail)
        {
            return RegisterMember(Guid.NewGuid(), registerName, password, confirmPassword, strUserType, hostInMail);
        }
        [UnitOfWork]
        public RegisterResult RegisterMember(Guid Id, string registerName, string password, string confirmPassword,

    string strUserType, string hostInMail)
        {
            var userType = (UserType) Enum.Parse(typeof(UserType), strUserType);

            var registerResult = new RegisterResult();
            if (password != confirmPassword)
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = "密码不匹配";

                return registerResult;
            }
            string errMsg;
            DZMembership createdUser = dzmembershipDomainService.CreateUser(Id, registerName, password, userType, out errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = errMsg;
            }
            if (!string.IsNullOrEmpty(createdUser.Email))
                try
                {
                    emailService.SendEmail(createdUser.Email, "一点办注册验证邮件",
                        createdUser.BuildRegisterValidationContent(hostInMail)
                    );
                }
                catch (Exception ex)
                {
                    registerResult.SendEmailSuccess = false;
                    registerResult.SendEmailErrMsg = "邮件发送失败";

                    log.Warn("注册邮件发送失败.注册用户" + createdUser.Id
                             + Environment.NewLine + ex);
                }
            var registeredUser = Mapper.Map<MemberDto>(createdUser);
            registerResult.ResultObject = registeredUser;
            return registerResult;
        }

        [UnitOfWork]
        public void RegisterWeChat(MemberWeChatDto wechatDto)
        {
            var wechatUser = new DZMembershipWeChat();
            repositoryMembership.Add(wechatUser);
        }

        public IList<DZMembershipCustomerServiceDto> GetDZMembershipCustomerServiceByArea(IList<string> areaIdList)
        {
            return Mapper.Map<IList<DZMembershipCustomerServiceDto>>(dzmembershipDomainService.GetDZMembershipCustomerServiceByArea(areaIdList));
        }
    }
}