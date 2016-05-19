using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.Util;
using Dianzhu.DAL;
using Dianzhu.Model;
using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
using System.Net;
using Dianzhu.BLL.Validator;
using FluentValidation;
namespace Dianzhu.BLL
{
    /// <summary>
    /// ddd:applicationservice
    /// </summary>
    public class DZMembershipProvider : MembershipProvider
    {
        DALMembership DALMembership = null;
        public DZMembershipProvider() {
           DALMembership = DALFactory.DALMembership;
        }
        public DZMembershipProvider(DALMembership dal) {
            DALMembership = dal;
        }
        
        #region override of membership provider
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

            if (!this.ValidateUser(username, oldPassword))
            {
                throw new Exception("原密码有误,请核实后重新输入");
            }
            if (newPassword.Length < this.MinRequiredPasswordLength)
            {

                throw new ArgumentException("密码长度至少" + this.MinRequiredPasswordLength + "位");
            }

            DZMembership member = DALMembership.GetMemberByName(username);

            member.ChangePassword(oldPassword, newPassword);
            DALMembership.Update(member);
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            DZMembership user = new DZMembership
            {
                UserName = username,
                Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"),
                TimeCreated = DateTime.Now
            };
            DALMembership.CreateUser(user);
            MembershipUser mu = new MembershipUser("DZMembershipProvider",
                 username, user.Id, "", "", string.Empty,
                 true, true, DateTime.Now,
                 DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            status = MembershipCreateStatus.Success;
            return mu;
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

            DZMembership user = DALMembership.GetMemberByName(username);
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
           
            string encryptedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

           DZMembership member = DALMembership.ValidateUser(username, encryptedPwd);

            if (member == null) { return false; }
            else {
                member.LoginTimes += 1;
                DALMembership.Update(member);
                return true;
            }
           
        }
        #endregion

        #region additional method for user
       
        
        public DZMembership CreateUser(string userName, string userPhone, string userEmail, string password, out MembershipCreateStatus createStatus, Dianzhu.Model.Enums.enum_UserType userType)
        {
            createStatus = MembershipCreateStatus.ProviderError;
            var savedUserName = !string.IsNullOrEmpty(userName) ? userName : string.IsNullOrEmpty(userPhone) ? userEmail : userPhone;
            string userNameForOpenfire = savedUserName;
           
            Guid validateCode = Guid.Empty;
            if (System.Text.RegularExpressions.Regex.IsMatch(savedUserName, @".+@.+\..+"))
            {
                if (string.IsNullOrEmpty(userEmail))
                {
                    userEmail = savedUserName;

                }

                userNameForOpenfire = savedUserName.Replace("@", "||");
                validateCode=Guid.NewGuid();
            }
            else
            {
                if (string.IsNullOrEmpty(userPhone))
                {
                    userPhone = savedUserName;
                }
            }
            
            var user = GetUserByName(savedUserName);
            if (user != null)
            {
                createStatus = MembershipCreateStatus.DuplicateUserName;
                return null;
            }
            else
            {
                var password_cred = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                DZMembership newMember = new DZMembership
                {
                    UserName = savedUserName,
                    Password = password_cred,
                    Email = userEmail,
                    Phone = userPhone,
                    NickName = savedUserName,
                    PlainPassword = password,
                    UserNameForOpenFire = userNameForOpenfire,
                    UserType = Model.Enums.enum_UserType.customer,
                };
                if (validateCode != Guid.Empty)
                {
                    newMember.RegisterValidateCode = validateCode.ToString();
                }
                
                DALMembership.Add(newMember);
                createStatus = MembershipCreateStatus.Success;
                return newMember;
            }
        }
        public void CreateUserForU3rd(DZMembership member)
        {
            DALMembership.Update(member);
        }
        public DZMembership GetUserByWechatOpenId(string openid)
        {
            return DALMembership.GetMemberByWechatOpenId(openid);
        }
        public DZMembership GetUserByQQOpenId(string openid)
        {
            return DALMembership.GetMemberByQQOpenId(openid);
        }
        public DZMembership GetUserBySinaWeiboUId(long uid)
        {
            return DALMembership.GetMemberBySinaWeiboUid(uid);
        }
        public virtual DZMembership GetUserByName(string name)
        {
            return DALMembership.GetMemberByName(name);
        }
        public virtual DZMembership GetUserById(Guid id)
        {
            return DALMembership.FindById(id);
        }
        public IList<DZMembership> GetAllDZMembers(int pageIndex, int pageSize, out long totalRecords)
        {
            return DALMembership.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }
      
        public void UpdateDZMembership(DZMembership member)
        {

            //var password_cred = FormsAuthentication.HashPasswordForStoringInConfigFile(member.Password, "MD5");
            //member.Password = password_cred;
            DALMembership.Update(member);
        }
        public DZMembership GetUserByEmail(string email)
        {
            return DALMembership.GetMemberByEmail(email);
        }
        public DZMembership GetUserByPhone(string phone)
        {

            return DALMembership.GetMemberByPhone(phone);
        }
        #endregion


        //获取用户的接待次数


    }

    
}