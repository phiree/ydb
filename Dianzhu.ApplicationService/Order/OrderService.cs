using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
using AutoMapper;
using Dianzhu.Model;

namespace Dianzhu.ApplicationService.Order
{
    public class OrderService : IOrderService
    {

        BLL.IBLLServiceOrder ibllserviceorder;
        BLL.IIMSession imSession;

        public static BLL.BLLServiceOrderStateChangeHis bllstatehis;
        BLL.BLLDZService blldzservice;
        public static BLL.PushService bllpushservice;
        BLL.BLLServiceOrderRemind bllServiceOrderRemind;
        BLL.BLLServiceOrderAppraise bllServiceOrderAppraise;
        BLL.BLLOrderAssignment bllOrderAssignment;
        BLL.DZMembershipProvider bllDZM;
        BLL.BLLClaims bllClaims;
        BLL.BLLClaimsDetails bLLClaimsDetails;
        BLL.BLLStaff bllStaff;
        public OrderService(BLL.IBLLServiceOrder ibllserviceorder, BLL.BLLServiceOrderStateChangeHis bllstatehis, BLL.BLLDZService blldzservice, BLL.PushService bllpushservice, BLL.BLLServiceOrderRemind bllServiceOrderRemind,
        BLL.BLLServiceOrderAppraise bllServiceOrderAppraise, BLL.IIMSession imSession, BLL.BLLOrderAssignment bllOrderAssignment, BLL.DZMembershipProvider bllDZM, BLL.BLLClaims bllClaims, BLL.BLLClaimsDetails bLLClaimsDetails, BLL.BLLStaff bllStaff)
        {
            this.ibllserviceorder = ibllserviceorder;
            OrderService.bllstatehis = bllstatehis;
            this.blldzservice = blldzservice;
            OrderService.bllpushservice = bllpushservice;
            this.bllServiceOrderRemind = bllServiceOrderRemind;
            this.bllServiceOrderAppraise = bllServiceOrderAppraise;
            this.imSession = imSession;
            this.bllOrderAssignment = bllOrderAssignment;
            this.bllDZM = bllDZM;
            this.bllClaims = bllClaims;
            this.bLLClaimsDetails = bLLClaimsDetails;
            this.bllStaff = bllStaff;
        }

        /// <summary>
        /// 根据orderID获取Order
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public orderObj GetOne(Guid guid)
        {
            orderObj orderobj = new orderObj();
            return orderobj;
        }

