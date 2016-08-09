using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.IdentityAccess
{
   public   class DALRoleMember:NHRepositoryBase<RoleMember,Guid>,IDAL.IDALRoleMember
    {
        public void AddUsersToRoles(IList<DZMembership> members, IList<DZRole> roles)
        {
            foreach (DZMembership member in members)
            {
                foreach (DZRole role in roles)
                {
                    RoleMember rm = new RoleMember
                    {
                        Member = member,
                        Role = role
                    };
                    Add(rm);
                }
            }
        }

        public void RemoveUsersFromRoles(IList<DZMembership> members, IList<DZRole> roles)
        {
            foreach (DZMembership member in members)
            {
                foreach (DZRole role in roles)
                {
                    RoleMember rm = FindOne(x => x.Member.Id == member.Id && x.Role.Id == role.Id);
                    if (rm != null)
                    {
                        Delete(rm);
                    }
                }
            }
        }

        public string[] GetUsersInRole(string roleName)
        {
            //var query = Session.QueryOver<RoleMember>().Where(x => x.Role.Name == roleName);
            //return query.Select(x => x.Member.UserName).List<string>().ToArray();
            return Find(x => x.Role.Name == roleName).Select(x => x.Member.UserName).ToList().ToArray();
        }

       
        public   string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        #region query
        public bool IsUserInRole(string username, string roleName)
        {
            var aa = GetRowCount(x => x.Role.Name == roleName && x.Member.UserName == username);
            if (aa > 1)
            {
                throw new Exception("重复的用户-角色对应数据");
            }
            return aa == 1;
        }
        public IList<DZRole> GetRolesForUser(string username)
        {
            //var query = Session.QueryOver<RoleMember>()
            //    .Where(x => x.Member.UserName == username);
            //var r = query.Select(x=>x.Role).List<DZRole>();
            return Find(x => x.Member.UserName == username).Select(x => x.Role).ToList();

        }
        #endregion

    }
}
