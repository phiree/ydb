using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Common;

namespace Ydb.Membership.Application
{
    public interface IMembershipLoginLogService
    {
        void MemberLogin(string memeberId, string memo, enum_appName appName);
        void MemberLogoff(string memberId, string memo, enum_appName appName);
    }
}
