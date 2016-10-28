using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
using AutoMapper;
using Dianzhu.Model;

namespace Dianzhu.ApplicationService.Snapshot
{
    public class SnapshotService: ISnapshotService
    {
        BLL.IBLLServiceOrder ibllserviceorder;
        BLL.BLLDZService blldzservice;
        BLL.BLLServiceOrderStateChangeHis bllstatehis;

        public SnapshotService(BLL.IBLLServiceOrder ibllserviceorder, BLL.BLLDZService blldzservice, BLL.BLLServiceOrderStateChangeHis bllstatehis)
        {
            this.ibllserviceorder = ibllserviceorder;
            this.blldzservice = blldzservice;
            this.bllstatehis = bllstatehis;
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
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "Snapshots");
            DZService dzService = blldzservice.GetOne(guidService);
            if (dzService.Business.OwnerId.ToString() != customer.UserID)
            {
                throw new Exception("你的店铺没有该项服务！");
            }
            IList<snapshortsObj> snapshortsobj = new List<snapshortsObj>();
            IList<ServiceOrder> orderList = ibllserviceorder.GetOrderListOfServiceByDateRange(guidService, StartTime, EndTime);
            snapshortsobj = new snapshortsObj().Adap(orderList, bllstatehis);
            return snapshortsobj;
        }

    }
}
