using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
namespace Ydb.Membership.Application
{
  public interface  IDZMembershipService
    {
        bool RegisterBusinessUser(string registerName, string password, out string errMsg);
        DZMembership GetUserByName(string userName);




    }
}
