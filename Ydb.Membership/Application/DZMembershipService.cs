using System;
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

namespace Ydb.Membership.Application
{
    public class DZMembershipService : IDZMembershipService
    {
        log4net.ILog log = log4net.LogManager.GetLogger(" Ydb.Membership.Application.DZMembershipService");
        IDZMembershipDomainService dzmembershipDomainService;
        IEmailService emailService;
        IRepositoryDZMembership repositoryMembership;
        ILogin3rd login3rdService;
        IEncryptService encryptService;

        public DZMembershipService(IDZMembershipDomainService dzmembershipDomainService,
            IRepositoryDZMembership repositoryMembership,
            IEmailService emailService,IEncryptService encryptService,
            ILogin3rd login3rdService)
        {
            this.dzmembershipDomainService = dzmembershipDomainService;// Bootstrap.Container.Resolve<IDZMembershipDomainService>();
            this.login3rdService = login3rdService;// Bootstrap.Container.Resolve<ILogin3rd>();
            this.emailService = emailService;
            this.repositoryMembership = repositoryMembership;// Bootstrap.Container.Resolve<IRepositoryDZMembership>();
            this.encryptService = encryptService;

        }



         
        public Dto.RegisterResult RegisterMember(string registerName, string password, string confirmPassword, string strUserType, string hostInMail)
        {
            UserType userType =(UserType) Enum.Parse(typeof(UserType), strUserType);

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
            registerResult.o = registeredUser;
            return registerResult;

        }
        [UnitOfWork]
        public Dto.RegisterResult RegisterCustomerService(string registerName, string password, string confirmPassword, string hostInMail)
        {
            return RegisterMember(registerName, password, confirmPassword, UserType.customerservice.ToString(), hostInMail);

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
        public ActionResult VerifyRegisterCode(string verifyCode, string userid) {

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
            DZMembership membership = repositoryMembership.GetMemberById(new Guid (id));
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
            }
            return member.ChangePasswordByPhone( newPassword, encryptService.GetMD5Hash(newPassword));
        }


        [UnitOfWork]
        public ActionResult ChangeUserCity(Guid memberId, string cityCode)
        {
            ActionResult result = new ActionResult();
            DZMembership member = repositoryMembership.GetMemberById(memberId);
            if (member == null)
            {
                result.IsSuccess = false;
                result.ErrMsg = "该用户不存在!";
            }
            member.UserCity = cityCode;
            repositoryMembership.Update(member);
            return result;
        }

        [UnitOfWork]
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            DZMembership member = repositoryMembership.GetMemberByName(userName);
            string oldEncryptedPassword = encryptService.GetMD5Hash(oldPassword);
            string newEncryptedPassword = encryptService.GetMD5Hash(newPassword);
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
            IList<DZMembership> memberList = repositoryMembership.GetUsers(filter, name, email, phone, loginType, userType);

          return  Mapper.Map<IList<DZMembership>, IList<MemberDto>>(memberList);
        }
        [Obsolete("尽快移除")]
        [UnitOfWork]
        public long GetUsersCount(string name, string email, string phone, string loginType, string userType)
        {
         return   repositoryMembership.GetUsersCount(name, email, phone, loginType, userType);
        }
        [UnitOfWork]
        public MemberDto Login3rd(string platform, string code, string appName, string userType)
        {
          DZMembership membership=  login3rdService.Login(platform, code, appName, userType);

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

        public RegisterResult RegisterStaff(string registerName, string password, string confirmPassword, string hostInMail)
        {
            return RegisterMember(registerName, password, confirmPassword, UserType.staff.ToString(), hostInMail);
        }

        public IList<MemberDto> GetAllCustomer(int currentPageIndex, int pageSize, out long totalRecord)
        {
            TraitFilter filter = new TraitFilter { pageNum=currentPageIndex,pageSize=pageSize };
            IList<DZMembership> memberList = repositoryMembership.GetUsers(filter, string.Empty, string.Empty, string.Empty, string.Empty,UserType.customer.ToString());
            totalRecord = repositoryMembership.GetUsersCount(string.Empty, string.Empty, string.Empty, string.Empty, UserType.customer.ToString());
            return Mapper.Map<IList<DZMembership>, IList<MemberDto>>(memberList);
        }
    }
}
