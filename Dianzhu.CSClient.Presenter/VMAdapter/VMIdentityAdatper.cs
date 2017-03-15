using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dianzhu.CSClient.ViewModel;
using Ydb.Order.DomainModel;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
    public class VMIdentityAdatper : IVMIdentityAdapter
    {
        public VMIdentity OrderToVMIdentity(ServiceOrder order, string customerAvatarUrl)
        {
            return new VMIdentity(order.Id.ToString(), order.CustomerId, order.CustomerId, customerAvatarUrl);
        }
    }
}
