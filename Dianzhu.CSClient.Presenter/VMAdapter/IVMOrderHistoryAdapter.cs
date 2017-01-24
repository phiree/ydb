using Dianzhu.CSClient.ViewModel;
using Ydb.Order.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
    public interface IVMOrderHistoryAdapter
    {
        VMOrderHistory OrderToVMOrderHistory(ServiceOrder order);
    }
}
