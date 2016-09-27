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
        public VMIdentity OrderToIdentity(ServiceOrder order, string customerAvatarUrl)
        {
            return new VMIdentity(order.Id, order.Customer.Id, order.Customer.DisplayName, customerAvatarUrl);
        }
    }
}
