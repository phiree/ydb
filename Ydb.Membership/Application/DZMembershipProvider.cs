using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.Util;

using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
using System.Net;

using System.Runtime.Remoting.Contexts;


using Ydb.Common.Specification;
using Ydb.Membership.DomainModel.Enums;
using Castle.Windsor;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.DomainModel;
namespace Ydb.Membership.Application
{
    /// <summary>
    /// ddd:applicationservice
    /// todo: 使用 aspnet identity 替代 membershipprovider
    /// 只为使用内置的 asp.net membership,不要外部调用.
    /// </summary>
    public class DZMembershipProvider : MembershipProvider
    {
        IDZMembershipDomainService dzmembershipDomainService;
        public DZMembershipProvider() {
           
            var containerAccessor = System.Web.HttpContext.Current.ApplicationInstance as IContainerAccessor;
            var container = containerAccessor.Container;
            this.dzmembershipDomainService = container.Resolve<IDZMembershipDomainService>();
        }
        public DZMembershipProvider(IDZMembershipDomainService dzmembershipDomainServic)
        {
            this.dzmembershipDomainService = dzmembershipDomainServic;
        }
        
        

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            string errmsg;
          return  dzmembershipDomainService.ChangePassword(username, oldPassword, newPassword,out errmsg);
 
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException("请使用DZMembershipService创建用户");
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {

            throw new NotImplementedException("请调用IList<DZMembership>  DZMembershipProvider.GetALLUsers");
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {

            DZMembership user = dzmembershipDomainService.GetUser(username,userIsOnline);
            if (user == null) return null;
            MembershipUser mu = new MembershipUser("DZMembershipProvider",
                 username, user.Id, "", "", string.Empty,
                 true, true, DateTime.Now,
                 DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            return mu;

        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 10; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Encrypted; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            string errMsg;
           DZMembership member=  dzmembershipDomainService.ValidateUser(username, password,false,out errMsg);
            return member != null;

        }
 
        
    }

    
}