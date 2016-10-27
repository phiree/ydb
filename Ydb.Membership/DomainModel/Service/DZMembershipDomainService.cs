 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Ydb.Common.Domain;
using Ydb.Common.Specification;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Membership.DomainModel.Repository;
namespace Ydb.Membership.DomainModel
{
    /// <summary>
    /// 基本的用户类.
    /// </summary>
    public class DZMembershipDomainService:IDZMembershipDomainService
    {
        IRepositoryDZMembership repositoryDZMembership;
        IRepositoryUserToken repositoryUserToken;
        IEmailService emailService;
        IEncryptService encryptService;
        ILoginNameDetermine loginNameDetermine;
        public DZMembershipDomainService(IRepositoryDZMembership repositoryDZMembership, IEmailService emailService,
        IEncryptService encryptService,
            IRepositoryUserToken repositoryUserToken, ILoginNameDetermine loginNameDetermine)
        {
            this.emailService = emailService;
            this.encryptService = encryptService;
            this.repositoryDZMembership = repositoryDZMembership;
            this.repositoryUserToken = repositoryUserToken;
            this.loginNameDetermine = loginNameDetermine;
        }
        public   bool ChangePassword(string username, string oldPassword, string newPassword)
        {

            if (!this.ValidateUser(username, oldPassword))
            {
                throw new Exception("原密码有误,请核实后重新输入");
            }
            if (newPassword.Length <6)
            {

                throw new ArgumentException("密码长度至少6位");
            }

            DZMembership member = repositoryDZMembership.GetMemberByName(username);
            string oldPasswordEncrypted = encryptService.Encrypt(oldPassword, false);
            string newPasswordEncrypted = encryptService.Encrypt(newPassword, false);
            member.ChangePassword(oldPasswordEncrypted, newPassword, newPasswordEncrypted);
            repositoryDZMembership.Update(member);

            System.Runtime.Caching.MemoryCache.Default.Remove(member.Id.ToString());
            UserToken ut = repositoryUserToken.GetToken(member.Id.ToString());
            ut.Flag = 0;
            repositoryUserToken.Update(ut);

            return true;
        }

      

        public   DZMembership CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey)
        {
            DZMembership user = new DZMembership
            {
                UserName = username,
                Password = encryptService.GetMD5Hash(password),
                TimeCreated = DateTime.Now
            };
            repositoryDZMembership.CreateUser(user);
            
            return user;
        }

       

        public   DZMembership GetUser(string username, bool userIsOnline)
        {

            DZMembership user = repositoryDZMembership.GetMemberByName(username);
            if (user == null) return null;
             
            return user;

        }

       

       

