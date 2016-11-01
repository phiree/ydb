using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel.Enums
{
   public enum LoginNameType
    {
        NormalString=0,//无特征的字符串
        PhoneNumber=1,//电话号码
        Email=2,//是email

    }
}
