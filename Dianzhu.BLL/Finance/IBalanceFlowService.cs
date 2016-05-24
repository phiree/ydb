using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Finance
{
    public interface IBalanceFlowService
    {
        void Save(Dianzhu.Model.Finance.BalanceFlow flow);
    }
}
