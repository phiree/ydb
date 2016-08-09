using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
namespace Dianzhu.DAL.Finance
{
   public  class DALBalanceFlow:NHRepositoryBase<Model.Finance.BalanceFlow,Guid>, IDAL.Finance.IDALBalanceFlow
    {
        
       

        public void Save(Model.Finance.BalanceFlow flow)
        {
            Add(flow);
        }
    }
}
