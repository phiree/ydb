using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
namespace Dianzhu.IDAL
{
    /// <summary>
    /// 支付记录持久化接口.
    /// </summary>
    public interface IDALPaylog
    {
        void Save(Paylog paylog);
        IList<Paylog> GetList(ServiceOrder order);
    }
}
