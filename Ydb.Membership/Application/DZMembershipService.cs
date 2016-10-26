using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Membership.DomainModel.Repository;

namespace Ydb.Membership.Application
{
  public class DZMembershipService:IDZMembershipService
    {

        public DZMembershipService(IRepositoryDZMembership repositoryDZMembership,
        
         IRepositoryUserToken repositoryUserToken,
          DZMembershipProvider memberProvider)
        {
            this.repositoryDZMembership = repositoryDZMembership;
          
            this.repositoryUserToken = repositoryUserToken;
           
            this.memberProvider = memberProvider;
        }
        DZMembershipProvider memberProvider;
        IRepositoryDZMembership repositoryDZMembership = null;
        IEncryptService encryptService;
       
        IRepositoryUserToken repositoryUserToken;

        public bool RegisterBusinessUser( string registerName, string password, out string errMsg)
        {
            errMsg = string.Empty;
            Guid memberId= memberProvider.CreateUser(registerName, password, UserType.business, out errMsg);
            return string.IsNullOrEmpty(errMsg);

        }
        public DZMembership GetUserByName(string userName)
        {
            return memberProvider.GetUserByName(userName);
        }
           


    }
}
