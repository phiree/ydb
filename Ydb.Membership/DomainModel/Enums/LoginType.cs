using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel.Enums
{
    public enum LoginType
    {
        original = 0,
        //第三方登录用户
        WeChat = 1,
        SinaWeiBo = 2,
        TencentQQ = 3,
    }
}
