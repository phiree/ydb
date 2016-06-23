using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using AutoMapper.Mappers;
using AutoMapper;

namespace Dianzhu.BLL.IdentityAccess
{
    /// <summary>
    /// refactor: this is the application service in identityaccess context.
    /// </summary>
    public class RoleService:System.Web.Security.RoleProvider
    {

        DAL.IdentityAccess.DALRole dalRole;
        DAL.DALMembership dalMembership;
        DAL.IdentityAccess.DALRoleMember dalRoleMember;

        public RoleService(DAL.IdentityAccess.DALRole dalRole,DAL.DALMembership dalMembership)
        {
            this.dalRole = dalRole;
            this.dalMembership = dalMembership;
        }
        public RoleService() : this(new DAL.IdentityAccess.DALRole(),new DAL.DALMembership())
        { }

        public string GetRoleName(string id)
        {
            Guid guidId = new Guid(id);
            DZRole role = dalRole.FindById(guidId);
            return role.Name;
        }
        public IList<RoleDto> GetAllRolesDto()
        {
            IList<DZRole> all = dalRole.Find(x=>true);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DZRole, RoleDto>());
            var mapper = config.CreateMapper();
            IList<RoleDto> dtos = new List<RoleDto>();
            foreach( DZRole r in all)
            { 
            RoleDto dto = mapper.Map<DZRole, RoleDto>(r);
                dtos.Add(dto);
            }
            return dtos;

        }
        public void SaveOrUpdate(string id,string rolename)
        {
            Model.DZRole role;
            if (string.IsNullOrEmpty(id))
            {
                role = new DZRole { Name = rolename };
                dalRole.Add(role);
            }
            else
            {
                role = dalRole.FindById(new Guid(id));
                role.Name = rolename;
                dalRole.Update(role);
            }
        }
         
        public override string ApplicationName
        {
            get
            {
                return string.Empty;
            }

            set
            {
                throw new NotImplementedException();
            }
        }
 
        

        public override bool IsUserInRole(string username, string roleName)
        {

            return dalRole.IsUserInRole(username, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return dalRole.GetRolesForUser(username).Select(x => x.Name).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            dalRole.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
          return dalRole.DeleteRole(roleName,throwOnPopulatedRole);
        }

        public override bool RoleExists(string roleName)
        {
            return dalRole.RoleExists(roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            IList<DZMembership> members = dalMembership.GetList(usernames);
            IList<DZRole> roles = dalRole.GetList(roleNames);
           
            dalRoleMember.AddUsersToRoles(members,roles);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            IList<DZMembership> members = dalMembership.GetList(usernames);
            IList<DZRole> roles = dalRole.GetList(roleNames);

            dalRoleMember.RemoveUsersFromRoles(members, roles);
        }

        public override string[] GetUsersInRole(string roleName)
        {
          return   dalRoleMember.GetUsersInRole(roleName);
        }

        public override string[] GetAllRoles()
        {
            return dalRole.GetAllRoles();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
    }
}
