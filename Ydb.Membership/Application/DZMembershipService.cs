using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.Infrastructure.UnitOfWork;
using AutoMapper;
using Ydb.Membership.Application.Dto;

namespace Ydb.Membership.Application
{
    public class DZMembershipService : IDZMembershipService
    {
        log4net.ILog log = log4net.LogManager.GetLogger(" Ydb.Membership.Application.DZMembershipService");
        IDZMembershipDomainService dzmembershipDomainService;
        IEmailService emailService;
        
        public DZMembershipService(IDZMembershipDomainService dzmembershipDomainService, IEmailService emailService)
        {
            this.dzmembershipDomainService = dzmembershipDomainService;
            this.emailService = emailService;

        }


        [UnitOfWork]
        public Dto.RegisterResult RegisterBusinessUser(string registerName, string password, string confirmPassword)
        {
            Dto.RegisterResult registerResult = new Dto.RegisterResult();
            if (password != confirmPassword)
            {
                registerResult.RegisterSuccess = false;
                registerResult.RegisterErrMsg = "密码不匹配";

                return registerResult;
            }
            string errMsg;
            DZMembership createdUser = dzmembershipDomainService.CreateUser(registerName, password, UserType.business, out errMsg);
            if (!string.IsNullOrEmpty(createdUser.Email))
            {
                try
                {
                    emailService.SendEmail(createdUser.Email, "一点办注册验证邮件", 
                        createdUser.BuildRegisterValidationContent(Dianzhu.Config.Config.GetAppSetting("ImServer"))
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
        public Dto.MemberDto GetUserByName(string userName)
        {
            DZMembership membership = dzmembershipDomainService.GetUserByName(userName);
            if (membership == null) { return null; }

            Dto.MemberDto memberDto = Mapper.Map<DZMembership, Dto.MemberDto>(membership);//new Dto.MemberDto { Id = membership.Id, UserName = membership.UserName };
            return memberDto;
        }
        [UnitOfWork]
        public Dto.ValidateResult ValidateUser(string username, string password,bool isLogin)
        {
            Dto.ValidateResult validateResult = new Dto.ValidateResult();
            string errMsg;
             
            DZMembership member= dzmembershipDomainService.ValidateUser(username, password,isLogin,out errMsg);
            if (member == null)
            {
                validateResult.IsValidated = false;
                validateResult.ValidateErrMsg = errMsg;
            }
            else
            {
                validateResult.ValidatedMember= Mapper.Map<DZMembership, Dto.MemberDto>(member);
            }
            return validateResult;

        }

        [UnitOfWork]
        public ValidateResult Login(string userNameOrUserId, string password)
        {
            return ValidateUser(userNameOrUserId, password, true);
        }
    }
}
