using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Common.Repository;
using Ydb.Membership.DomainModel.Repository;
using NHibernate;
using System.Linq.Expressions;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate
{
   public class RepositoryDZMembership:NHRepositoryBase<DZMembership,Guid>,IRepositoryDZMembership
    {
       
        public void CreateUser(DZMembership user)
        {
            Add(user);
        }

        /// <summary>
        ///ddd:domainservice
        ///通过名字获取列表 属于 应用服务.
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        public IList<DZMembership> GetList(string[] userNames)
        {
            List<DZMembership> members = new List<DZMembership>();
            foreach (string username in userNames)
            {
                DZMembership member = GetMemberByName(username);
                if (member != null)
                {
                    members.Add(member);
                }
            }
            return members;
        }
        public DZMembership ValidateUser(string username, string password)
        {
            //IQuery query =   Session.CreateQuery("select u from DZMembership as u where u.UserName='" + username + "' and u.Password='" + password + "'");
            // DZMembership member = query.FutureValue<DZMembership>().Value;

            //var test = FindOne(x => ((DZMembershipQQ)x).OpenId == "4298A4AF0296BB6DE817D54DBE7FD20F");
            var member = FindOne(x => (x.UserName == username || x.Id.ToString() == username) && x.Password == password);
            //var member = FindOne(x => x.UserName == username  && x.Password == password);
            return member;
        }


        public DZMembership GetMemberByName(string username)
        {

            var user = FindOne(x => x.UserName == username);
            //  TransactionCommit(a);
            return user;

        }
  
        public DZMembership GetMemberById(Guid memberId)
        {
            var member = FindOne(x => x.Id == memberId);
            return member;
        }
        public DZMembership GetMemberByEmail(string email)
        {
            //return  Session.QueryOver<DZMembership>().Where(x => x.Email == email).SingleOrDefault();
            var member = FindOne(x => x.Email == email);
            return member;
        }
        public DZMembership GetMemberByWechatOpenId(string openid)
        {
            // return Session.QueryOver<DZMembershipWeChat>().Where(x => x.OpenId == openid).SingleOrDefault();
            var member = FindOne(x => (( DZMembershipWeChat)x).WeChatOpenId == openid);
            return member;
        }
        public DZMembership GetMemberByQQOpenId(string openid)
        {
            //  return Session.QueryOver<DZMembershipQQ>().Where(x => x.OpenId == openid).SingleOrDefault();
            var member = FindOne(x => (( DZMembershipQQ)x).OpenId == openid);
            return member;
        }
        public DZMembership GetMemberBySinaWeiboUid(long uid)
        {
            //return Session.QueryOver<DZMembershipSinaWeibo>().Where(x => x.UId == uid).SingleOrDefault();
            var member = FindOne(x => (( DZMembershipSinaWeibo)x).UId == uid);
            return member;
        }
        public DZMembership GetMemberByPhone(string phone)
        { return FindOne(x => x.Phone == phone); }


        public IList<DZMembership> GetAllUsers(int pageIndex, int pageSize, out long totalRecord)
        {

            Expression<Func<DZMembership, bool>> where = i => true;
            var result = Find(where, pageIndex, pageSize, out totalRecord).ToList();
            return result;
        }
        public IList<DZMembership> GetAllCustomer(int pageIndex, int pageSize, out long totalRecords)
        {
            Expression<Func<DZMembership, bool>> where = i => i.UserType ==  UserType.customer;
            var result = Find(where, pageIndex, pageSize, out totalRecords).ToList();
            return result;
        }


        public IList<DZMembership> GetAll()
        {
            return Find(x => true).ToList();
        }
       
    }
}
