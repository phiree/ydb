using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel
{
   public  class LoginNameDetermine:ILoginNameDetermine
    {
        public Enums.LoginNameType Determin(string loginName)
        {
            if (Regex.IsMatch(loginName, @".+@.+\..+"))
                 { return Enums.LoginNameType.Email; }
            else if (Regex.IsMatch(loginName, @"d{11}"))
            {
                return Enums.LoginNameType.PhoneNumber;
            }
            return Enums.LoginNameType.NormalString;
        }
    }
}
