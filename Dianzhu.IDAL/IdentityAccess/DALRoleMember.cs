using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
namespace Dianzhu.IDAL
{
   public   interface IDALRoleMember :IRepository<RoleMember,Guid>
    {


         void AddUsersToRoles(IList<DZMembership> members, IList<DZRole> roles);

         void RemoveUsersFromRoles(IList<DZMembership> members, IList<DZRole> roles);

         string[] GetUsersInRole(string roleName);


         string[] FindUsersInRole(string roleName, string usernameToMatch);
        #region query
         bool IsUserInRole(string username, string roleName);
         IList<DZRole> GetRolesForUser(string username);
        #endregion

    }
}
