using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.DomainModel
{
    public class CountServiceFee_Alipay: ICountServiceFee
    {
        /// <summary>
        /// 提现手续费计算
        /// </summary>
        /// <param name="amount" type="decimal">提现金额</param>
        /// <param name="rate" type="string">提现费率</param
        public decimal CountServiceFee(decimal amount,string rate)
        {
            //该手续费计算规则：每笔费率0.5%，最低1元，最高25元
            decimal fee = amount * 0.005m;
            if (fee < 1)
            {
                fee = 1;
            }
            if (fee > 25)
            {
                fee = 25;
            }
            return fee;
        }
    }
}
