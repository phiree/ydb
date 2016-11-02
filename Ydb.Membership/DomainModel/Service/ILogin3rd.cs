using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel.Service
{
   public  interface ILogin3rd
    {
        DZMembership Login(string platform, string code, string appName, string userType);
    }
}
