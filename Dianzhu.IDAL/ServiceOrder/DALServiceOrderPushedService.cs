using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.DAL
{

    public interface IDALServiceOrderPushedService

    {
        IList<ServiceOrderPushedService> FindByOrder(ServiceOrder order);
    }
}
