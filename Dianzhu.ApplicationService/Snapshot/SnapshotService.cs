using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
 
using Dianzhu.ApplicationService.Order;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
namespace Dianzhu.ApplicationService.Snapshot
{
    public class SnapshotService: ISnapshotService
    {
        IServiceOrderService ibllserviceorder;
         IDZServiceService dzServiceService;
       IServiceOrderStateChangeHisService bllstatehis;
        Order.IOrderService orderService;
        public SnapshotService(IServiceOrderService ibllserviceorder, IDZServiceService dzServiceService, 
            IServiceOrderStateChangeHisService bllstatehis,IOrderService orderService)
        {
            this.ibllserviceorder = ibllserviceorder;
            this.dzServiceService = dzServiceService;
            this.bllstatehis = bllstatehis;
            this.orderService = orderService;
        }

        /// <summary>
        /// 查询快照
        /// </summary>
        /// <param name="ServiceID"></param>
        /// <param name="filter"></param>
        /// <param name="sna"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<snapshortsObj> GetSnapshots(string ServiceID, common_Trait_Filtering filter, common_Trait_SnapshotFiltering sna, Customer customer)
        {
            DateTime StartTime = utils.CheckDateTime(sna.startTime, "yyyyMMdd", "SnapshotFiltering.startTime");
            DateTime EndTime = utils.CheckDateTime(sna.endTime, "yyyyMMdd", "SnapshotFiltering.endTime");
            if (StartTime > EndTime)
            {
                throw new Exception("startTime不得大于endTime!");
            }
            //Customer customer = new Customer();
            //customer = customer.getCustomer(headers.token, headers.apiKey, false);
            //if (customer.UserType != "business")
            //{
            //    throw new Exception("没有访问权限！");
            //}
            Guid guidService = utils.CheckGuidID(ServiceID, "ServiceID");
            TraitFilter filter1 = utils.CheckFilter(filter, "Snapshots");
            ServiceDto dzService = dzServiceService.GetOne(guidService);
            if (dzService.ServiceBusinessOwnerId!= customer.UserID)
            {
                throw new Exception("你的店铺没有该项服务！");
            }
            IList<snapshortsObj> snapshortsobj = new List<snapshortsObj>();
            IList<ServiceOrder> orderList = ibllserviceorder.GetOrderListOfServiceByDateRange(guidService, StartTime, EndTime);
            snapshortsobj = new snapshortsObj().Adap(orderList, bllstatehis,orderService,dzServiceService);
            return snapshortsobj;
        }

    }
}
