using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Order.Application;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.ApplicationService.Application.AgentService
{
    public class OrdersService: IOrdersService
    {
        IBusinessService businessService;
        IServiceOrderService serviceOrderService;
        public OrdersService(IBusinessService businessService,
            IServiceOrderService serviceOrderService)
        {
            this.businessService = businessService;
            this.serviceOrderService = serviceOrderService;
        }
        public long GetOrdersCountByArea(IList<string> areaIdList,bool isSharea)
        {
            IList<Business> businessList = businessService.GetAllBusinessesByArea(areaIdList);
            IList<string> businessIdList= businessList.Select(x => x.Id.ToString()).ToList();
            return serviceOrderService.GetOrdersCountByBusinessList(businessIdList, isSharea);
        }
    }
}
