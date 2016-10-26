using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.Application
{
  public interface  IDZMembershipService
    {
        bool RegisterBusinessUser(string registerName, string password, out string errMsg);
        
           


    }
}
