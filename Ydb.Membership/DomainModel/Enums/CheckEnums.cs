using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel.Enums
{
    public class CheckEnums
    {
        public static UserType CheckUserType(string strUserType)
        {
            UserType userType;
            if (!Enum.TryParse<UserType>(strUserType, out userType))
            {
                throw new FormatException("用户类型错误！");
            }
            return userType;
        }
    }
}
