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
namespace Ydb.Membership.Application
{
    public class DZMembershipService : IDZMembershipService
    {
        log4net.ILog log = log4net.LogManager.GetLogger(" Ydb.Membership.Application.DZMembershipService");
        IDZMembershipDomainService dzmembershipDomainService;
        IEmailService emailService;
        IRepositoryDZMembership repositoryMembership;


        public DZMembershipService(IDZMembershipDomainService dzmembershipDomainService, IEmailService emailService, IRepositoryDZMembership repositoryMembership)
        {
            this.dzmembershipDomainService = dzmembershipDomainService;
            this.emailService = emailService;
            this.repositoryMembership = repositoryMembership;

        }



         
        private Dto.RegisterResult RegisterMember(string registerName, string password, string confirmPassword, UserType userType, string hostInMail)
        {
            Dto.RegisterResult registerResult = new Dto.RegisterResult();
            if (password != confirmPassword)
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = "密码不匹配";

                return registerResult;
            }
            string errMsg;
            DZMembership createdUser = dzmembershipDomainService.CreateUser(registerName, password, userType, out errMsg);
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
            return registerResult;

        }
        [UnitOfWork]
        public Dto.RegisterResult RegisterCustomerService(string registerName, string password, string confirmPassword, string hostInMail)
        {
            return RegisterMember(registerName, password, confirmPassword, UserType.customerservice, hostInMail);

        }

        [UnitOfWork]
        public Dto.RegisterResult RegisterBusinessUser(string registerName, string password, string confirmPassword, string hostInMail)
        {
            return RegisterMember(registerName, password, confirmPassword, UserType.business, hostInMail);

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
            string userName = EncryptService.Decrypt(recoveryParameters[0], false);
            string recoveryCode = recoveryParameters[1];

            DZMembership member = repositoryMembership.GetMemberByName(userName);
            return member.RecoveryPassword(recoveryCode, newPassword, EncryptService.GetMD5Hash(newPassword));


        }

        [UnitOfWork]
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            DZMembership member = repositoryMembership.GetMemberByName(userName);
            string oldEncryptedPassword = EncryptService.GetMD5Hash(oldPassword);
            string newEncryptedPassword = EncryptService.GetMD5Hash(newPassword);
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
    }
}
