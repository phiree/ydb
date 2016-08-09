using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.IdentityAccess
{
   public   class DALRole:NHRepositoryBase<DZRole,Guid>,IDAL.IDALRole
    {

        public DZRole GetByName(string roleName)
        {
            return FindOne(x => x.Name == roleName);
        }
        public void CreateRole(string roleName)
        {
            DZRole role = new DZRole();
            role.Name = roleName;
            Add(role);
        }
        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            DZRole role = new DZRole();
            role.Name = roleName;
            Delete(role);
            return true;
        }
        public   bool RoleExists(string roleName)
        {
            var r = GetRowCount(x => x.Name == roleName);
            return r >= 1;
        }
      
        public   void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public   string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public IList<DZRole> GetList(string[] roleNames)
        {
            List<DZRole> roles = new List<DZRole>();
            foreach(string roleName in roleNames)
            {
                DZRole role = FindOne(x => x.Name == roleName);
                if (role != null)
                {
                    roles.Add(role);
                }
            }
            return roles;
        }
        public string[] GetAllRoles()
        {
            return Find(x=>true).Select(x=>x.Name).ToArray<string>();
        }        

        public   string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        #region query
        public bool IsUserInRole(string username, string roleName)
        {
            
                var query = Session.QueryOver<RoleMember>()
                .Where(x => x.Role.Name == roleName)
                .And(x => x.Member.UserName == username);
                var aa = query.RowCount();
                if (aa > 1)
                {
                    throw new Exception("重复的用户-角色对应数据");
                }
               
                return aa == 1;
             
        }
        public IList<DZRole> GetRolesForUser(string username)
        {
           
                var query = Session.QueryOver<RoleMember>()
                .Where(x => x.Member.UserName == username);
                var r = query.Select(x => x.Role).List<DZRole>();
               
                return r;
            
        }
        #endregion

    }
}
