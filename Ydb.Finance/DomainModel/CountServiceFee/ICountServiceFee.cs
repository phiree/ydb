using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.DomainModel
{
    public interface ICountServiceFee
    {
        /// <summary>
        /// 提现手续费计算
        /// </summary>
        /// <param name="amount" type="decimal">提现金额</param>
        /// <param name="rate" type="string">提现费率</param
        decimal CountServiceFee(decimal amount, string rate);
    }
}
