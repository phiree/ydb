using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
namespace Ydb.Membership.Application
{
  public interface  IDZMembershipService
    {
        Dto.RegisterResult RegisterBusinessUser(string registerName, string password,string confirmPassword );
        Dto.MemberDto GetUserByName(string userName);

        Dto.LoginResult  Login(string userName, string password,UserType userType);



    }
}