       public static void changeObj(orderObj orderobj, Model.ServiceOrder serviceorder)
        {
            Model.ServiceOrderStateChangeHis statehis = bllstatehis.GetMaxNumberOrderHis(serviceorder);
            orderobj.currentStatusObj = Mapper.Map<Model.ServiceOrderStateChangeHis, orderStatusObj>(statehis);
            if (statehis == null)
            {
                orderobj.currentStatusObj = new orderStatusObj();
                orderobj.currentStatusObj.status = serviceorder.OrderStatus.ToString();
                orderobj.currentStatusObj.createTime = serviceorder.OrderCreated.ToString("yyyyMMddHHmmss");
            }
            orderobj.currentStatusObj.title = serviceorder.GetStatusTitleFriendly(serviceorder.OrderStatus);
            orderobj.currentStatusObj.content = serviceorder.GetStatusContextFriendly(serviceorder.OrderStatus);
            orderobj.serviceSnapshotObj = Mapper.Map<Model.DZService, servicesObj>(serviceorder.Service);
            orderobj.storeObj = Mapper.Map<Model.Business, storeObj>(serviceorder.Business);
            if (orderobj.serviceSnapshotObj == null)
            {
                IList<Model.ServiceOrderPushedService> dzs= bllpushservice.GetPushedServicesForOrder(serviceorder);
                if (dzs.Count > 0)
                {
                    orderobj.serviceSnapshotObj = Mapper.Map<Model.ServiceOrderPushedService, servicesObj>(dzs[0]);
                    if (dzs[0].OriginalService != null && dzs[0].OriginalService.Business != null)
                    {
                        orderobj.storeObj = Mapper.Map<Model.Business, storeObj>(dzs[0].OriginalService.Business);
                    }
                }

            }
            //if (orderobj.serviceSnapshotObj.serviceType != null && serviceorder.Service.ServiceType!=null)
            //{
            //    orderobj.serviceSnapshotObj.serviceType.fullDescription = serviceorder.Service.ServiceType.ToString();
            //}
            orderobj.customerObj = Mapper.Map<Model.DZMembership, customerObj>(serviceorder.Customer);
            
            orderobj.formanObj = Mapper.Map<Model.Staff, staffObj>(serviceorder.Staff);
            if (serviceorder.Business != null)
            {
                foreach (Model.BusinessImage bimg in serviceorder.Business.ChargePersonIdCards)
                {
                    if (bimg.ImageName != null)
                    {
                        orderobj.storeObj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);//MediaGetUrl
                    }
                }
                foreach (Model.BusinessImage bimg in serviceorder.Business.BusinessLicenses)
                {
                    if (bimg.ImageName != null)
                    {
                        orderobj.storeObj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);
                    }
                }
                foreach (Model.BusinessImage bimg in serviceorder.Business.BusinessShows)
                {
                    if (bimg.ImageName != null)
                    {
                        orderobj.storeObj.showImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);
                    }
                }
                orderobj.serviceSnapshotObj.location.longitude = serviceorder.Business.Longitude.ToString();
                orderobj.serviceSnapshotObj.location.latitude = serviceorder.Business.Latitude.ToString();
                orderobj.serviceSnapshotObj.location.address = serviceorder.Business.RawAddressFromMapAPI == null ? "" : serviceorder.Business.RawAddressFromMapAPI;

