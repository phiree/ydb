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
        void Share(Dianzhu.Model.ServiceOrder order);
    }
}
