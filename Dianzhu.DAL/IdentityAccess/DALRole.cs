using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.IdentityAccess
{
   public   class DALRole:DALBase<Model.DZRole>
    {

        public DZRole GetByName(string roleName)
        {
            var query = Session.QueryOver<DZRole>().Where(x => x.Name == roleName);
         return    GetOneByQuery(query);
        }
        public void CreateRole(string roleName)
        {
            DZRole role = new DZRole();
            role.Name = roleName;
            
            Session.Save(role);
        }
        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            DZRole role = new DZRole();
            role.Name = roleName;
            Session.Delete(role);
            return true;
        }
        public   bool RoleExists(string roleName)
        {
            var query = Session.QueryOver<DZRole>()
                 .Where(x => x.Name == roleName);
            var r = query.RowCount();
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
            var query = Session.QueryOver<DZRole>().Where(x => x.Name == roleName);
                DZRole role = GetOneByQuery(query);
                if (role != null)
                {
                    roles.Add(role);
                }
            }
            return roles;
        }
        public   string[] GetAllRoles()
        {
            return GetAll<DZRole>().Select(x=>x.Name).ToArray<string>();
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
            var r = query.Select(x=>x.Role).List<DZRole>();
            return r;
        }
        #endregion

    }
}
