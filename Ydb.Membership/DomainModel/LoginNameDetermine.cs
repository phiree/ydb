using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel
{
   public  interface ILoginNameDetermine
    {
        Enums.LoginNameType Determin(string loginName);
    }
}
