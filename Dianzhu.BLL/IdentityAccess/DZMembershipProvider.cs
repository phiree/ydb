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
using Dianzhu.IDAL;
using System.Runtime.Remoting.Contexts;
using Castle.Windsor;
using DDDCommon;

namespace Dianzhu.BLL
{
    /// <summary>
    /// ddd:applicationservice
    /// </summary>
    public class DZMembershipProvider : MembershipProvider
    {
        public DZMembershipProvider() {
            DALMembership = new DALMembership();
            var containerAccessor = System.Web.HttpContext.Current.ApplicationInstance as IContainerAccessor;
            var container = containerAccessor.Container;
            this.DALMembership = container.Resolve<IDALMembership>();
            this.encryptService = container.Resolve<IEncryptService>();
            this.emailService = container.Resolve<IEmailService>();

        }
        IDALMembership DALMembership = null;
        IEncryptService encryptService;
        IEmailService emailService;
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.DZMembershipProvider");
        public DZMembershipProvider(IDALMembership dal, IEncryptService encryptService, IEmailService emailService) {
            DALMembership = dal;
            this.encryptService = encryptService;
            this.emailService = emailService;
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
                Password = encryptService.GetMD5Hash(password),
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

            string encryptedPwd = encryptService.GetMD5Hash(password);

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
                validateCode = Guid.NewGuid();
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
                var password_cred = encryptService.GetMD5Hash(password).ToUpper();
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
                    PlatForm= Model.Enums.enum_PlatFormType.system
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
            DALMembership.Add(member);
        }
        public void UpdateUserForU3rd(DZMembership member)
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
        public IList<DZMembership> GetUsers(Model.Trait_Filtering filter, string name, string email, string phone,string platform,string userType)
        {
            var where = PredicateBuilder.True<DZMembership>();
            where = where.And(x => x.UserType.ToString() == userType);
            if (name != null && name !="")
            {
                where = where.And(x => x.DisplayName .Contains(name));
            }
            if (email != null && email != "")
            {
                where = where.And(x => x.Email == email);
            }
            if (phone != null && phone != "")
            {
                where = where.And(x => x.Phone == phone);
            }
            if (platform != null && platform != "")
            {
                where = where.And(x => x.PlatForm.ToString() == platform);
            }
            DZMembership baseone = null;
            if (filter.baseID != null && filter.baseID != "")
            {
                try
                {
                    baseone = DALMembership.FindById(new Guid(filter.baseID));
                }
                catch
                {
                    baseone = null;
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? DALMembership.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : DALMembership.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }

        /// <summary>
        /// 统计用户数量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="platform"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public long GetUsersCount(string name, string email, string phone, string platform, string userType)
        {
            var where = PredicateBuilder.True<DZMembership>();
            where = where.And(x => x.UserType.ToString() == userType);
            if (name != null && name != "")
            {
                where = where.And(x => x.DisplayName.Contains(name));
            }
            if (email != null && email != "")
            {
                where = where.And(x => x.Email == email);
            }
            if (phone != null && phone != "")
            {
                where = where.And(x => x.Phone == phone);
            }
            if (platform != null && platform != "")
            {
                where = where.And(x => x.PlatForm.ToString() == platform);
            }
            long count = DALMembership.GetRowCount(where);
            return count;
        }
        #endregion


        public bool SendValidationMail(string to, string verifyUrl)
        {
            string subjecst = "一点办验证邮件";
            bool sendSuccess = true;
            string body = "感谢您加入一点办.请点击下面的连接验证您的注册邮箱.</br>"
                        + "<a style='border:solid 1px #999;margin:20px;padding:10px 40px; background-color:#eee' href='"
                            + verifyUrl + "'>点击验证</a><br/><br/><br/>"
                        + "如果你无法点击此链接,请将下面的网址粘贴到浏览器地址栏.<br/><br/><br/>"
                        + verifyUrl;
            ;
            try
            {
                emailService.SendEmail(to, subjecst, body);
            }
            catch (Exception ex)
            {
                sendSuccess = false;
                PHSuit.ExceptionLoger.ExceptionLog(log, ex);
                
            }
            return sendSuccess;
            //SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            //SmtpClient client = new SmtpClient(smtpSection.Network.Host, smtpSection.Network.Port);
            //client.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
            //MailMessage mail = new MailMessage(smtpSection.From, "550700860@qq.com");
            //mail.Subject = "this is a test email.";
            //mail.Body = "this is my test email body";
            //client.Send(mail);
        }
        public bool SendRecoveryMail(string to, string recoveryUrl)
        {
            string subjecst = "一点办--密码重置邮件";
            bool sendSuccess = true;
            string body = "您已申请密码重置.请点击下面的连接重置您的密码.</br>"
                        + "<a style='border:solid 1px #999;margin:20px;padding:10px 40px; background-color:#eee' href='"
                        + recoveryUrl + "'>点击验证</a><br/><br/><br/>"
                        + "如果你无法点击此链接,请将下面的网址粘贴到浏览器地址栏.<br/><br/><br/>"
                        + recoveryUrl;
            try
            {
                emailService.SendEmail(to, subjecst, body);
            }
            catch (Exception ex)
            {
                sendSuccess = false;
            }
            return sendSuccess;
            //SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            //SmtpClient client = new SmtpClient(smtpSection.Network.Host, smtpSection.Network.Port);
            //client.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
            //MailMessage mail = new MailMessage(smtpSection.From, "550700860@qq.com");
            //mail.Subject = "this is a test email.";
            //mail.Body = "this is my test email body";
            //client.Send(mail);
        }
        public IList<DZMembership> GetAllCustomer(int pageIndex, int pageSize, out long totalRecords)
        {
            return DALMembership.GetAllCustomer(pageIndex, pageSize, out totalRecords);
        }

    }

    
}