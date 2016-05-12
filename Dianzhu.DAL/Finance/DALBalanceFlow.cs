using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
namespace Dianzhu.DAL.Finance
{
   public  class DALBalanceFlow:DALBase<Model.Finance.BalanceFlow>
    {
        
        public DALBalanceFlow(string fortest) : base(fortest) { }
        public DALBalanceFlow() { }

        public void Save(Model.Finance.BalanceFlow flow)
        {
            Session.Save(flow);
        }
    }
}
