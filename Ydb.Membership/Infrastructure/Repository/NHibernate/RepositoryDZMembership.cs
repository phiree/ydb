using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
 
using Ydb.Membership.DomainModel.Repository;
using NHibernate;
using System.Linq.Expressions;
using Ydb.Common.Repository;
using Ydb.Common.Specification;
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
        public IList<DZMembership> GetUsers(TraitFilter filter, string name, string email, string phone, LoginType loginType, UserType userType)
        {
            var where = Ydb.Common.Specification.PredicateBuilder.True<DZMembership>();
            //where = where.And(x => x.UserType == (UserType)Enum.Parse(typeof(UserType), userType));
            where = where.And(x => x.UserType == userType);
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
            if (loginType != LoginType.None)
            {
                where = where.And(x => x.LoginType ==  loginType);
            }
            DZMembership baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone =  FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() 
                :  Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }

        public IList<DZMembership> GetUsersByArea(IList<string> areaList, DateTime beginTime, DateTime endTime, UserType userType)
        {
            var where = Ydb.Common.Specification.PredicateBuilder.True<DZMembership>();
            //if (!string.IsNullOrEmpty(userType))
            //{
            //    where = where.And(x => x.UserType == (UserType)Enum.Parse(typeof(UserType), userType));
            //}
            where = where.And(x => x.UserType ==userType);
            if (areaList.Count>0)
            {
                where = where.And(x => areaList.Contains(x.AreaId));
            }
            if (beginTime != DateTime.MinValue)
            {
                where = where.And(x => x.TimeCreated >= beginTime);
            }
            if (endTime != DateTime.MinValue)
            {
                where = where.And(x => x.TimeCreated < endTime);
            }
            //long t = 0;
            //var list = filter.pageSize == 0 ? Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList()
            //    : Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return Find(where).ToList();
        }

        public long GetUsersCount(string name, string email, string phone, LoginType loginType, UserType userType)
        {
            var where = Ydb.Common.Specification.PredicateBuilder.True<DZMembership>();
            //if (!string.IsNullOrEmpty(userType))
            //{
            //    where = where.And(x => x.UserType == (UserType)Enum.Parse(typeof(UserType), userType));
            //}

            where = where.And(x => x.UserType ==userType);
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
            if (loginType!=LoginType.None)
            {
                where = where.And(x => x.LoginType == loginType);
            }
            long count = GetRowCount(where);
            return count;
        }

        public long GetUsersCountByArea(IList<string> areaList,DateTime beginTime,DateTime endTime, UserType userType)
        {
            //session.CreateQuery
            var where = Ydb.Common.Specification.PredicateBuilder.True<DZMembership>();
           
            where = where.And(x => x.UserType == userType);
            
            if (areaList.Count >0)
            {
                where = where.And(x => areaList.Contains(x.AreaId));
            }
            if (beginTime != DateTime.MinValue)
            {
                where = where.And(x => x.TimeCreated >= beginTime);
            }
            if (endTime != DateTime.MinValue)
            {
                where = where.And(x => x.TimeCreated < endTime);
            }
            long count = GetRowCount(where);
            return count;
        }

        public IList<DZMembership> GetAll()
        {
            return Find(x => true).ToList();
        }

        public DZMembershipCustomerService GetOneNotVerifiedDZMembershipCustomerServiceByArea(IList<string> areaList)
        {
            var where = Ydb.Common.Specification.PredicateBuilder.True<DZMembership>();
            where = where.And(x => x.UserType == UserType.customerservice && x is DZMembershipCustomerService && areaList.Contains(x.AreaId));
            long totalRecord = 0;
            return  (DZMembershipCustomerService)Find(where,1,1,out totalRecord).ToList()[0];
        }

    }
}
