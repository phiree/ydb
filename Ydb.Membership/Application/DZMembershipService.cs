using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Common.Repository;
namespace Ydb.Membership.Application
{
  public class DZMembershipService:IDZMembershipService
    {
        IDZMembershipDomainService dzmembershipDomainService;
        public DZMembershipService(IDZMembershipDomainService dzmembershipDomainService)
        {
            this.dzmembershipDomainService = dzmembershipDomainService;
 
        }
        

        [UnitOfWork]
        public bool RegisterBusinessUser( string registerName, string password, out string errMsg)
        {

            errMsg = string.Empty;
            Guid memberId= dzmembershipDomainService.CreateUser(registerName, password, UserType.business, out errMsg);
            return string.IsNullOrEmpty(errMsg);

        }
        [UnitOfWork]
        public DZMembership GetUserByName(string userName)
        {
            return dzmembershipDomainService.GetUserByName(userName);
        }
           


    }
}
