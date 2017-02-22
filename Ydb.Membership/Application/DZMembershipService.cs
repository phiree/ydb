using System;
using System.Linq;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Membership.DomainModel.Repository;
//using Ydb.Membership.Infrastructure.UnitOfWork;
using AutoMapper;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.Infrastructure;
using Ydb.Common.Application;
using Ydb.Common.Infrastructure;
using System.Collections.Generic;
using Ydb.Common.Specification;
using Ydb.Common.Repository;
using Ydb.Membership.DomainModel.Service;
using Ydb.Membership.DomainModel.DataStatistics;
using Ydb.Common.Domain;

namespace Ydb.Membership.Application
{
    public class DZMembershipService : IDZMembershipService
    {
        log4net.ILog log = log4net.LogManager.GetLogger(" Ydb.Membership.Application.DZMembershipService");
        IDZMembershipDomainService dzmembershipDomainService;
        IEmailService emailService;
        IRepositoryDZMembership repositoryMembership;
        IRepositoryUserToken repositoryUserToken;
        ILogin3rd login3rdService;
        IEncryptService encryptService;

        IRepositoryMembershipLoginLog repositoryMembershipLoginLog;
        IStatisticsMembershipCount statisticsMembershipCount;

        public DZMembershipService(IDZMembershipDomainService dzmembershipDomainService,
            IRepositoryDZMembership repositoryMembership,
            IEmailService emailService, IEncryptService encryptService,
            ILogin3rd login3rdService, IRepositoryUserToken repositoryUserToken,
            IRepositoryMembershipLoginLog repositoryMembershipLoginLog,
            IStatisticsMembershipCount statisticsMembershipCount)
        {
            this.dzmembershipDomainService = dzmembershipDomainService;// Bootstrap.Container.Resolve<IDZMembershipDomainService>();
            this.login3rdService = login3rdService;// Bootstrap.Container.Resolve<ILogin3rd>();
            this.emailService = emailService;
            this.repositoryMembership = repositoryMembership;// Bootstrap.Container.Resolve<IRepositoryDZMembership>();
            this.encryptService = encryptService;
            this.repositoryUserToken = repositoryUserToken;
            this.repositoryMembershipLoginLog = repositoryMembershipLoginLog;
            this.statisticsMembershipCount = statisticsMembershipCount;

        }




