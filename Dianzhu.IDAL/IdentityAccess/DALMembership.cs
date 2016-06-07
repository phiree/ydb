using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Dianzhu;
 
using System.Net.Mail;
namespace Dianzhu.IDAL
{
    public interface IDALMembership:IRepository<Model.DZMembership,Guid>
    {

         void CreateUser(Model.DZMembership user);

          /// <summary>
        ///ddd:domainservice
        ///通过名字获取列表 属于 领域服务.
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
          IList<Model.DZMembership> GetList(string[] userNames);
          Model.DZMembership ValidateUser(string username, string encryptedPassword);


          Model.DZMembership GetMemberByName(string username);




          Model.DZMembership GetMemberById(Guid memberId);
          Model.DZMembership GetMemberByEmail(string email);
          Model.DZMembership GetMemberByWechatOpenId(string openid);
          Model.DZMembership GetMemberByQQOpenId(string openid);

          Model.DZMembership GetMemberBySinaWeiboUid(long uid);
          Model.DZMembership GetMemberByPhone(string phone);

        
          IList<Model.DZMembership> GetAllUsers(int pageIndex, int pageSize, out long totalRecord);
        IList<Model.DZMembership> GetAllCustomer(int pageIndex, int pageSize, out long totalRecord);




        IList<Model.DZMembership> GetAll();
          Model.BusinessUser CreateBusinessUser(string username, string password, Model.Business business);
        
        
    }
}