using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.CSClient.ViewModel;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
    public class VMIdentityAdatper : IVMIdentityAdapter
    {
        public VMIdentity OrderToVMIdentity(ServiceOrder order, string customerAvatarUrl)
        {
            return new VMIdentity(order.Id,new Guid( order.CustomerId), order.CustomerId, customerAvatarUrl);
        }
    }
}
