using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model.Finance;

namespace Dianzhu.BLL.Finance
{
    public class BalanceFlowService : IBalanceFlowService
    {
        DAL.Finance.DALBalanceFlow dalBalanceFlow = new DAL.Finance.DALBalanceFlow();
        
        public void Save(BalanceFlow flow)
        {
            dalBalanceFlow.Save(flow);
        }
    }
}
