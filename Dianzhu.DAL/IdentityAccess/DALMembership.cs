using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Dianzhu;
 
using System.Net.Mail;
namespace Dianzhu.DAL
{
    public class DALMembership :DALBase<Model.DZMembership>
    {
        public DALMembership()
        {
             
        }
        //注入依赖,供测试使用;
    public DALMembership(string fortest):base(fortest)
        {
            
        }
         
        public void CreateUser(Model.DZMembership user)
        {
            Save(user);
        }

        public void CreateUpdateMember(Model.DZMembership member)
        {

            SaveOrUpdate(member);
             
        }
        /// <summary>
        ///ddd:domainservice
        ///通过名字获取列表 属于 领域服务.
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        public IList<Model.DZMembership> GetList(string[] userNames)
        {
            List<Model.DZMembership> members = new List<Model.DZMembership>();
            foreach (string username in userNames)
            {
                Model.DZMembership member = GetMemberByName(username);
                if (member != null)
                {
                    members.Add(member);
                }
            }
            return members;
        }
        public Model.DZMembership ValidateUser(string username, string password)
        {
               IQuery query = Session.CreateQuery("select u from DZMembership as u where u.UserName='" + username + "' and u.Password='" + password + "'");
                Model.DZMembership member = query.FutureValue<Model.DZMembership>().Value;
              return member;
        }


        public Model.DZMembership GetMemberByName(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            username = username.Replace("||", "@");
            Model.DZMembership user = null;
            Action a = () => {
                user = Session.QueryOver<Model.DZMembership>().Where(x=>x.UserName== username).FutureValue().Value;
               // user = query.UniqueResult<Model.DZMembership>();
            };
            TransactionCommit(a);
            return user;
           
        }

       

         
        public Model.DZMembership GetMemberById(Guid memberId)
        {
            Model.DZMembership member=null;
            IQuery query = Session.CreateQuery("select m from  DZMembership as m where Id='" + memberId + "'");

            Action a = () => { member = query.UniqueResult<Model.DZMembership>(); };
            TransactionCommit(a);
            return member;
        }
        public Model.DZMembership GetMemberByEmail(string email)
        {
          return  Session.QueryOver<Model.DZMembership>().Where(x => x.Email == email).SingleOrDefault();
        }
        public Model.DZMembership GetMemberByWechatOpenId(string openid)
        {
            return Session.QueryOver<Model.DZMembershipWeChat>().Where(x => x.OpenId == openid).SingleOrDefault();
        }
        public Model.DZMembership GetMemberByQQOpenId(string openid)
        {
            return Session.QueryOver<Model.DZMembershipQQ>().Where(x => x.OpenId == openid).SingleOrDefault();
        }
        public Model.DZMembership GetMemberBySinaWeiboUid(long uid)
        {
            return Session.QueryOver<Model.DZMembershipSinaWeibo>().Where(x => x.UId == uid).SingleOrDefault();
        }
        public Model.DZMembership GetMemberByPhone(string phone)
        { return Session.QueryOver<Model.DZMembership>().Where(x => x.Phone == phone).SingleOrDefault(); }

        public IList<Model.DZMembership> GetAllUsers()
        {
            IQuery query = Session.CreateQuery("select u from DZMembership u ");
            return query.List<Model.DZMembership>();
        }

        public IList<Model.DZMembership> GetAllUsers(int pageIndex, int pageSize, out long totalRecord)
        {
            IQuery qry = Session.CreateQuery("select u from DZMembership u order by u.TimeCreated desc");
            IQuery qryTotal = Session.CreateQuery("select count(*) from DZMembership u ");
            List<Model.DZMembership> memList = qry.List<Model.DZMembership>().Skip(pageIndex * pageSize).Take(pageSize).ToList();
            totalRecord = qryTotal.UniqueResult<long>();
            return memList;
        }

        public IList<Model.DZMembership> GetAllCustomer(int pageIndex, int pageSize, out long totalRecord)
        {
            return GetUserList(Model.Enums.enum_UserType.customer, pageIndex, pageSize, out totalRecord);
        }
        private IList<Model.DZMembership> GetUserList(Model.Enums.enum_UserType? userType,
            int pageIndex, int pageSize, out long totalRecord)
        {
            var query = Session.QueryOver<Model.DZMembership>().Where(x=>x.Id==x.Id);
            if (userType.HasValue)
            {
                query = query.And(x => x.UserType == userType.Value);
            }

            totalRecord = query.RowCount();
            return query.List().Skip(pageIndex*pageSize).Take(pageSize).ToList();
        }
        //public Model.ScenicAdmin GetScenicAdmin(Guid id)
        //{
        //    IQuery query = session.CreateQuery("select sa from ScenicAdmin sa where sa.Membership.Id='" + id + "'");
        //    IFutureValue<Model.ScenicAdmin> sa = query.FutureValue<Model.ScenicAdmin>();
        //    if (sa == null) return null;
        //    return sa.Value;
        //}





        public void ChangePassword(Model.DZMembership member)
        {
            using (var x = Session.Transaction)
            {
                x.Begin();
                Session.Update(member);
                x.Commit();
            }

        }


        public Model.BusinessUser GetBusinessUser(Guid id)
        {
            IQuery query = Session.CreateQuery("select m from  BusinessUser as m where Id='" + id + "'");
            Model.BusinessUser member = query.UniqueResult<Model.BusinessUser>();
            return member;
        }
        public IList<Model.DZMembership> GetAll()
        {
            return GetAll<Model.DZMembership>();
        }
        public Model.BusinessUser CreateBusinessUser(string username, string password, Model.Business business)
        {
            
            Model.BusinessUser member =  new Model.BusinessUser
            { UserName = username, Password = password, TimeCreated = DateTime.Now, BelongTo = business,
             LastLoginTime=DateTime.Now,
            RegisterValidateCode=Guid.NewGuid().ToString(),IsRegisterValidated=false};
            Save(member);

            //send validation email

            return member;
        }
        
        
    }
}