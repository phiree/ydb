using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public interface IWithdrawCashService
    {
        /// <summary>
        /// 提现操作
        /// </summary>
        /// <param name="order" type="IList<WithdrawCashParam>">提现信息列表</param>
        void WithdrawCash(IList<WithdrawCashParam> cashList);
    }
}
