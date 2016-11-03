using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Common.Repository;
namespace Ydb.Membership.DomainModel.Repository
{
   public interface IRepositoryDZMembership:IRepository<DZMembership,Guid>
    {
        void CreateUser( DZMembership user);

        /// <summary>
        ///ddd:domainservice
        ///通过名字获取列表 属于 领域服务.
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        IList< DZMembership> GetList(string[] userNames);
      DZMembership ValidateUser(string username, string encryptedPassword);


       DZMembership GetMemberByName(string username);




        DZMembership GetMemberById(Guid memberId);
        DZMembership GetMemberByEmail(string email);
      DZMembership GetMemberByWechatOpenId(string openid);
      DZMembership GetMemberByQQOpenId(string openid);

         DZMembership GetMemberBySinaWeiboUid(long uid);
      DZMembership GetMemberByPhone(string phone);


        IList< DZMembership> GetAllUsers(int pageIndex, int pageSize, out long totalRecord);
        IList< DZMembership> GetAllCustomer(int pageIndex, int pageSize, out long totalRecord);




        IList< DZMembership> GetAll();
        

    }
}
