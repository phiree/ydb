using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Dianzhu;

using System.Net.Mail;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System.Linq.Expressions;

namespace Dianzhu.DAL
{
    public class DALMembership : NHRepositoryBase<Model.DZMembership, Guid>, IDAL.IDALMembership// DALBase<Model.DZMembership>
    {


        public void CreateUser(Model.DZMembership user)
        {
            Add(user);
        }

        /// <summary>
        ///ddd:domainservice
        ///通过名字获取列表 属于 应用服务.
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
            //IQuery query =   Session.CreateQuery("select u from DZMembership as u where u.UserName='" + username + "' and u.Password='" + password + "'");
            // Model.DZMembership member = query.FutureValue<Model.DZMembership>().Value;

            var test = FindOne(x => ((DZMembershipQQ)x).OpenId == "4298A4AF0296BB6DE817D54DBE7FD20F");
            var member = FindOne(x => x.UserName == username && x.Password == password);
            return member;
        }


        public Model.DZMembership GetMemberByName(string username)
        {

            var user = FindOne(x => x.UserName == username);
            //  TransactionCommit(a);
            return user;

        }




        public Model.DZMembership GetMemberById(Guid memberId)
        {
            var member = FindOne(x => x.Id == memberId);
            return member;
        }
        public Model.DZMembership GetMemberByEmail(string email)
        {
            //return  Session.QueryOver<Model.DZMembership>().Where(x => x.Email == email).SingleOrDefault();
            var member = FindOne(x => x.Email == email);
            return member;
        }
        public Model.DZMembership GetMemberByWechatOpenId(string openid)
        {
            // return Session.QueryOver<Model.DZMembershipWeChat>().Where(x => x.OpenId == openid).SingleOrDefault();
            var member = FindOne(x => ((Dianzhu.Model.DZMembershipWeChat)x).OpenId == openid);
            return member;
        }
        public Model.DZMembership GetMemberByQQOpenId(string openid)
        {
            //  return Session.QueryOver<Model.DZMembershipQQ>().Where(x => x.OpenId == openid).SingleOrDefault();
            var member = FindOne(x => ((Dianzhu.Model.DZMembershipQQ)x).OpenId == openid);
            return member;
        }
        public Model.DZMembership GetMemberBySinaWeiboUid(long uid)
        {
            //return Session.QueryOver<Model.DZMembershipSinaWeibo>().Where(x => x.UId == uid).SingleOrDefault();
            var member = FindOne(x => ((Dianzhu.Model.DZMembershipSinaWeibo)x).UId == uid);
            return member;
        }
        public Model.DZMembership GetMemberByPhone(string phone)
        { return FindOne(x => x.Phone == phone); }


        public IList<Model.DZMembership> GetAllUsers(int pageIndex, int pageSize, out long totalRecord)
        {

            Expression<Func<Model.DZMembership, bool>> where = i => true;
            var result = Find(where, pageIndex, pageSize, out totalRecord).ToList();
            return result;
        }
        public IList<DZMembership> GetAllCustomer(int pageIndex, int pageSize, out long totalRecords)
        {
            Expression<Func<Model.DZMembership, bool>> where = i => i.UserType== enum_UserType.customer;
            var result = Find(where, pageIndex, pageSize, out totalRecords).ToList();
            return result;
        }


        public IList<Model.DZMembership> GetAll()
        {
            return Find(x => true).ToList();
        }
        public Model.BusinessUser CreateBusinessUser(string username, string password, Model.Business business)
        {

            Model.BusinessUser member = new Model.BusinessUser
            {
                UserName = username,
                Password = password,
                TimeCreated = DateTime.Now,
                BelongTo = business,
                LastLoginTime = DateTime.Now,
                RegisterValidateCode = Guid.NewGuid().ToString(),
                IsRegisterValidated = false
            };
            Add(member);

            //send validation email

            return member;
        }
 
    }
}