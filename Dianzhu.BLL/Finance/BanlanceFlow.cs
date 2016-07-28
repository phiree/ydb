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
        IDAL.Finance.IDALBalanceFlow dalBalanceFlow;

        public BalanceFlowService(IDAL.Finance.IDALBalanceFlow dalBalanceFlow)
        {
            this.dalBalanceFlow = dalBalanceFlow;
        }

        public IList<BalanceFlow> GetList()
        {
          return  dalBalanceFlow.Find(x => true);
        }

        public void Save(BalanceFlow flow)
        {
            dalBalanceFlow.Add(flow);
        }
    }
}
