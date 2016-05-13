using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.IDAL.IdentityAccess
{
   public   interface IDALRole {

          DZRole GetByName(string roleName);
          void CreateRole(string roleName);
          bool DeleteRole(string roleName, bool throwOnPopulatedRole);
          bool RoleExists(string roleName);


          void RemoveUsersFromRoles(string[] usernames, string[] roleNames);

          string[] GetUsersInRole(string roleName);
          IList<DZRole> GetList(string[] roleNames);
          string[] GetAllRoles();


          string[] FindUsersInRole(string roleName, string usernameToMatch);
        #region query
          bool IsUserInRole(string username, string roleName);
          IList<DZRole> GetRolesForUser(string username);
        #endregion

    }
}
