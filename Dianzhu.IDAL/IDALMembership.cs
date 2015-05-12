using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu;

namespace Dianzhu.IDAL
{
    public interface IDALMembership
    {
        #region for membership provier;
        IDALBase<Model.DZMembership> DalBase { get; set; }
        //创建用户
        void CreateUser(Model.DZMembership user);

        //验证用户
        bool ValidateUser(string username, string password);

        //创建更新membership
        void CreateUpdateMember(Model.DZMembership member);

        //获得所有的membership
        IList<Model.DZMembership> GetAllUsers();

        //分页获得所有的membership
        IList<Model.DZMembership> GetAllUsers(int pageIndex, int pageSize, out long totalRecord);

        //通过usertype获得membership

        //通过username获取membership
        Model.DZMembership GetMemberByName(string username);
 
        //通过guid获取membership
        Model.DZMembership GetMemberById(Guid memberId);


        //更改密码
        void ChangePassword(Model.DZMembership member);
        //更改信息

        #endregion 
        #region additional method
        Model.BusinessUser GetBusinessUser(Guid id);
        #endregion
    }
}