        public   bool ValidateUser(string username, string password)
        {

            string encryptedPwd = encryptService.GetMD5Hash(password);

            DZMembership member = repositoryDZMembership.ValidateUser(username, encryptedPwd);

            if (member == null) { return false; }
            else
            {
                member.LoginTimes += 1;
                repositoryDZMembership.Update(member);
                return true;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="requireUserType">该用户应该的登录类型</param>
        /// <returns></returns>
        public bool ValidateUser(string username, string password, UserType requireUserType,out string errMsg )
        {
            bool valid = false;
            errMsg = string.Empty;

            string encryptedPwd = encryptService.GetMD5Hash(password);

            DZMembership member = repositoryDZMembership.ValidateUser(username, encryptedPwd);

            if (member == null)
            {
                errMsg = "用户名或密码有误";
            }
            else if (member.UserType !=requireUserType)
            {
                errMsg = "用户类型有误";
            }
            else
            {
                member.LoginTimes += 1;
                repositoryDZMembership.Update(member);
                valid = true;
            }

            return valid;
        }
 

        #region additional method for user

        public Guid CreateUser(string loginName, string password, UserType userType, out string errMsg)
        {

            errMsg = string.Empty;
            LoginNameType loginNameType = loginNameDetermine.Determin(loginName);
            string userName = loginName,
                    nickName = loginName,
                    email = string.Empty,
                    phone = string.Empty;
            Guid registerValidateCode = Guid.Empty;
            switch (loginNameType)
            {
                case LoginNameType.Email:
                    email = loginName;
                    registerValidateCode = Guid.NewGuid();
                    break;
                case LoginNameType.PhoneNumber: phone = loginName; break;
            }
            var existedUser = GetUserByName(loginName);
            if (existedUser != null)
            {
                errMsg = "用户已存在";
                return Guid.Empty;
            }
            var password_cred = encryptService.GetMD5Hash(password).ToUpper();
            DZMembership newMember = new DZMembership
            {
                UserName = userName,
                Password = password_cred,
                Email = email,
                Phone = phone,
                NickName = nickName,
                PlainPassword = password,

                UserType = userType,
                LoginType = LoginType.original
            };
            if (registerValidateCode != Guid.Empty)
            {
                newMember.RegisterValidateCode = registerValidateCode.ToString();
            }

            repositoryDZMembership.Add(newMember);

            return newMember.Id;

        }
        public void CreateUserForU3rd(DZMembership member)
        {
            repositoryDZMembership.Add(member);
        }
        public void UpdateUserForU3rd(DZMembership member)
        {
            repositoryDZMembership.Update(member);
        }
        public DZMembership GetUserByWechatOpenId(string openid)
        {
            return repositoryDZMembership.GetMemberByWechatOpenId(openid);
        }
        public DZMembership GetUserByQQOpenId(string openid)
        {
            return repositoryDZMembership.GetMemberByQQOpenId(openid);
        }
        public DZMembership GetUserBySinaWeiboUId(long uid)
        {
            return repositoryDZMembership.GetMemberBySinaWeiboUid(uid);
        }
        public virtual DZMembership GetUserByName(string name)
        {
            return repositoryDZMembership.GetMemberByName(name);
        }
        public virtual DZMembership GetUserById(Guid id)
        {
            return repositoryDZMembership.FindById(id);
        }
        public IList<DZMembership> GetAllDZMembers(int pageIndex, int pageSize, out long totalRecords)
        {
            return repositoryDZMembership.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public void UpdateDZMembership(DZMembership member)
        {

            //var password_cred = FormsAuthentication.HashPasswordForStoringInConfigFile(member.Password, "MD5");
            //member.Password = password_cred;
            repositoryDZMembership.Update(member);
        }
        public DZMembership GetUserByEmail(string email)
        {
            return repositoryDZMembership.GetMemberByEmail(email);
        }
        public DZMembership GetUserByPhone(string phone)
        {

            return repositoryDZMembership.GetMemberByPhone(phone);
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
        public IList<DZMembership> GetUsers(TraitFilter filter, string name, string email, string phone, string platform, string userType)
        {
            var where = PredicateBuilder.True<DZMembership>();
            where = where.And(x => x.UserType == (UserType)Enum.Parse(typeof(UserType), userType));
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(x => x.DisplayName.Contains(name));
            }
            if (!string.IsNullOrEmpty(email))
            {
                where = where.And(x => x.Email == email);
            }
            if (!string.IsNullOrEmpty(phone))
            {
                where = where.And(x => x.Phone == phone);
            }
            if (!string.IsNullOrEmpty(platform))
            {
                where = where.And(x => x.LoginType == (LoginType)Enum.Parse(typeof(LoginType), platform));
            }
            DZMembership baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repositoryDZMembership.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? repositoryDZMembership.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : repositoryDZMembership.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }

        /// <summary>
        /// 统计用户数量
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="platform">用户的登录平台(微信,qq,微博,原生)</param>
        /// <param name="userType">用户类型:商户,助理,客户....</param>
        /// <returns></returns>
        public long GetUsersCount(string name, string email, string phone, string platform, string userType)
        {
            var where = PredicateBuilder.True<DZMembership>();
            where = where.And(x => x.UserType == (UserType)Enum.Parse(typeof(UserType), userType));
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(x => x.DisplayName.Contains(name));
            }
            if (!string.IsNullOrEmpty(email))
            {
                where = where.And(x => x.Email == email);
            }
            if (!string.IsNullOrEmpty(phone))
            {
                where = where.And(x => x.Phone == phone);
            }
            if (!string.IsNullOrEmpty(platform))
            {
                where = where.And(x => x.LoginType == (LoginType)Enum.Parse(typeof(LoginType), platform));
            }
            long count = repositoryDZMembership.GetRowCount(where);
            return count;
        }
        #endregion


        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.DZMembership.DomainModel.DZMembershipProvider");
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
            return repositoryDZMembership.GetAllCustomer(pageIndex, pageSize, out totalRecords);
        }

    }
}