        public Dto.RegisterResult RegisterMember(string registerName, string password, string confirmPassword, string strUserType, string hostInMail)
        {
            UserType userType = (UserType)Enum.Parse(typeof(UserType), strUserType);

            Dto.RegisterResult registerResult = new Dto.RegisterResult();
            if (password != confirmPassword)
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = "密码不匹配";

                return registerResult;
            }
            string errMsg;
            DZMembership createdUser = dzmembershipDomainService.CreateUser(registerName, password, userType, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = errMsg;
            }
            if (!string.IsNullOrEmpty(createdUser.Email))
            {
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
                        + Environment.NewLine + ex.ToString());

                }
            }
            MemberDto registeredUser = Mapper.Map<MemberDto>(createdUser);
            registerResult.ResultObject = registeredUser;
            return registerResult;

        }

        [UnitOfWork]
        public Dto.RegisterResult RegisterCustomerService(string registerName, string password, string confirmPassword, string hostInMail)
        {
            //return RegisterMember(registerName, password, confirmPassword, UserType.customerservice.ToString(), hostInMail);

            Dto.RegisterResult registerResult = new Dto.RegisterResult();
            if (password != confirmPassword)
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = "密码不匹配";

                return registerResult;
            }
            string errMsg;
            DZMembership createdUser = dzmembershipDomainService.CreateCustomerService(registerName, password, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = errMsg;
            }
            if (!string.IsNullOrEmpty(createdUser.Email))
            {
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
                        + Environment.NewLine + ex.ToString());

                }
            }
            MemberDto registeredUser = Mapper.Map<MemberDto>(createdUser);
            registerResult.ResultObject = registeredUser;
            return registerResult;
        }


        [UnitOfWork]
        public Dto.RegisterResult RegisterBusinessUser(string registerName, string password, string confirmPassword, string hostInMail)
        {
            return RegisterMember(registerName, password, confirmPassword, UserType.business.ToString(), hostInMail);

        }

        [UnitOfWork]
        public void RegisterWeChat(MemberWeChatDto wechatDto)
        {
            DZMembershipWeChat wechatUser = new DZMembershipWeChat();
            repositoryMembership.Add(wechatUser);
        }




        [UnitOfWork]
        public ActionResult ResendVerifyEmail(string username, string hostInEmail)
        {
            ActionResult result = new ActionResult();
            DZMembership member = repositoryMembership.GetMemberByName(username);
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
                    + Environment.NewLine + ex.ToString());

            }
            return result;
        }

        [UnitOfWork]
        public ActionResult VerifyRegisterCode(string verifyCode, string userid)
        {

            ActionResult result = new ActionResult();
            DZMembership member = repositoryMembership.GetMemberById(new Guid(userid));
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
        public Dto.MemberDto GetUserByName(string userName)
        {
            DZMembership membership = dzmembershipDomainService.GetUserByName(userName);
            if (membership == null) { return null; }

            Dto.MemberDto memberDto = Mapper.Map<DZMembership, Dto.MemberDto>(membership);//new Dto.MemberDto { Id = membership.Id, UserName = membership.UserName };
            return memberDto;
        }

        [UnitOfWork]
        public Dto.MemberDto GetUserById(string id)
        {
            DZMembership membership = repositoryMembership.GetMemberById(new Guid(id));
            if (membership == null) { return null; }

            Dto.MemberDto memberDto = Mapper.Map<DZMembership, Dto.MemberDto>(membership);//new Dto.MemberDto { Id = membership.Id, UserName = membership.UserName };
            return memberDto;
        }

        [UnitOfWork]
        public Dto.ValidateResult ValidateUser(string username, string password, bool isLogin)
        {
            Dto.ValidateResult validateResult = new Dto.ValidateResult();
            string errMsg;

            DZMembership member = dzmembershipDomainService.ValidateUser(username, password, isLogin, out errMsg);
            if (member == null)
            {
                validateResult.IsValidated = false;
                validateResult.ValidateErrMsg = errMsg;
            }
            else
            {
                validateResult.ValidatedMember = Mapper.Map<DZMembership, Dto.MemberDto>(member);
            }
            return validateResult;

        }

        [UnitOfWork]
        public ValidateResult Login(string userNameOrUserId, string password)
        {
            return ValidateUser(userNameOrUserId, password, true);
        }

        /// <summary>
        /// 申请密码恢复
        /// </summary>
        /// <param name="userName"></param>
        [UnitOfWork]
        public ActionResult ApplyRecovery(string userName, string hostInMail)
        {
            ActionResult result = new ActionResult();
            DZMembership member = dzmembershipDomainService.GetUserByName(userName);
            if (member == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "不存在此用户";
            }
            else
            {
                try
                {
                    string body = member.BuildRecoveryContent(hostInMail);
                    string tilte = "一点办-密码重置";
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
            string[] recoveryParameters = recoveryString.Split(new string[] { Config.pwssword_recovery_spliter }, StringSplitOptions.None);
            string userName = encryptService.Decrypt(recoveryParameters[0], false);
            string recoveryCode = recoveryParameters[1];
            DZMembership member = repositoryMembership.GetMemberByName(userName);
            repositoryUserToken.DeleteToken(member.Id.ToString());
            return member.RecoveryPassword(recoveryCode, newPassword, encryptService.GetMD5Hash(newPassword));


        }

        [UnitOfWork]
        public ActionResult RecoveryPasswordByPhone(string phone, string newPassword)
        {
            ActionResult result = new ActionResult();
            DZMembership member = repositoryMembership.GetMemberByName(phone);
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
        public ActionResult ChangeUserCity(Guid memberId, string cityCode, string longitude, string latitude, string areaId)
        {
            ActionResult result = new ActionResult();
            DZMembership member = repositoryMembership.GetMemberById(memberId);
            if (member == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "该用户不存在!";
            }
            if (!string.IsNullOrEmpty(cityCode))
            {
                member.UserCity = cityCode;
            }
            if (string.IsNullOrEmpty(longitude) ^ string.IsNullOrEmpty(latitude))
            {
                result.IsSuccess = false;
                result.ErrMsg = "经纬度不能只传一个!";
            }
            if (!string.IsNullOrEmpty(longitude))
            {
                member.Longitude = longitude;
            }
            if (!string.IsNullOrEmpty(latitude))
            {
                member.Latitude = latitude;
            }
            if (!string.IsNullOrEmpty(areaId))
            {
                member.AreaId = areaId;
            }
            repositoryMembership.Update(member);
            return result;
        }

        [UnitOfWork]
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            DZMembership member = repositoryMembership.GetMemberByName(userName);
            string oldEncryptedPassword = encryptService.GetMD5Hash(oldPassword);
            string newEncryptedPassword = encryptService.GetMD5Hash(newPassword);
            repositoryUserToken.DeleteToken(member.Id.ToString());
            return member.ChangePassword(oldEncryptedPassword, newPassword, newEncryptedPassword);
        }

        [UnitOfWork]
        public ActionResult ChangePhone(string userId, string newPhone)
        {
            ActionResult result = new ActionResult();
            DZMembership member = repositoryMembership.GetMemberById(new Guid(userId));
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
            ActionResult result = new ActionResult();
            DZMembership member = repositoryMembership.GetMemberById(new Guid(userId));
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
        public IList<MemberDto> GetUsers(TraitFilter filter, string name, string email, string phone, string loginType, string userType)
        {
            //(UserType)Enum.Parse(typeof(UserType), userType)
            LoginType lType = LoginType.None;
            Enum.TryParse<LoginType>(loginType, out lType);
            IList<DZMembership> memberList = repositoryMembership.GetUsers(filter, name, email, phone, lType, (UserType)Enum.Parse(typeof(UserType), userType));

            return Mapper.Map<IList<DZMembership>, IList<MemberDto>>(memberList);
        }
        [Obsolete("尽快移除")]
        [UnitOfWork]
        public long GetUsersCount(string name, string email, string phone, string loginType, string userType)
        {
            LoginType lType = LoginType.None;
            Enum.TryParse<LoginType>(loginType, out lType);
            return repositoryMembership.GetUsersCount(name, email, phone, lType, (UserType)Enum.Parse(typeof(UserType), userType));
        }
        [UnitOfWork]
        public MemberDto Login3rd(string platform, string code, string appName, string userType)
        {
            DZMembership membership = login3rdService.Login(platform, code, appName, userType);

            return Mapper.Map<MemberDto>(membership);

        }
        [UnitOfWork]
        public ActionResult ChangeAlias(string userId, string neAlias)
        {
            DZMembership member = repositoryMembership.GetMemberById(new Guid(userId));

            member.NickName = neAlias;

            return new ActionResult();
        }
        [UnitOfWork]
        public ActionResult ChangeAddress(string userId, string newAddress)
        {
            DZMembership member = repositoryMembership.GetMemberById(new Guid(userId));

            member.Address = newAddress;

            return new ActionResult();
        }
        [UnitOfWork]
        public ActionResult ChangeAvatar(string userId, string newAvatar)
        {
            DZMembership member = repositoryMembership.GetMemberById(new Guid(userId));

            member.AvatarUrl = newAvatar;

            return new ActionResult();
        }
        [UnitOfWork]
        public RegisterResult RegisterStaff(string registerName, string password, string confirmPassword, string hostInMail)
        {
            return RegisterMember(registerName, password, confirmPassword, UserType.staff.ToString(), hostInMail);
        }
        [UnitOfWork]
        public IList<MemberDto> GetAllCustomer(int currentPageIndex, int pageSize, out long totalRecord)
        {
            TraitFilter filter = new TraitFilter { pageNum = currentPageIndex, pageSize = pageSize };
            IList<DZMembership> memberList = repositoryMembership.GetUsers(filter, string.Empty, string.Empty, string.Empty, LoginType.None, UserType.customer);
            totalRecord = repositoryMembership.GetUsersCount(string.Empty, string.Empty, string.Empty, LoginType.None, UserType.customer);
            return Mapper.Map<IList<DZMembership>, IList<MemberDto>>(memberList);
        }

        /// <summary>
        /// 昨日新增用户
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public long GetCountOfNewMembershipsYesterdayByArea(IList<string> areaList, UserType userType)
        {
            DateTime beginTime = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            return repositoryMembership.GetUsersCountByArea(areaList, beginTime, beginTime.AddDays(1), userType);
        }

        /// <summary>
        /// 当前用户总量
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
        /// 上月用户在线活跃度（数量）
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public long GetCountOfLoginMembershipsLastMonthByArea(IList<string> areaList, UserType userType)
        {
            IList<DZMembership> memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, userType);
            DateTime baseTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
            IList<MembershipLoginLog> loginList = repositoryMembershipLoginLog.GetMembershipLoginLogListByTime(baseTime.AddMonths(-1), baseTime);
            return statisticsMembershipCount.StatisticsLoginCountLastMonth(memberList, loginList);
        }
        /// <summary>
        /// 统计用户每日或每时新增数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="strBeginTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsNewMembershipsCountListByTime(IList<string> areaList, string strBeginTime, string strEndTime, UserType userType)
        {
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<DZMembership> memberList = repositoryMembership.GetUsersByArea(areaList, BeginTime, EndTime, userType);
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsNewMembershipsCountListByTime(memberList, BeginTime, EndTime, strBeginTime == strEndTime);
            return statisticsInfo;
        }
        /// <summary>
        /// 统计用户每日或每时累计数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="strBeginTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllMembershipsCountListByTime(IList<string> areaList, string strBeginTime, string strEndTime, UserType userType)
        {
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<DZMembership> memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, userType);
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListByTime(memberList, BeginTime, EndTime, strBeginTime == strEndTime);
            return statisticsInfo;
        }
        /// <summary>
        /// 统计用户每日或每时在线活跃度（数量）列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="strBeginTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsLoginCountListByTime(IList<string> areaList, string strBeginTime, string strEndTime, UserType userType)
        {
            DateTime BeginTime = Common.StringHelper.ParseToDate(strBeginTime, false);
            DateTime EndTime = Common.StringHelper.ParseToDate(strEndTime, true);
            IList<DZMembership> memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, userType);
            IList<MembershipLoginLog> loginList = repositoryMembershipLoginLog.GetMembershipLoginLogListByTime(BeginTime, EndTime);
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsLoginCountListByTime(memberList, loginList, BeginTime, EndTime, strBeginTime == strEndTime);
            return statisticsInfo;
        }

        /// <summary>
        /// 根据用户手机系统统计用户数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllMembershipsCountListByAppName(IList<string> areaList, UserType userType)
        {
            IList<DZMembership> memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, userType);
            IList<MembershipLoginLog> loginList = repositoryMembershipLoginLog.GetMembershipLastLoginLog();
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListByAppName(memberList, loginList);
            return statisticsInfo;
        }

        /// <summary>
        /// 根据用户性别统计用户数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllMembershipsCountListBySex(IList<string> areaList, UserType userType)
        {
            IList<DZMembership> memberList = repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, userType);
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountListBySex(memberList);
            return statisticsInfo;
        }

        /// <summary>
        /// 根据用户所在子区域统计用户数量列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public StatisticsInfo GetStatisticsAllMembershipsCountListByArea(IList<Area> areaList, UserType userType)
        {
            IList<string> AreaIdList = areaList.Select(x => x.Id.ToString()).ToList();
            IList<DZMembership> memberList = repositoryMembership.GetUsersByArea(AreaIdList, DateTime.MinValue, DateTime.MinValue, userType);
            StatisticsInfo statisticsInfo = statisticsMembershipCount.StatisticsAllMembershipsCountGroupByArea(memberList, areaList);
            return statisticsInfo;
        }

        /// <summary>
        /// 根据用户所在子区域统计用户列表
        /// </summary>
        /// <param name="areaList"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public IList<Dto.MemberDto> GetMembershipsByArea(IList<string> areaList, UserType userType)
        {
            IList<Dto.MemberDto> memberDtoList = Mapper.Map<IList<Dto.MemberDto>>(repositoryMembership.GetUsersByArea(areaList, DateTime.MinValue, DateTime.MinValue, userType));
            return memberDtoList;
        }

        public ActionResult<MemberDto> GetAreaAgent(string city)
        {
            ActionResult<MemberDto> result = new ActionResult<MemberDto>();
            try
            {
                DZMembership agentForCity = repositoryMembership.FindOne(x => x.UserCity == city);
                result.ResultObject = Mapper.Map<MemberDto>(agentForCity);
            }
            catch
            {
                result.IsSuccess = false;
                result.ErrMsg = "错误.该城市出现多个代理:" + city;
            }
            return result;
        }

        /// <summary>
        /// 用户信息补全
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        [UnitOfWork]
        public ActionResult CompleteDZMembership(MemberDto membershipDto)
        {
            ActionResult result = new ActionResult();
            try
            {
                DZMembership dzMembership = Mapper.Map<DZMembership>(membershipDto);
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
        /// 申请助理
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="realName"></param>
        /// <param name="personalID"></param>
        /// <param name="phone"></param>
        /// <param name="certificatesImageList"></param>
        /// <param name="diplomaImage"></param>
        /// <returns></returns>
        [UnitOfWork]
        public ActionResult ApplyDZMembershipCustomerService(string membershipId, string realName, string personalID, string phone, IList<Dto.DZMembershipImageDto> certificatesImageList, Dto.DZMembershipImageDto diplomaImage)
        {
            ActionResult result = new ActionResult();
            try
            {
                if (string.IsNullOrEmpty(realName))
                {
                    throw new Exception("真实姓名不能为空");
                }
                if (string.IsNullOrEmpty(personalID))
                {
                    throw new Exception("身份证件不能为空");
                }
                if (string.IsNullOrEmpty(phone))
                {
                    throw new Exception("手机不能为空");
                }
                if (diplomaImage == null)
                {
                    throw new Exception("学历证明不能为空");
                }
                if (certificatesImageList == null || certificatesImageList.Count == 0)
                {
                    throw new Exception("证件照片不能为空");
                }
                DZMembershipCustomerService dzMembership = dzmembershipDomainService.GetDZMembershipCustomerServiceById(membershipId);
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
        /// 认证审核助理
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        [UnitOfWork]
        public Dto.RegisterResult VerifyDZMembershipCustomerService(string membershipId, bool isVerified, string strMemo)
        {
            RegisterResult result = new RegisterResult();
            try
            {
                DZMembershipCustomerService dzMembership = dzmembershipDomainService.GetDZMembershipCustomerServiceById(membershipId);
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
        /// 封停/解封助理
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        [UnitOfWork]
        public Dto.RegisterResult LockDZMembershipCustomerService(string membershipId, bool isLocked, string strMemo)
        {
            RegisterResult result = new RegisterResult();
            try
            {
                DZMembershipCustomerService dzMembership = dzmembershipDomainService.GetDZMembershipCustomerServiceById(membershipId);
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
        /// 根据用户名获取客服
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [UnitOfWork]
        public Dto.DZMembershipCustomerServiceDto GetDZMembershipCustomerServiceByName(string userName)
        {
            DZMembershipCustomerService membership = dzmembershipDomainService.GetDZMembershipCustomerServiceByName(userName);
            
            Dto.DZMembershipCustomerServiceDto memberDto = Mapper.Map<DZMembershipCustomerService, Dto.DZMembershipCustomerServiceDto>(membership);
            return memberDto;
        }

        /// <summary>
        /// 根据用户Id获取客服
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [UnitOfWork]
        public Dto.DZMembershipCustomerServiceDto GetDZMembershipCustomerServiceById(string id)
        {
            DZMembershipCustomerService membership = dzmembershipDomainService.GetDZMembershipCustomerServiceById(id);
            
            Dto.DZMembershipCustomerServiceDto memberDto = Mapper.Map<DZMembershipCustomerService, Dto.DZMembershipCustomerServiceDto>(membership);
            return memberDto;
        }

    }
}
