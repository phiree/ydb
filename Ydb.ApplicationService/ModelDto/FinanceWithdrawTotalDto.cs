using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.ApplicationService.ModelDto
{
    public class FinanceWithdrawTotalDto
    {
        public decimal WithdrawTotal { get; set; }

        public long WithdrawNotDeal { get; set; }

        public IList<FinanceWithdrawDto> financeWithdrawDtoList { get; set; }

    }
}
