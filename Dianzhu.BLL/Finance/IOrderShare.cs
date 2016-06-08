using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Finance
{
    /// <summary>
    /// 订单分账
    /// </summary>
    public interface IOrderShare
    {
        IList<Dianzhu.Model.Finance.BalanceFlow> Share(Dianzhu.Model.ServiceOrder order);
        void ShareOrder(Dianzhu.Model.ServiceOrder order);
    }
}