                orderobj.storeObj.location.latitude = serviceorder.Business.Latitude.ToString();
                orderobj.storeObj.location.longitude = serviceorder.Business.Longitude.ToString();
                orderobj.storeObj.location.address = serviceorder.Business.RawAddressFromMapAPI==null?"":serviceorder.Business.RawAddressFromMapAPI;
            }
            if (serviceorder.CustomerService != null)
            {
                orderobj.customerServicesObj.id = serviceorder.CustomerService.Id.ToString();
                orderobj.customerServicesObj.alias = serviceorder.CustomerService.DisplayName;
                orderobj.customerServicesObj.imgUrl = serviceorder.CustomerService.AvatarUrl==null?"" : Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + serviceorder.CustomerService.AvatarUrl;
            }
            
        }

        /// <summary>
        /// 查询订单合集
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<orderObj> GetOrders(common_Trait_Filtering filter, common_Trait_OrderFiltering orderfilter, Customer customer)
        {
            Guid guidStore = utils.CheckGuidID(orderfilter.storeID, "orderfilter.storeID");
            string strStaffID = orderfilter.formanID == null ? null : utils.CheckGuidID(orderfilter.formanID, "orderfilter.formanID").ToString();
            DateTime dtAfter = utils.CheckDateTime(orderfilter.afterThisTime, "yyyyMMddHHmmss", "orderfilter.afterThisTime");
            DateTime dtBefore= utils.CheckDateTime(orderfilter.beforeThisTime, "yyyyMMddHHmmss", "orderfilter.beforeThisTime");
            IList<Model.ServiceOrder> order = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "ServiceOrder");
            Guid guidUser = utils.CheckGuidID(customer.UserID, "token.UserID");
            order = ibllserviceorder.GetOrders(filter1, orderfilter.statusSort, orderfilter.status,guidStore,strStaffID, dtBefore, dtAfter,guidUser,customer.UserType,orderfilter.assign);

            if (order == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<orderObj> orderobj = Mapper.Map<IList<Model.ServiceOrder>, IList<orderObj>>(order);
            for (int i = 0; i < orderobj.Count; i++)
            {
                changeObj(orderobj[i], order[i]);
            }
            return orderobj;
        }

        /// <summary>
        /// 查询订单数量
        /// </summary>
        /// <param name="orderfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public countObj GetOrdersCount(common_Trait_OrderFiltering orderfilter, Customer customer)
        {
            Guid guidStore = utils.CheckGuidID(orderfilter.storeID, "orderfilter.storeID");
            string strStaffID = orderfilter.formanID == null ? null : utils.CheckGuidID(orderfilter.formanID, "orderfilter.formanID").ToString();
            DateTime dtAfter = utils.CheckDateTime(orderfilter.afterThisTime, "yyyyMMddHHmmss", "orderfilter.afterThisTime");
            DateTime dtBefore = utils.CheckDateTime(orderfilter.beforeThisTime, "yyyyMMddHHmmss", "orderfilter.beforeThisTime");
            countObj c = new countObj();
            Guid guidUser = utils.CheckGuidID(customer.UserID, "token.UserID");
            c.count = ibllserviceorder.GetOrdersCount(orderfilter.statusSort, orderfilter.status, guidStore, strStaffID, dtBefore, dtAfter, guidUser,customer.UserType, orderfilter.assign).ToString();
            return c;
        }

        /// <summary>
        /// 根据订单 ID 读取订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public orderObj GetOrder(string orderID)
        {
            Model.ServiceOrder order = null;
            order = ibllserviceorder.GetOne(utils.CheckGuidID(orderID, "orderID"));
            if (order == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            orderObj orderobj = Mapper.Map<Model.ServiceOrder, orderObj>(order);
            changeObj(orderobj, order);
            return orderobj;
        }

        /// <summary>
        /// 获得订单历史状态列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public IList<orderStatusObj> GetAllStatusList(string orderID)
        {
            //Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Model.ServiceOrder order= ibllserviceorder.GetOne(utils.CheckGuidID(orderID, "orderID"));
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            IList<Model.ServiceOrderStateChangeHis> statehis = null;
            statehis = bllstatehis.GetOrderHisList(order);
            if (statehis == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<orderStatusObj> orderstatussbj = Mapper.Map<IList<Model.ServiceOrderStateChangeHis>, IList<orderStatusObj>>(statehis);
            return orderstatussbj;
        }

        /// <summary>
        /// 修改订单信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public orderObj PatchOrder(string orderID, orderObj orderobj, Customer customer)
        {
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Model.ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (customer.UserType == "business")
            {
                if (order.Business == null || order.Business.Owner.Id.ToString() != customer.UserID)
                {
                    throw new Exception("这不是你的订单！");
                }
            }
            if (customer.UserType == "staff")
            {
                Model.Staff staff = bllStaff.GetOneByUserID(Guid.Empty, customer.UserID);
                if (staff == null || order.Business == null || staff.Belongto.Id != order.Business.Id)
                {
                    throw new Exception("这不是你店铺的订单！");
                }
            }
            if (!string.IsNullOrEmpty(orderobj.negotiateAmount))
            {
                try
                {
                    order.NegotiateAmount = decimal.Parse(orderobj.negotiateAmount);
                    if (order.OrderStatus == Model.Enums.enum_OrderStatus.Finished || order.OrderStatus == Model.Enums.enum_OrderStatus.Appraised || order.OrderStatus == Model.Enums.enum_OrderStatus.checkPayWithNegotiate)
                    {
                        throw new Exception("该订单已经完成支付，无法再修改协商价格！");
                    }
                }
                catch
                {
                    throw new FormatException("新协商价格格式不正确！");
                }
            }
            if (!string.IsNullOrEmpty(orderobj.notes))
            {
                order.Memo = orderobj.notes;
            }
            
            DateTime dt = DateTime.Now;
            order.LatestOrderUpdated = dt;
            //ibllserviceorder.Update(order);
            //order = ibllserviceorder.GetOne(guidOrder);


            //if (order != null && order.LatestOrderUpdated == dt)
            //{
            orderobj = Mapper.Map<Model.ServiceOrder, orderObj>(order);
            changeObj(orderobj, order);
            //}
            //else
            //{
            //    throw new Exception("更新失败");
            //}
            return orderobj;
        }

        /// <summary>
        /// 获得订单所包含的推送服务
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<servicesObj> GetPushServices(string orderID,Customer customer)
        {
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Model.ServiceOrder order = null;
            order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (customer.UserType == "customer" && order.Customer.Id.ToString ()!=customer.UserID)
            {
                throw new Exception("这不是你的订单！");
            }
            IList<Model.ServiceOrderPushedService> pushServiceList = bllpushservice.GetPushedServicesForOrder(order);
            IList<servicesObj> servicesobj = Mapper.Map<IList<Model.ServiceOrderPushedService>, IList<servicesObj>>(pushServiceList);
            for (int i = 0; i < servicesobj.Count; i++)
            {
                servicesobj[i].location.longitude = pushServiceList[i].OriginalService.Business.Longitude.ToString();
                servicesobj[i].location.latitude = pushServiceList[i].OriginalService.Business.Latitude.ToString();
                servicesobj[i].location.address = pushServiceList[i].OriginalService.Business.RawAddressFromMapAPI == null ? "" : pushServiceList[i].OriginalService.Business.RawAddressFromMapAPI;
            }
            return servicesobj;
        }

        /// <summary>
        /// 草稿单确定服务
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="serviceID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public orderObj PutConfirmService(string orderID, string serviceID,Customer customer)
        {
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Model.ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (order.Customer.Id.ToString() != customer.UserID)
            {
                throw new Exception("这不是你的订单！");
            }
            Model.DZService service = blldzservice.GetOne(utils.CheckGuidID(serviceID, "serviceID"));
            if (service == null)
            {
                throw new Exception("该服务不存在！");
            }
            if (order.OrderStatus != Model.Enums.enum_OrderStatus.DraftPushed)
            {
                throw new Exception("该订单不是已推送服务的状态！");
            }
            bllpushservice.SelectServiceAndCreate(order, service);
            ibllserviceorder.Update(order);
            //IList<ServiceOrderPushedService> pushServiceList = bllpushservice.GetPushedServicesForOrder(order);

            ServiceOrderStateChangeHis orderHis = bllstatehis.GetOrderHis(order);

            string strName = order.Details[0].ServieSnapShot.ServiceName ?? string.Empty;
            string strAlias = order.Details[0].OriginalService.Business.Name ?? string.Empty;
            string strType = order.Details[0].OriginalService.ServiceType.ToString() ?? string.Empty;
            //order.Details.Count > 0? order.Details[0].ServieSnapShot.ServiceName ?? string.Empty: pushService.ServiceName ?? string.Empty;

            //增加订单提醒
            ServiceOrderRemind remind = new ServiceOrderRemind(strName, strAlias + "提供" + strType, order.Details[0].TargetTime, true, order.Id, order.Customer.Id);
            bllServiceOrderRemind.Save(remind);

            orderObj orderobj = Mapper.Map<Model.ServiceOrder, orderObj>(order);
            changeObj(orderobj, order);
            return orderobj;
        }

        /// <summary>
        /// 提交评价
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="appraiseobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public orderObj PutAppraisee(string orderID, appraiseObj appraiseobj,Customer customer)
        {
            Model.Enums.enum_ChatTarget target;
            if (!Enum.TryParse(appraiseobj.target, out target))
            {
                throw new FormatException("评价对象格式不正确！");
            }
            decimal appValue;
            if (!decimal.TryParse(appraiseobj.value, out appValue))
            {
                throw new FormatException("评分值格式不正确,只能取（0 ~ 5的整数）！");
            }
            if (appValue < 0 || appValue > 5 || appValue*10 % 5 != 0)
            {
                throw new FormatException("评分值格式不正确,只能取（0 ~ 5的整数）！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Model.ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (order.Customer.Id.ToString() != customer.UserID)
            {
                throw new Exception("这不是你的订单！");
            }
            ServiceOrderAppraise appraise = new ServiceOrderAppraise(order, target, appValue, appraiseobj.content); 
            bllServiceOrderAppraise.Save(appraise);

            ibllserviceorder.OrderFlow_CustomerAppraise(order);

            orderObj orderobj = Mapper.Map<Model.ServiceOrder, orderObj>(order);
            changeObj(orderobj, order);
            return orderobj;
        }

        /// <summary>
        /// 获得该订单的聊天人
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public string GetLinkMan(string orderID, Customer customer)
        {
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Model.ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (order.Customer.Id.ToString() != customer.UserID)
            {
                throw new Exception("这不是你的订单！");
            }
            if (order.Details.Count <= 0)
            {
                throw new Exception("该订单没有确定的服务！");
            }
            //ReceptionAssigner ra = new ReceptionAssigner(imSession);

            return order.OpenFireLinkMan;

            //string targetId = "";
            //IList<OrderAssignment> orderAssList = bllOrderAssignment.GetOAListByOrder(order);
            //if (orderAssList.Count > 0)
            //{
            //    for (int i = 0; i < orderAssList.Count; i++)
            //    {
            //        if (orderAssList[i].IsHeader)
            //        {
            //            targetId = orderAssList[i].AssignedStaff.UserID;
            //        }
            //    }
            //    if (targetId == "")
            //    {
            //        targetId = orderAssList[0].AssignedStaff.UserID.ToString();
            //    }
            //}
            //else
            //{
            //    targetId = order.Details[0].OriginalService.Business.Owner.Id.ToString();
            //}
            //return targetId ;
        }

        /// <summary>
        /// 请求变更订单状态
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="newStatus"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public object PatchCurrentStatus(string orderID, string newStatus, Customer customer)
        {
            Model.Enums.enum_OrderStatus status = Model.Enums.enum_OrderStatus.Unknow;
            try
            {
                status = (Model.Enums.enum_OrderStatus)Enum.Parse(typeof(Model.Enums.enum_OrderStatus), newStatus);
            }
            catch (Exception e)
            {
                throw new FormatException("要变更的状态无效！");
            }
            if (status == Model.Enums.enum_OrderStatus.Unknow)
            {
                throw new FormatException("要变更的状态无效！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Guid userId = utils.CheckGuidID(customer.UserID, "customer.UserID");
            DZMembership member = bllDZM.GetUserById(userId);

            OrderServiceFlow osf = new OrderServiceFlow();
            if (member.UserType == Model.Enums.enum_UserType.customer)
            {
                ServiceOrder order = ibllserviceorder.GetOrderByIdAndCustomer(guidOrder, member);
                if (order == null)
                {
                    throw new Exception("没有对应的订单！");
                }

                switch (status)
                {
                    case Model.Enums.enum_OrderStatus.checkPayWithDeposit:
                        ibllserviceorder.OrderFlow_PayDepositAndWaiting(order);
                        break;
                    case Model.Enums.enum_OrderStatus.Negotiate:
                        ibllserviceorder.OrderFlow_CustomDisagreeNegotiate(order);
                        break;
                    case Model.Enums.enum_OrderStatus.Assigned:
                        ibllserviceorder.OrderFlow_CustomConfirmNegotiate(order);
                        break;
                    case Model.Enums.enum_OrderStatus.Canceled:
                        //bllServiceOrder.OrderFlow_Canceled(order);
                        if (ibllserviceorder.OrderFlow_Canceled(order))
                        {
                            return "订单取消成功";
                        }
                        else
                        {
                            return "订单取消失败，请稍候再试";
                        }
                    case Model.Enums.enum_OrderStatus.Ended:
                        ibllserviceorder.OrderFlow_CustomerFinish(order);
                        break;
                    case Model.Enums.enum_OrderStatus.checkPayWithNegotiate:
                        ibllserviceorder.OrderFlow_CustomerPayFinalPayment(order);
                        break;
                    case Model.Enums.enum_OrderStatus.WaitingPayWithRefund:
                        ibllserviceorder.OrderFlow_WaitingPayWithRefund(order, member);
                        break;
                    case Model.Enums.enum_OrderStatus.checkPayWithRefund:
                        ibllserviceorder.OrderFlow_CustomerPayRefund(order);
                        break;
                    case Model.Enums.enum_OrderStatus.checkPayWithIntervention:
                        ibllserviceorder.OrderFlow_CustomerPayInternention(order);
                        break;

                    default:
                        throw new Exception("用户提交的状态类型有误！");
                        //ilog.Debug("用户Id：" + userId + "，用户类型：" + member.UserType + "，禁止提交status为" + status + "的访问数据！");
                        //this.state_CODE = Dicts.StateCode[1];
                        //this.err_Msg = "用户提交的类型有误！";
                        //return;
                }
            }
            else if (member.UserType == Model.Enums.enum_UserType.business)
            {
                ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
                if (order.Details[0].OriginalService.Business.Owner.Id != userId)
                {
                    throw new Exception("没有对应的订单！");
                }
                if (order.Details[0].OriginalService.Business.Owner.Id != member.Id)
                {
                    throw new Exception("该订单不属于该用商户！");
                }
                switch (status)
                {
                    case Model.Enums.enum_OrderStatus.Negotiate:
                        ibllserviceorder.OrderFlow_BusinessConfirm(order);
                        break;
                    case Model.Enums.enum_OrderStatus.Begin:
                        ibllserviceorder.OrderFlow_BusinessStartService(order);
                        break;
                    case Model.Enums.enum_OrderStatus.isEnd:
                        ibllserviceorder.OrderFlow_BusinessFinish(order);
                        break;

                    default:
                        throw new Exception("用户提交的类型有误！");
                        //ilog.Debug("用户Id：" + userId + "，用户类型：" + member.UserType + "，禁止提交status为" + status + "的访问数据！");
                        //this.state_CODE = Dicts.StateCode[1];
                        //this.err_Msg = "用户提交的类型有误!";
                        //return;
                }
            }
            else
            {
                throw new Exception("该用户没有权限访问接口！");
                //ilog.Debug("该用户没有权限访问接口!用户Id：" + userId + ";用户类型：" + member.UserType);
                //this.state_CODE = Dicts.StateCode[1];
                //this.err_Msg = "该用户没有权限访问接口!";
                //return;
            }
            return "订单取消成功";
        }

        /// <summary>
        /// 获得理赔状态列表
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="refundfilter"></param>
        /// <returns></returns>
        public IList<refundStatusObj> GetRefundStatus(string orderID, common_Trait_Filtering filter, common_Trait_RefundFiltering refundfilter)
        {
            Model.Enums.enum_RefundAction action;
            try
            {
                action = (Model.Enums.enum_RefundAction)Enum.Parse(typeof(Model.Enums.enum_RefundAction), refundfilter.action);
            }
            catch 
            {
                throw new FormatException("该理赔动作无效！");
            }
            IList<Model.ClaimsDetails> claimsdetails = null;
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "ClaimsDetails");
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            claimsdetails = bLLClaimsDetails.GetRefundStatus(guidOrder, filter1, action);
            if (claimsdetails == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<refundStatusObj> refundstatusobj = Mapper.Map<IList<Model.ClaimsDetails>, IList<refundStatusObj>>(claimsdetails);
            for (int i = 0; i < refundstatusobj.Count; i++)
            {
                for (int j = 0; j < claimsdetails[i].ClaimsDetailsResourcesUrl.Count; j++)
                {
                    refundstatusobj[i].resourcesUrls.Add ( claimsdetails[i].ClaimsDetailsResourcesUrl[j] == null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + claimsdetails[i].ClaimsDetailsResourcesUrl[j] : "");
                }
            }
            return refundstatusobj;
        }

        /// <summary>
        /// 提交理赔动作
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="refundobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public refundStatusObj PostRefundAction(string orderID, refundObj refundobj,Customer customer)
        {
            IList<string> resourcesurls = new List<string>();
            for (int i = 0; i < refundobj.resourcesUrls.Count; i++)
            {
                resourcesurls.Add(utils.GetFileName(refundobj.resourcesUrls[i]));
            }
            Model.Enums.enum_RefundAction action ;
            try
            {
                action = (Model.Enums.enum_RefundAction)Enum.Parse(typeof(Model.Enums.enum_RefundAction), refundobj.action);
            }
            catch (Exception e)
            {
                throw new FormatException("该理赔动作无效！");
            }
            decimal amount = 0;
            bool isAmount = decimal.TryParse(refundobj.amount, out amount);
            if(action== Model.Enums.enum_RefundAction.submit|| action == Model.Enums.enum_RefundAction.askPay)
            if (!isAmount)
            {
                throw new FormatException("提交价格的格式有误,需为大于零的数值！");
            }
            if (amount <= 0)
            {
                throw new FormatException("提交价格的格式有误,需为大于零的数值！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Guid userId = utils.CheckGuidID(customer.UserID, "customer.UserID");
            DZMembership member = bllDZM.GetUserById(userId);

            OrderServiceFlow osf = new OrderServiceFlow();
            bool ActionSuccess = false;
            refundStatusObj refundstatusobj = new refundStatusObj();
            Model.Enums.enum_ChatTarget target = Model.Enums.enum_ChatTarget.all;
            if (member.UserType == Model.Enums.enum_UserType.customer)
            {
                target = Model.Enums.enum_ChatTarget.user;
                ServiceOrder order = ibllserviceorder.GetOrderByIdAndCustomer(guidOrder, member);
                Model.Enums.enum_OrderStatus oldStatus = order.OrderStatus;
                if (order == null)
                {
                    throw new Exception("没有对应的订单！");
                }
                switch (action)
                {
                    case Model.Enums.enum_RefundAction.submit:
                        bool isNeesRefund = false;
                        if (order.OrderStatus == Model.Enums.enum_OrderStatus.Begin ||
                             order.OrderStatus == Model.Enums.enum_OrderStatus.isEnd ||
                              order.OrderStatus == Model.Enums.enum_OrderStatus.Ended)
                        {
                            isNeesRefund = false;
                        }
                        else if (order.OrderStatus == Model.Enums.enum_OrderStatus.Finished ||
                                  order.OrderStatus == Model.Enums.enum_OrderStatus.Appraised)
                        {
                            isNeesRefund = true;
                        }
                        else
                        {
                            throw new Exception("该订单状态无法提交理赔！");
                        }

                        if (!ibllserviceorder.OrderFlow_CustomerRefund(order, isNeesRefund, amount))
                        {
                            throw new Exception("提交理赔失败！");
                        }
                        //target = Model.Enums.enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;
                    case Model.Enums.enum_RefundAction.agree:
                        ibllserviceorder.OrderFlow_WaitingPayWithRefund(order, member);
                        //target = Model.Enums.enum_ChatTarget.store;
                        ActionSuccess = true;
                        break;
                    case Model.Enums.enum_RefundAction.intervention:
                        ibllserviceorder.OrderFlow_YDBInsertIntervention(order);
                        //target = Model.Enums.enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;
                    case Model.Enums.enum_RefundAction.cancel:
                        ibllserviceorder.OrderFlow_RefundSuccess(order);
                        //target = Model.Enums.enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;
                    default:
                        throw new Exception("暂未支持该动作！");
                }
                if (ActionSuccess)
                {
                    Claims claims = new Claims(order, oldStatus, member);
                    claims.AddDetailsFromClaims(claims, refundobj.content, amount, resourcesurls, target, member);
                    bllClaims.Save(claims);
                    refundstatusobj.content = refundobj.content;
                    refundstatusobj.amount = refundobj.amount;
                    refundstatusobj.resourcesUrls = refundobj.resourcesUrls;
                    refundstatusobj.target = target.ToString();
                    refundstatusobj.orderStatus =order.OrderStatus.ToString();
                }
            }
            else if (member.UserType == Model.Enums.enum_UserType.business)
            {
                target = Model.Enums.enum_ChatTarget.store;
                ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
                Model.Enums.enum_OrderStatus oldStatus = order.OrderStatus;
                if (order.Details[0].OriginalService.Business.Owner.Id != userId)
                {
                    throw new Exception("没有对应的订单！");
                }
                if (order.Details[0].OriginalService.Business.Owner.Id != member.Id)
                {
                    throw new Exception("该订单不属于该用商户！");
                }
                switch (action)
                {
                    case Model.Enums.enum_RefundAction.refund:
                        ibllserviceorder.OrderFlow_BusinessIsRefund(order, member);
                        //target = Model.Enums.enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;
                    case Model.Enums.enum_RefundAction.reject:
                        ibllserviceorder.OrderFlow_BusinessRejectRefund(order, member);
                        //target = Model.Enums.enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;
                    case Model.Enums.enum_RefundAction.askPay:
                        ibllserviceorder.OrderFlow_BusinessAskPayWithRefund(order, refundobj.content, amount, resourcesurls, member);
                        refundstatusobj.content = refundobj.content;
                        refundstatusobj.amount = refundobj.amount;
                        refundstatusobj.resourcesUrls = refundobj.resourcesUrls;
                        refundstatusobj.target = target.ToString();
                        refundstatusobj.orderStatus = order.OrderStatus.ToString();
                        ActionSuccess = false;
                        break;
                    default:
                        throw new Exception("暂未支持该动作！");
                }
                if (ActionSuccess)
                {
                    Claims claims = new Claims(order, oldStatus, member);
                    claims.AddDetailsFromClaims(claims, refundobj.content, amount, resourcesurls, target, member);
                    bllClaims.Save(claims);
                    refundstatusobj.content = refundobj.content;
                    refundstatusobj.amount = refundobj.amount;
                    refundstatusobj.resourcesUrls = refundobj.resourcesUrls;
                    refundstatusobj.target = target.ToString();
                    refundstatusobj.orderStatus = order.OrderStatus.ToString();
                }
            }
            else
            {
                throw new Exception("该用户没有权限访问接口！");
                //ilog.Debug("该用户没有权限访问接口!用户Id：" + userId + ";用户类型：" + member.UserType);
                //this.state_CODE = Dicts.StateCode[1];
                //this.err_Msg = "该用户没有权限访问接口!";
                //return;
            }
            return refundstatusobj;
        }

        /// <summary>
        /// 读取订单负责人
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public staffObj GetForman(string orderID)
        {
            Model.ServiceOrder order = null;
            order = ibllserviceorder.GetOne(utils.CheckGuidID(orderID, "orderID"));
            if (order == null)
            {
                throw new Exception("该订单不存在!");
            }
            staffObj staffobj = Mapper.Map<Model.Staff, staffObj>(order.Staff);
            return staffobj;
        }

        /// <summary>
        /// 更改负责人
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="staffID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public staffObj PatchForman(string orderID,string staffID,Customer customer)
        {
            Model.ServiceOrder order = null;
            order = ibllserviceorder.GetOne(utils.CheckGuidID(orderID, "orderID"));
            if (order == null)
            {
                throw new Exception("该订单不存在!");
            }
            if (order.Business == null || order.Business.Owner.Id.ToString() != customer.UserID)
            {
                throw new Exception("这不是你的订单!");
            }
            IList<OrderAssignment> assignment = bllOrderAssignment.GetOAListByOrder(order);
            if (assignment.Count == 0)
            {
                throw new Exception("该订单还没有指派!");
            }
            int c =-1;
            for (int i = 0; i < assignment.Count; i++)
            {
                if (assignment[i].AssignedStaff.Id.ToString() == staffID)
                {
                    assignment[i].IsHeader = true;
                    order.Staff = assignment[i].AssignedStaff;
                    c = i;
                }
                else
                {
                    assignment[i].IsHeader = false;
                }
            }
            if (c == -1)
            {
                throw new Exception("该订单没有指派过该员工!");
            }
            staffObj staffobj = Mapper.Map<Model.Staff, staffObj>(order.Staff);
            return staffobj;
        }
    }
}
