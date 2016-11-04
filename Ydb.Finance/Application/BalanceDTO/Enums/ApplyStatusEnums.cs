using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public enum ApplyStatusEnums
    {
        None=0,//初始状态
        ApplyWithdraw=1,//申请提现
        ApplyCancel = 2,//申请取消
        PaySeccuss = 3,//付款成功
        Payfail = 4,//付款失败
    }
}
