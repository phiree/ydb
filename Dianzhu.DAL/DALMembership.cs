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
    { public DALMembership()
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

        public bool ValidateUser(string username, string password)
        {
            //  User user = session.QueryOver<User>(x=>x.User);
            bool result = false;
            IQuery query = Session.CreateQuery("select u from DZMembership as u where u.UserName='" + username + "' and u.Password='" + password + "'");
            int matchLength = query.Future<Model.DZMembership>().ToArray().Length;

            if (matchLength == 1) { result = true;

            IQuery queryUpdate = Session.CreateQuery("update DZMembership u  set u.LastLoginTime='" + DateTime.Now.ToString() + "'   where u.UserName='" + username + "' and u.Password='" + password + "'");
           // queryUpdate.ExecuteUpdate();
            }
            if (matchLength > 1)
            {
                throw new Exception("账户重名,拒绝登录");
            }

            return result;
        }

        public Model.DZMembership GetMemberByName(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            username = username.Replace("||", "@");
            IQuery query =Session.CreateQuery("select m from  DZMembership as m where UserName='" + username + "'");
            var temp = query.FutureValue<Model.DZMembership>();
            var user = temp.Value;
            return user;
        }

         
        public Model.DZMembership GetMemberById(Guid memberId)
        {
            IQuery query = Session.CreateQuery("select m from  DZMembership as m where Id='" + memberId + "'");
            Model.DZMembership member = query.FutureValue<Model.DZMembership>().Value;
            return member;
        }
        public Model.DZMembership GetMemberByEmail(string email)
        {
          return  Session.QueryOver<Model.DZMembership>().Where(x => x.Email == email).SingleOrDefault();
        }
        public Model.DZMembership GetMemberByPhone(string phone)
        { return Session.QueryOver<Model.DZMembership>().Where(x => x.Phone == phone).SingleOrDefault(); }

        public IList<Model.DZMembership> GetAllUsers()
        {
            IQuery query = Session.CreateQuery("select u from DZMembership u ");
            return query.Future<Model.DZMembership>().ToList();
        }

        public IList<Model.DZMembership> GetAllUsers(int pageIndex, int pageSize, out long totalRecord)
        {
            IQuery qry = Session.CreateQuery("select u from DZMembership u order by u.TimeCreated desc");
            IQuery qryTotal = Session.CreateQuery("select count(*) from DZMembership u ");
            List<Model.DZMembership> memList = qry.Future<Model.DZMembership>().Skip(pageIndex * pageSize).Take(pageSize).ToList();
            totalRecord = qryTotal.FutureValue<long>().Value;
            return memList;
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
            Model.BusinessUser member = query.FutureValue<Model.BusinessUser>().Value;
            return member;
        }
        public IList<Model.DZMembership> GetAll()
        {
            return GetAll<Model.DZMembership>();
        }
        public Model.BusinessUser CreateBusinessUser(string username, string password, Model.Business business)
        {
            
            Model.BusinessUser member = new Model.BusinessUser
            { UserName = username, Password = password, TimeCreated = DateTime.Now, BelongTo = business,
             LastLoginTime=DateTime.Now,
            RegisterValidateCode=Guid.NewGuid().ToString(),IsRegisterValidated=false};
            Save(member);

            //send validation email

            return member;
        }
        
        
    }
}