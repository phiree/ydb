using AutoMapper;
using System;
using System.Collections.Generic;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common;
using Ydb.Common.Specification;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;

namespace Dianzhu.ApplicationService.Order
{
    public class OrderService : IOrderService
    {
        private IServiceOrderService ibllserviceorder;
        private IServiceOrderStateChangeHisService bllstatehis;

        private IDZServiceService dzServiceService;
        private IOrderPushService bllpushservice;
        private IServiceOrderRemindService bllServiceOrderRemind;
        private IServiceOrderAppraiseService bllServiceOrderAppraise;
        private IOrderAssignmentService bllOrderAssignment;
        private IDZMembershipService memberService;
        private IClaimsService bllClaims;
        private IClaimsDetailsService bLLClaimsDetails;
        private IBusinessService businessService;

        private IStaffService staffService;
        private IServiceTypeService serviceTypeService;
        private Dianzhu.ApplicationService.Staff.IStaffService appStaffService;

        public OrderService(IServiceOrderService ibllserviceorder, IServiceOrderStateChangeHisService bllstatehis, IDZServiceService dzServiceService,
           IOrderPushService bllpushservice, IServiceOrderRemindService bllServiceOrderRemind,
       IServiceOrderAppraiseService bllServiceOrderAppraise,
     IOrderAssignmentService bllOrderAssignment, IDZMembershipService memberService,
      IClaimsService bllClaims, IClaimsDetailsService bLLClaimsDetails, Staff.IStaffService appStaffService, IStaffService staffService, IBusinessService businessService, IServiceTypeService serviceTypeService)

        {
            this.businessService = businessService;
            this.ibllserviceorder = ibllserviceorder;
            this.bllstatehis = bllstatehis;
            this.dzServiceService = dzServiceService;
            this.appStaffService = appStaffService;
            this.bllpushservice = bllpushservice;
            this.bllServiceOrderRemind = bllServiceOrderRemind;
            this.bllServiceOrderAppraise = bllServiceOrderAppraise;
            this.bllOrderAssignment = bllOrderAssignment;
            this.memberService = memberService;
            this.bllClaims = bllClaims;
            this.bLLClaimsDetails = bLLClaimsDetails;
            this.staffService = staffService;
            this.serviceTypeService = serviceTypeService;
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

        /// <summary>
        /// todo: 请重构.
        /// </summary>
        /// <param name="orderobj"></param>
        /// <param name="serviceorder"></param>
        public void changeObj(orderObj orderobj, ServiceOrder serviceorder)
        {
                               
            Business business = null;//= businessService.GetOne(new Guid(serviceorder.BusinessId));
            ServiceOrderStateChangeHis statehis = bllstatehis.GetMaxNumberOrderHis(serviceorder);
            orderobj.currentStatusObj = Mapper.Map<ServiceOrderStateChangeHis, orderStatusObj>(statehis);
            if (statehis == null)
            {
                orderobj.currentStatusObj = new orderStatusObj();
                orderobj.currentStatusObj.status = serviceorder.OrderStatus.ToString();
                orderobj.currentStatusObj.createTime = serviceorder.OrderCreated.ToString("yyyyMMddHHmmss");
            }
            orderobj.currentStatusObj.title = serviceorder.GetStatusTitleFriendly(serviceorder.OrderStatus);
            orderobj.currentStatusObj.content = serviceorder.GetStatusContextFriendly(serviceorder.OrderStatus);
            // Store.StoreService.staffService = staffService;
            ServiceType serviceType = null;
            if (serviceorder.Details != null && serviceorder.Details.Count > 0)
            {
                orderobj.serviceSnapshotObj = Mapper.Map<ServiceOrderDetail, serviceSnapshotObj>(serviceorder.Details[0]);

                business = businessService.GetOne(utils.CheckGuidID(serviceorder.Details[0].ServiceSnapShot.BusinessId, "店铺Id"));
                serviceType = serviceTypeService.GetOne(utils.CheckGuidID(serviceorder.Details[0].ServiceSnapShot.ServiceTypeId, "服务类型Id"));

                orderobj.storeObj = Mapper.Map<Business, storeObj>(business);
                // todo: refactor 待解决
                //Store.StoreService.changeObj(orderobj.storeObj, business);

                IList<DZTag> tagsList = dzServiceService.GetServiceTags(new Guid(serviceorder.Details[0].OriginalServiceId));
                string strTag = "";
                if (tagsList != null && tagsList.Count > 0)
                {
                    foreach (DZTag dztag in tagsList)
                    {
                        strTag = dztag.Text + ",";
                    }
                }

                decimal d = 0;
                if (!decimal.TryParse(orderobj.negotiateAmount, out d) || d < 0)
                {
                    orderobj.orderAmount = orderobj.negotiateAmount = (serviceorder.Details[0].UnitAmount * serviceorder.Details[0].
                        ServiceSnapShot.UnitPrice).ToString("0.00");
                }
                if (string.IsNullOrEmpty(orderobj.serviceAddress))
                {
                    orderobj.serviceAddress = serviceorder.Details[0].TargetAddress;
                }

                orderobj.serviceSnapshotObj.tag = strTag.TrimEnd(',');
                orderobj.contactObj.address = serviceorder.Details[0].TargetAddress;
                orderobj.contactObj.alias = serviceorder.Details[0].TargetCustomerName ?? "";
                orderobj.contactObj.phone = serviceorder.Details[0].TargetCustomerPhone ?? "";
                orderobj.notes = serviceorder.Details[0].Memo ?? "";
            }
            else
            {
                IList<ServiceOrderPushedService> dzs = bllpushservice.GetPushedServicesForOrder(serviceorder);
                if (dzs.Count > 0)
                {
                    decimal d = 0;

                    string strAmount = (dzs[0].UnitAmount * dzs[0].ServiceSnapShot.UnitPrice).ToString("0.00");
                    if (!decimal.TryParse(orderobj.orderAmount, out d) || d <= 0)
                    {
                        orderobj.orderAmount = strAmount;
                    }

                    if (!decimal.TryParse(orderobj.negotiateAmount, out d) || d < 0)
                    {
                        orderobj.orderAmount = orderobj.negotiateAmount = (dzs[0].UnitAmount * dzs[0].ServiceSnapShot.UnitPrice).ToString("0.00");
                    }
                    orderobj.contactObj.address = dzs[0].TargetAddress;
                    orderobj.contactObj.alias = dzs[0].TargetCustomerName ?? "";
                    orderobj.contactObj.phone = dzs[0].TargetCustomerPhone ?? "";
                    orderobj.notes = dzs[0].Memo ?? "";
                    orderobj.serviceTime = dzs[0].TargetTime.ToString("yyyyMMddHHmmss");
                    orderobj.serviceSnapshotObj = Mapper.Map<ServiceOrderPushedService, serviceSnapshotObj>(dzs[0]);

                    business = businessService.GetOne(utils.CheckGuidID(dzs[0].ServiceSnapShot.BusinessId, "店铺Id"));
                    serviceType = serviceTypeService.GetOne(utils.CheckGuidID(dzs[0].ServiceSnapShot.ServiceTypeId, "服务类型Id"));

                    if (!string.IsNullOrEmpty(dzs[0].OriginalServiceId) && !string.IsNullOrEmpty(dzs[0].ServiceSnapShot.BusinessId))
                    {
                        orderobj.storeObj = Mapper.Map<Business, storeObj>(business);
                        changeObj(orderobj.storeObj, business);
                    }
                    if (string.IsNullOrEmpty(orderobj.serviceAddress))
                    {
                        orderobj.serviceAddress = dzs[0].TargetAddress;
                    }
                }
            }

            //if (orderobj.serviceSnapshotObj.serviceType != null && serviceorder.Service.ServiceType!=null)
            //{
            //    orderobj.serviceSnapshotObj.serviceType.fullDescription = serviceorder.Service.ServiceType.ToString();
            //}

            orderobj.customerObj = Mapper.Map<MemberDto, customerObj>(memberService.GetUserById(serviceorder.CustomerId));

            if (serviceorder.StaffId != null)
            {
                Ydb.BusinessResource.DomainModel.Staff staff = staffService.GetOne(new Guid(serviceorder.StaffId));
                orderobj.formanObj = Mapper.Map<Ydb.BusinessResource.DomainModel.Staff, staffObj>(staff);
                appStaffService.changeObj(orderobj.formanObj, staff);
            }
            else
            {
                orderobj.formanObj = null;
            }

            if (serviceType != null)
            {
                orderobj.serviceSnapshotObj.serviceType.id = serviceType.Id.ToString();
                orderobj.serviceSnapshotObj.serviceType.fullDescription = serviceType.ToString();
                orderobj.serviceSnapshotObj.serviceType.superID = serviceType.ParentId.ToString();
            }

            if (business != null)
            {
                foreach (BusinessImage bimg in business.ChargePersonIdCards)
                {
                    if (bimg.ImageName != null)
                    {
                        orderobj.storeObj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);//MediaGetUrl
                    }
                }
                foreach (BusinessImage bimg in business.BusinessLicenses)
                {
                    if (bimg.ImageName != null)
                    {
                        orderobj.storeObj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);
                    }
                }
                foreach (BusinessImage bimg in business.BusinessShows)
                {
                    if (bimg.ImageName != null)
                    {
                        orderobj.storeObj.showImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);
                    }
                }
                orderobj.serviceSnapshotObj.location.longitude = business.Longitude.ToString();
                orderobj.serviceSnapshotObj.location.latitude = business.Latitude.ToString();
                orderobj.serviceSnapshotObj.location.address = business.RawAddressFromMapAPI == null ? "" : business.RawAddressFromMapAPI;

                orderobj.storeObj.location.latitude = business.Latitude.ToString();
                orderobj.storeObj.location.longitude = business.Longitude.ToString();
                orderobj.storeObj.location.address = business.RawAddressFromMapAPI == null ? "" : business.RawAddressFromMapAPI;
            }
            if (serviceorder.CustomerServiceId != null)
            {
                MemberDto member = memberService.GetUserById(serviceorder.CustomerServiceId);
                orderobj.customerServicesObj.id = serviceorder.CustomerServiceId;
                orderobj.customerServicesObj.alias = member.UserName;
                orderobj.customerServicesObj.imgUrl = member.AvatarUrl == null ? "" : Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + member.AvatarUrl;
            }
        }

        public void changeObj(storeObj storeobj, Business business)
        {
            foreach (BusinessImage bimg in business.ChargePersonIdCards)
            {
                if (bimg.ImageName != null)
                {
                    storeobj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);//MediaGetUrl
                }
            }
            foreach (BusinessImage bimg in business.BusinessLicenses)
            {
                if (bimg.ImageName != null)
                {
                    storeobj.certificateImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);
                }
            }
            foreach (BusinessImage bimg in business.BusinessShows)
            {
                if (bimg.ImageName != null)
                {
                    storeobj.showImgUrls.Add(Dianzhu.Config.Config.GetAppSetting("ImageHandler") + bimg.ImageName);
                }
            }
            if (storeobj.location == null)
            {
                storeobj.location = new locationObj();
            }
            storeobj.location.latitude = business.Latitude.ToString();
            storeobj.location.longitude = business.Longitude.ToString();
            storeobj.location.address = business.RawAddressFromMapAPI == null ? "" : business.RawAddressFromMapAPI;

            storeobj.headCount = int.Parse(staffService.GetStaffsCount("", "", "", "", "", "", business.Id).ToString());
        }

        /// <summary>
        /// 根据条件从数据库中获取订单列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        private IList<ServiceOrder> SeleteOrders(common_Trait_Filtering filter, common_Trait_OrderFiltering orderfilter, Customer customer)
        {
            Guid guidStore = utils.CheckGuidID(orderfilter.storeID, "orderfilter.storeID");
            string strStaffID = orderfilter.formanID == null ? null : utils.CheckGuidID(orderfilter.formanID, "orderfilter.formanID").ToString();
            DateTime dtAfter = utils.CheckDateTime(orderfilter.afterThisTime, "yyyyMMddHHmmss", "orderfilter.afterThisTime");
            DateTime dtBefore = utils.CheckDateTime(orderfilter.beforeThisTime, "yyyyMMddHHmmss", "orderfilter.beforeThisTime");
            IList<ServiceOrder> order = null;
            TraitFilter filter1 = utils.CheckFilter(filter, "ServiceOrder");
            Guid guidUser = utils.CheckGuidID(customer.UserID, "token.UserID");
            order = ibllserviceorder.GetOrders(filter1, orderfilter.statusSort, orderfilter.status, guidStore, strStaffID, dtAfter, dtBefore, guidUser, customer.UserType, orderfilter.assign);
            return order;
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
            IList<ServiceOrder> order = SeleteOrders(filter, orderfilter, customer);

            if (order == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<orderObj>();
            }
            IList<orderObj> orderobj = Mapper.Map<IList<ServiceOrder>, IList<orderObj>>(order);
            for (int i = 0; i < orderobj.Count; i++)
            {
                changeObj(orderobj[i], order[i]);
            }
            return orderobj;
        }

        /// <summary>
        /// 查询订单合集的校验
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ordersVerifyObj GetOrdersVerify(common_Trait_Filtering filter, common_Trait_OrderFiltering orderfilter, Customer customer)
        {
            IList<ServiceOrder> order = SeleteOrders(filter, orderfilter, customer);
            if (order == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return null;
            }
            ordersVerifyObj vo = new ordersVerifyObj();
            string strVerify = "";
            for (int i = 0; i < order.Count; i++)
            {
                strVerify = strVerify + order[i].Id.ToString() + "+" + order[i].LatestOrderUpdated.ToString("yyyyMMddHHmmss") + "+";
            }
            vo.verifyMD5 = utils.SignMD5(strVerify.TrimEnd('+'), "", "utf-8");
            return vo;
        }

        /// <summary>
        /// 查询订单超媒体合集
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderfilter"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<orderHypermediaObj> GetOrdersHypermedias(common_Trait_Filtering filter, common_Trait_OrderFiltering orderfilter, Customer customer)
        {
            IList<ServiceOrder> order = SeleteOrders(filter, orderfilter, customer);
            if (order == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return null;
            }
            IList<orderHypermediaObj> hdl = new List<orderHypermediaObj>();
            for (int i = 0; i < order.Count; i++)
            {
                orderHypermediaObj hd = new orderHypermediaObj();
                hd.href = "orders/" + order[i].Id.ToString();
                hdl.Add(hd);
            }
            return hdl;
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
            c.count = ibllserviceorder.GetOrdersCount(orderfilter.statusSort, orderfilter.status, guidStore, strStaffID, dtAfter, dtBefore, guidUser, customer.UserType, orderfilter.assign).ToString();
            return c;
        }

        /// <summary>
        /// 根据订单 ID 读取订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public orderObj GetOrder(string orderID)
        {
            ServiceOrder order = null;
            order = ibllserviceorder.GetOne(utils.CheckGuidID(orderID, "orderID"));
            if (order == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                //return null;
                throw new Exception("没有找到资源！");
            }
            //排除草稿单，因为推送服务会有时间差
            if (order.OrderStatus == enum_OrderStatus.Draft || order.OrderStatus == enum_OrderStatus.Search)
            {
                throw new Exception("没有找到资源！");
            }
            orderObj orderobj = Mapper.Map<ServiceOrder, orderObj>(order);
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
            ServiceOrder order = ibllserviceorder.GetOne(utils.CheckGuidID(orderID, "orderID"));
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            IList<ServiceOrderStateChangeHis> statehis = null;
            statehis = bllstatehis.GetOrderHisList(order);
            if (statehis == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<orderStatusObj>();
            }
            IList<orderStatusObj> orderstatussbj = Mapper.Map<IList<ServiceOrderStateChangeHis>, IList<orderStatusObj>>(statehis);
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
            ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (customer.UserType == "business")
            {
                if (string.IsNullOrEmpty(order.BusinessId) || order.ServiceBusinessOwnerId != customer.UserID)
                {
                    throw new Exception("这不是你的订单！");
                }
            }
            if (customer.UserType == "staff")
            {
                Ydb.BusinessResource.DomainModel.Staff staff = staffService.GetOneByUserID(Guid.Empty, customer.UserID);
                if (staff == null || string.IsNullOrEmpty(order.BusinessId) || staff.Belongto.Id.ToString() != order.BusinessId)
                {
                    throw new Exception("这不是你店铺的订单！");
                }
            }
            //if (!string.IsNullOrEmpty(orderobj.negotiateAmount))
            //{
            //    try
            //    {
            //        order.NegotiateAmount = decimal.Parse(orderobj.negotiateAmount);
            //        if (order.OrderStatus ==enum_OrderStatus.Finished || order.OrderStatus ==enum_OrderStatus.Appraised || order.OrderStatus ==enum_OrderStatus.checkPayWithNegotiate)
            //        {
            //            throw new Exception("该订单已经完成支付，无法再修改协商价格！");
            //        }
            //    }
            //    catch
            //    {
            //        throw new FormatException("新协商价格格式不正确！");
            //    }
            //}
            if (!string.IsNullOrEmpty(orderobj.notes))
            {
                ibllserviceorder.UpdateMemo(order.Id, orderobj.notes);
            }
            order = ibllserviceorder.GetOne(order.Id);

            //if (order != null && order.LatestOrderUpdated == dt)
            //{
            orderobj = Mapper.Map<ServiceOrder, orderObj>(order);
            changeObj(orderobj, order);
            //}
            //else
            //{
            //    throw new Exception("更新失败");
            //}
            return orderobj;
        }

        /// <summary>
        /// 修改订单价格
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="orderobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public orderObj PatchOrderPrice(string orderID, orderObj orderobj, Customer customer)
        {
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");

            ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            Business business = businessService.GetOne(new Guid(order.BusinessId));
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (customer.UserType == "business")
            {
                if (business == null || business.OwnerId.ToString() != customer.UserID)
                {
                    throw new Exception("这不是你的订单！");
                }
            }
            if (customer.UserType == "staff")
            {
                Ydb.BusinessResource.DomainModel.Staff staff = staffService.GetOneByUserID(Guid.Empty, customer.UserID);
                if (staff == null || business == null || staff.Belongto.Id != business.Id)
                {
                    throw new Exception("这不是你店铺的订单！");
                }
            }
            if (!string.IsNullOrEmpty(orderobj.negotiateAmount))
            {
                if (order.OrderStatus != enum_OrderStatus.Negotiate)
                {
                    throw new Exception(order.GetStatusTitleFriendly(order.OrderStatus) + "状态，无法修改协商价格！");
                }
                decimal d = 0;
                if (decimal.TryParse(orderobj.negotiateAmount, out d))
                {
                    ibllserviceorder.OrderFlow_BusinessNegotiate(guidOrder, d);
                }
                else
                {
                    throw new FormatException("新协商价格格式不正确！");
                }
            }
            else
            {
                throw new FormatException("新协商价格不能为空！");
            }
            //if (!string.IsNullOrEmpty(orderobj.notes))
            //{
            //    order.Memo = orderobj.notes;
            //}

            //DateTime dt = DateTime.Now;
            //order.LatestOrderUpdated = dt;
            //ibllserviceorder.Update(order);
            order = ibllserviceorder.GetOne(guidOrder);

            //if (order != null && order.LatestOrderUpdated == dt)
            //{
            orderobj = Mapper.Map<ServiceOrder, orderObj>(order);
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
        public IList<serviceSnapshotObj> GetPushServices(string orderID, Customer customer)
        {
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            ServiceOrder order = null;
            order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (customer.UserType == "customer" && order.CustomerId != customer.UserID)
            {
                throw new Exception("这不是你的订单！");
            }
            IList<ServiceOrderPushedService> pushServiceList = bllpushservice.GetPushedServicesForOrder(order);
            IList<serviceSnapshotObj> servicesobj = Mapper.Map<IList<ServiceOrderPushedService>, IList<serviceSnapshotObj>>(pushServiceList);
            for (int i = 0; i < servicesobj.Count; i++)
            {
                Business business = businessService.GetOne(new Guid(pushServiceList[i].ServiceSnapShot.BusinessId));
                if (business != null)
                {
                    //未确认的订单中没有店铺
                    servicesobj[i].location.longitude = business.Longitude.ToString();
                    servicesobj[i].location.latitude = business.Latitude.ToString();
                    servicesobj[i].location.address = business.RawAddressFromMapAPI == null ? "" : business.RawAddressFromMapAPI;
                }
                ServiceType st = serviceTypeService.GetOne(utils.CheckGuidID(pushServiceList[i].ServiceSnapShot.ServiceTypeId, "服务类型Id"));
                if (st != null)
                {
                    servicesobj[i].serviceType.id = st.Id.ToString();
                    servicesobj[i].serviceType.fullDescription = st.ToString();
                    servicesobj[i].serviceType.superID = st.ParentId.ToString();
                }
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
        public orderObj PutConfirmService(string orderID, string serviceID, Customer customer)
        {
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (order.CustomerId != customer.UserID)
            {
                throw new Exception("这不是你的订单！");
            }
            ServiceDto service = dzServiceService.GetOne(utils.CheckGuidID(serviceID, "serviceID"));
            if (service == null)
            {
                throw new Exception("该服务不存在！");
            }
            if (order.OrderStatus != enum_OrderStatus.DraftPushed)
            {
                throw new Exception("该订单不是已推送服务的状态！");
            }
            ibllserviceorder.OrderFlow_ConfirmOrder(order.Id, serviceID);
            order = ibllserviceorder.GetOne(order.Id);
            // bllpushservice.SelectServiceAndCreate(order, serviceID);
           // ibllserviceorder.Update(order);
            //IList<ServiceOrderPushedService> pushServiceList = bllpushservice.GetPushedServicesForOrder(order);
            //orderObj.svcObj.SetTag(orderObj.svcObj, tagsList);
            ServiceOrderStateChangeHis orderHis = bllstatehis.GetOrderHis(order);

            string strName = order.Details[0].ServiceSnapShot.Name ?? string.Empty;
            string strAlias = order.Details[0].ServiceSnapShot.BusinessName ?? string.Empty;
            string strType = order.Details[0].ServiceSnapShot.ServiceTypeName ?? string.Empty;
            //order.Details.Count > 0? order.Details[0].ServieSnapShot.ServiceName ?? string.Empty: pushService.ServiceName ?? string.Empty;

            //增加订单提醒
            ServiceOrderRemind remind = new ServiceOrderRemind(strName, strAlias + "提供" + strType, order.Details[0].TargetTime, true, order.Id, new Guid(order.CustomerId));
            bllServiceOrderRemind.Save(remind);

            orderObj orderobj = Mapper.Map<ServiceOrder, orderObj>(order);
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
        public orderObj PutAppraisee(string orderID, appraiseObj appraiseobj, Customer customer)
        {
            if (appraiseobj.target != "customerService" && appraiseobj.target != "store")
            {
                throw new FormatException("评价对象只能是客户和店铺！");
            }
            //if (appraiseobj.target == "customerService")
            //{
            //    appraiseobj.target = "cer";
            //}
            enum_ComplaintTarget target;
            if (!Enum.TryParse<enum_ComplaintTarget>(appraiseobj.target, out target))
            {
                throw new FormatException("评价对象格式不正确！");
            }
            decimal appValue;
            if (!decimal.TryParse(appraiseobj.value, out appValue))
            {
                throw new FormatException("评分值格式不正确,只能取（0 ~ 5的整数）！");
            }
            if (appValue < 0 || appValue > 5 || appValue * 10 % 5 != 0)
            {
                throw new FormatException("评分值格式不正确,只能取（0 ~ 5的整数）！");
            }
            appValue = Math.Floor(appValue);//取整
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (order.CustomerId != customer.UserID)
            {
                throw new Exception("这不是你的订单！");
            }
            ServiceOrderAppraise appraise = new ServiceOrderAppraise(order, target, appValue, appraiseobj.content);
            bllServiceOrderAppraise.Save(appraise);

            //连续请求两次，可能同时修改一个订单的状态，会造成同一个表的死锁报错
            int c = 1;
            for (int i = 0; i < 10; i++)
            {
                if (c == 0)
                {
                    break;
                }
                try
                {
                    ServiceOrder so = ibllserviceorder.GetOne(order.Id);
                    if (so.OrderStatus != enum_OrderStatus.Appraised)
                    {
                        ibllserviceorder.OrderFlow_CustomerAppraise(guidOrder);
                    }
                    c = 0;
                }
                catch (Exception ex)
                {
                    if (i == 9)
                    {
                        throw ex;
                    }
                    System.Threading.Thread.Sleep(1000);
                    c = 1;
                }
            }
            order = ibllserviceorder.GetOne(guidOrder);
            orderObj orderobj = Mapper.Map<ServiceOrder, orderObj>(order);
            changeObj(orderobj, order);
            return orderobj;
        }

        /// <summary>
        /// 获得该订单的聊天人
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public linkManObj GetLinkMan(string orderID, Customer customer)
        {
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            if (order.CustomerId != customer.UserID)
            {
                throw new Exception("这不是你的订单！");
            }
            if (order.Details.Count <= 0)
            {
                throw new Exception("该订单没有确定的服务！");
            }
            linkManObj linkman = new linkManObj();
            //linkman.linkManID = order.OpenFireLinkMan;
            string strIp = System.Web.HttpContext.Current.Request.Url.Host;//.Url.ToString();
            //if (order.OpenFireLinkMan == order.Business.OwnerId )
            //{
            //    linkman.linkManID = order.OpenFireLinkMan + "@" + strIp + "/" +enum_XmppResource.YDBan_Store;
            //}
            //else
            //{
            //    linkman.linkManID = order.OpenFireLinkMan + "@" + strIp + "/" +enum_XmppResource.YDBan_Staff;
            //}
            linkman.linkManID = order.ServiceBusinessOwnerId + "@" + strIp + "/" + enum_XmppResource.YDBan_Store;
            return linkman;

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
            //    targetId = order.Details[0].OriginalService.Business.OwnerId;
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
        public orderObj PatchCurrentStatus(string orderID, string newStatus, Customer customer)
        {
            enum_OrderStatus status = enum_OrderStatus.Unknow;
            try
            {
                status = (enum_OrderStatus)Enum.Parse(typeof(enum_OrderStatus), newStatus);
            }
            catch (Exception e)
            {
                throw new FormatException("要变更的状态无效！");
            }
            if (status == enum_OrderStatus.Unknow)
            {
                throw new FormatException("要变更的状态无效！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Guid userId = utils.CheckGuidID(customer.UserID, "customer.UserID");
            MemberDto member = memberService.GetUserById(userId.ToString());

            OrderServiceFlow osf = new OrderServiceFlow();
            ServiceOrder order = new ServiceOrder();
            if (member.UserType == enum_UserType.customer.ToString())
            {
                order = ibllserviceorder.GetOrderByIdAndCustomer(guidOrder, member.Id.ToString());
                if (order == null)
                {
                    throw new Exception("没有对应的订单！");
                }

                switch (status)
                {
                    case enum_OrderStatus.checkPayWithDeposit:
                        ibllserviceorder.OrderFlow_PayDepositAndWaiting(guidOrder);
                        break;

                    case enum_OrderStatus.Negotiate:
                        ibllserviceorder.OrderFlow_CustomerDisagreeNegotiate(guidOrder);
                        break;

                    case enum_OrderStatus.Assigned:
                        ibllserviceorder.OrderFlow_CustomConfirmNegotiate(guidOrder);
                        break;

                    case enum_OrderStatus.Canceled:
                        //bllServiceOrder.OrderFlow_Canceled(order);
                        if (ibllserviceorder.OrderFlow_Canceled(guidOrder))
                        {
                            //orderObj orderobj = Mapper.Map<ServiceOrder, orderObj>(order);
                            //changeObj(orderobj, order);
                            //return orderobj;
                            break;
                        }
                        else
                        {
                            throw new Exception("订单取消失败，请稍候再试");
                        }
                    case enum_OrderStatus.Ended:
                        ibllserviceorder.OrderFlow_CustomerFinish(guidOrder);
                        break;

                    case enum_OrderStatus.checkPayWithNegotiate:
                        ibllserviceorder.OrderFlow_CustomerPayFinalPayment(guidOrder);
                        break;

                    case enum_OrderStatus.WaitingPayWithRefund:
                        ibllserviceorder.OrderFlow_WaitingPayWithRefund(guidOrder, member.Id.ToString());
                        break;

                    case enum_OrderStatus.checkPayWithRefund:
                        ibllserviceorder.OrderFlow_CustomerPayRefund(guidOrder);
                        break;

                    case enum_OrderStatus.checkPayWithIntervention:
                        ibllserviceorder.OrderFlow_CustomerPayInternention(guidOrder);
                        break;

                    default:
                        throw new Exception("用户提交的状态类型有误！");
                        //ilog.Debug("用户Id：" + userId + "，用户类型：" + member.UserType + "，禁止提交status为" + status + "的访问数据！");
                        //this.state_CODE = Dicts.StateCode[1];
                        //this.err_Msg = "用户提交的类型有误！";
                        //return;
                }
            }
            else if (member.UserType == enum_UserType.business.ToString())
            {
                order = ibllserviceorder.GetOne(guidOrder);
                if (order.Details[0].ServiceSnapShot.BusinessOwnerId != userId.ToString())
                {
                    throw new Exception("没有对应的订单！");
                }
                if (order.Details[0].ServiceSnapShot.BusinessOwnerId != member.Id.ToString())
                {
                    throw new Exception("该订单不属于该用商户！");
                }
                switch (status)
                {
                    case enum_OrderStatus.Negotiate:
                        ibllserviceorder.OrderFlow_BusinessConfirm(guidOrder);
                        break;

                    case enum_OrderStatus.Begin:
                        ibllserviceorder.OrderFlow_BusinessStartService(guidOrder);
                        break;

                    case enum_OrderStatus.isEnd:
                        ibllserviceorder.OrderFlow_BusinessFinish(guidOrder);
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
            order = ibllserviceorder.GetOne(guidOrder);
            orderObj orderobj = Mapper.Map<ServiceOrder, orderObj>(order);
            changeObj(orderobj, order);
            return orderobj;
            //return new string[] { "订单取消成功" };
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
            enum_RefundAction action;
            if (!Enum.TryParse<enum_RefundAction>(refundfilter.action, out action))
            {
                action = enum_RefundAction.None;
            }
            //try
            //{
            //    action = (enum_RefundAction)Enum.Parse(typeof(enum_RefundAction), refundfilter.action);
            //}
            //catch
            //{
            //    throw new FormatException("该理赔动作无效！");
            //}
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
            if (order == null)
            {
                throw new Exception("该订单不存在！");
            }
            IList<ClaimsDetails> claimsdetails = null;
            TraitFilter filter1 = utils.CheckFilter(filter, "ClaimsDetails");
            claimsdetails = bLLClaimsDetails.GetRefundStatus(guidOrder, filter1, action);
            if (claimsdetails == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<refundStatusObj>();
            }
            IList<refundStatusObj> refundstatusobj = Mapper.Map<IList<ClaimsDetails>, IList<refundStatusObj>>(claimsdetails);
            for (int i = 0; i < refundstatusobj.Count; i++)
            {
                for (int j = 0; j < claimsdetails[i].ClaimsDetailsResourcesUrl.Count; j++)
                {
                    refundstatusobj[i].resourcesUrls.Add(claimsdetails[i].ClaimsDetailsResourcesUrl[j] == null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + claimsdetails[i].ClaimsDetailsResourcesUrl[j] : "");
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
        public refundStatusObj PostRefundAction(string orderID, refundObj refundobj, Customer customer)
        {
            IList<string> resourcesurls = new List<string>();
            if (refundobj.resourcesUrls != null)
            {
                for (int i = 0; i < refundobj.resourcesUrls.Count; i++)
                {
                    resourcesurls.Add(utils.GetFileName(refundobj.resourcesUrls[i], "MediaGetUrl"));
                }
            }
            enum_RefundAction action;
            if (!Enum.TryParse<enum_RefundAction>(refundobj.action, out action))
            {
                throw new FormatException("该理赔动作无效！");
            }
            if (action == enum_RefundAction.None)
            {
                throw new FormatException("该理赔动作无效！");
            }
            //try
            //{
            //    action = (enum_RefundAction)Enum.Parse(typeof(enum_RefundAction), refundobj.action);
            //}
            //catch (Exception e)
            //{
            //    throw new FormatException("该理赔动作无效！");
            //}
            decimal amount = 0;
            bool isAmount = decimal.TryParse(refundobj.amount, out amount);
            if (action == enum_RefundAction.submit || action == enum_RefundAction.askPay)
                //开始服务后，取消订单走理赔流程，所以提交理赔时价格可以为零
                if (!isAmount)
                {
                    throw new FormatException("提交价格的格式有误,需为大于等于零的数值！");
                }
            if (amount < 0)
            {
                throw new FormatException("提交价格的格式有误,需为大于等于零的数值！");
            }
            Guid guidOrder = utils.CheckGuidID(orderID, "orderID");
            Guid userId = utils.CheckGuidID(customer.UserID, "customer.UserID");
            MemberDto member = memberService.GetUserById(userId.ToString());

            OrderServiceFlow osf = new OrderServiceFlow();
            bool ActionSuccess = false;
            refundStatusObj refundstatusobj = new refundStatusObj();
            enum_ChatTarget target = enum_ChatTarget.all;
            if (member.UserType == enum_UserType.customer.ToString())
            {
                target = enum_ChatTarget.user;
                ServiceOrder order = ibllserviceorder.GetOrderByIdAndCustomer(guidOrder, member.Id.ToString());
                enum_OrderStatus oldStatus = order.OrderStatus;
                if (order == null)
                {
                    throw new Exception("没有对应的订单！");
                }
                switch (action)
                {
                    case enum_RefundAction.submit:
                        bool isNeesRefund = false;
                        if (order.OrderStatus == enum_OrderStatus.Begin ||
                             order.OrderStatus == enum_OrderStatus.isEnd ||
                              order.OrderStatus == enum_OrderStatus.Ended)
                        {
                            isNeesRefund = false;
                        }
                        else if (order.OrderStatus == enum_OrderStatus.Finished ||
                                  order.OrderStatus == enum_OrderStatus.Appraised)
                        {
                            isNeesRefund = true;
                        }
                        else
                        {
                            throw new Exception("该订单状态无法提交理赔！");
                        }

                        if (!ibllserviceorder.OrderFlow_CustomerRefund(orderID, isNeesRefund, amount))
                        {
                            throw new Exception("提交理赔失败！");
                        }
                        //target =enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;

                    case enum_RefundAction.agree:
                        ibllserviceorder.OrderFlow_WaitingPayWithRefund(guidOrder, member.Id.ToString());
                        //target =enum_ChatTarget.store;
                        ActionSuccess = true;
                        break;

                    case enum_RefundAction.intervention:
                        ibllserviceorder.OrderFlow_YDBInsertIntervention(guidOrder);
                        //target =enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;

                    case enum_RefundAction.cancel:
                        ibllserviceorder.OrderFlow_RefundSuccess(guidOrder);
                        //target =enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;

                    default:
                        throw new Exception("暂未支持该动作！");
                }
                if (ActionSuccess)
                {
                    Claims claims = new Claims(order, oldStatus, member.Id.ToString());
                    claims.AddDetailsFromClaims(refundobj.content, amount, resourcesurls, target, member.Id.ToString());
                    bllClaims.Save(claims);
                    refundstatusobj.content = refundobj.content;
                    refundstatusobj.amount = refundobj.amount;
                    refundstatusobj.resourcesUrls = refundobj.resourcesUrls;
                    refundstatusobj.target = target.ToString();
                    refundstatusobj.orderStatus = order.OrderStatus.ToString();
                }
            }
            else if (member.UserType == enum_UserType.business.ToString())
            {
                target = enum_ChatTarget.store;
                ServiceOrder order = ibllserviceorder.GetOne(guidOrder);
                enum_OrderStatus oldStatus = order.OrderStatus;
                if (order.ServiceBusinessOwnerId != userId.ToString())
                {
                    throw new Exception("没有对应的订单！");
                }
                if (order.ServiceBusinessOwnerId != member.Id.ToString())
                {
                    throw new Exception("该订单不属于该用商户！");
                }
                switch (action)
                {
                    case enum_RefundAction.refund:
                        ibllserviceorder.OrderFlow_BusinessIsRefund(guidOrder, member.Id.ToString());
                        //target =enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;

                    case enum_RefundAction.reject:
                        ibllserviceorder.OrderFlow_BusinessRejectRefund(guidOrder, member.Id.ToString());
                        //target =enum_ChatTarget.user;
                        ActionSuccess = true;
                        break;

                    case enum_RefundAction.askPay:
                        ibllserviceorder.OrderFlow_BusinessAskPayWithRefund(guidOrder, refundobj.content, amount, resourcesurls, member.Id.ToString());
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
                    Claims claims = new Claims(order, oldStatus, member.Id.ToString());
                    claims.AddDetailsFromClaims(refundobj.content, amount, resourcesurls, target, member.Id.ToString());
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
            ServiceOrder order = null;
            order = ibllserviceorder.GetOne(utils.CheckGuidID(orderID, "orderID"));
            if (order == null)
            {
                throw new Exception("该订单不存在!");
            }
            if (string.IsNullOrEmpty(order.StaffId))
            {
                throw new Exception("该订单未指派员工!");
            }
            Ydb.BusinessResource.DomainModel.Staff staff = staffService.GetOne(new Guid(order.StaffId));
            staffObj staffobj = Mapper.Map<Ydb.BusinessResource.DomainModel.Staff, staffObj>(staff);
            return staffobj;
        }

        /// <summary>
        /// 更改负责人
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="staffID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public object PatchForman(string orderID, string staffID, Customer customer)
        {
            //ServiceOrder order = null;
            //order = ibllserviceorder.GetOne(utils.CheckGuidID(orderID, "orderID"));
            //if (order == null)
            //{
            //    throw new Exception("该订单不存在!");
            //}
            //if (order.Business == null || order.Business.OwnerId != customer.UserID)
            //{
            //    throw new Exception("这不是你的订单!");
            //}
            //IList<OrderAssignment> assignment = bllOrderAssignment.GetOAListByOrder(order);
            //if (assignment.Count == 0)
            //{
            //    throw new Exception("该订单还没有指派!");
            //}
            //int c =-1;
            //for (int i = 0; i < assignment.Count; i++)
            //{
            //    if (assignment[i].AssignedStaff.Id.ToString() == staffID)
            //    {
            //        assignment[i].IsHeader = true;
            //        order.Staff = assignment[i].AssignedStaff;
            //        c = i;
            //    }
            //    else
            //    {
            //        assignment[i].IsHeader = false;
            //    }
            //}
            //if (c == -1)
            //{
            //    throw new Exception("该订单没有指派过该员工!");
            //}
            //staffObj staffobj = Mapper.Map<Model.Staff, staffObj>(order.Staff);
            //return staffobj;

            if (string.IsNullOrEmpty(orderID))
            {
                throw new FormatException("改派的订单号不能为空！");
            }
            if (staffID == null)
            {
                throw new FormatException("改派的员工ID不能为空！");
            }
            ServiceOrder order = ibllserviceorder.GetOneOrder(utils.CheckGuidID(orderID, "orderID"), customer.UserID);
            if (order == null)
            {
                throw new Exception("该商户不存在该订单！");
            }
            string strState = "";
            if (order.StaffId == null && staffID != "")
            {
                strState = "指派";
                //throw new Exception("该订单还没有被指派过！");
            }
            if (order.StaffId != null && staffID != "")
            {
                strState = "更改指派";
            }
            if (order.StaffId != null && staffID == "")
            {
                strState = "取消指派";
            }
            if (order.StaffId == null && staffID == "")
            {
                throw new Exception("该订单还没有被指派过，请指定指派员工！");
            }
            if (order.OrderStatus == enum_OrderStatus.Finished || order.OrderStatus == enum_OrderStatus.Appraised)
            {
                throw new Exception("该订单的服务已经完成，无法再改派！");
            }
            if (strState == "更改指派" && order.StaffId == staffID)
            {
                throw new Exception("改派给同一个人了！");
            }
            if (strState != "取消指派")
            {
                Ydb.BusinessResource.DomainModel.Staff staff = staffService.GetStaff(utils.CheckGuidID(order.BusinessId, "order.BusinessId"), utils.CheckGuidID(staffID, "staffID"));
                if (staff == null)
                {
                    throw new Exception("该订单所属的店铺中不存在该员工！");
                }
                if (!staff.Enable)
                {
                    throw new Exception("指派或改派的员工不在职！");
                }
            }
            //else
            //{
            //    staff = staffService.GetOne(utils.CheckGuidID(order.StaffId, "order.StaffId"));
            //}
            //if (staff.IsAssigned)
            //{
            //    throw new Exception("改派的员工已经被指派过！");
            //}

            OrderAssignment oa = new OrderAssignment();
            DateTime dt = DateTime.Now;
            switch (strState)
            {
                case "指派":
                    //staff.IsAssigned = true;
                    oa.Enabled = true;
                    oa.CreateTime = dt;
                    oa.AssignedTime = dt;
                    oa.OrderId = order.Id.ToString();
                    oa.BusinessId = order.BusinessId;
                    oa.AssignedStaffId = staffID;
                    //order.StaffId = staffID;
                    //order.LatestOrderUpdated = DateTime.Now;
                    //oa.Order.Details[0].Staff.Clear();
                    //oa.Order.Details[0].Staff.Add(staff);
                    bllOrderAssignment.Save(oa);
                    ibllserviceorder.UpdateStaff(order.Id,staffID);
                    strState = "指派成功";
                    break;

                case "更改指派":
                    oa = bllOrderAssignment.FindByOrderAndStaff(order, order.StaffId);
                    if (oa == null || oa.Enabled == false)
                    {
                        throw new Exception("原指派不存在或已取消！");
                    }
                    //staff.IsAssigned = false;
                    //staff.IsAssigned = true;
                    oa.AssignedStaffId = staffID;
                    oa.AssignedTime = dt;
                    //order.StaffId = staffID;
                    //order.LatestOrderUpdated = DateTime.Now;
                    ibllserviceorder.UpdateStaff(order.Id, staffID);
                    strState = "改派成功";
                    break;

                case "取消指派":
                    oa = bllOrderAssignment.FindByOrderAndStaff(order, order.StaffId);
                    if (oa == null || oa.Enabled == false)
                    {
                        throw new Exception("该指派不存在或已取消！");
                    }
                    //staff.IsAssigned = false;
                    oa.Enabled = false;
                    oa.DeAssignedTime = dt;
                    //order.StaffId = null;
                    //order.LatestOrderUpdated = DateTime.Now;
                    ibllserviceorder.Update(order);
                    ibllserviceorder.UpdateStaff(order.Id, null);
                    strState = "取消成功";
                    break;
            }
            return new string[] { strState };
        }

        /// <summary>
        /// 指派负责人
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="staffID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public object PostForman(string orderID, string staffID, Customer customer)
        {
            if (string.IsNullOrEmpty(orderID))
            {
                throw new FormatException("指派的订单号不能为空！");
            }
            if (string.IsNullOrEmpty(staffID))
            {
                throw new FormatException("指派的员工ID不能为空！");
            }
            OrderAssignment oa = new OrderAssignment();//Mapper.Map<assignObj, Model.OrderAssignment>(assignobj);
            ServiceOrder order = ibllserviceorder.GetOneOrder(utils.CheckGuidID(orderID, "orderID"), customer.UserID);
            if (order == null)
            {
                throw new Exception("该商户指派的订单不存在！");
            }
            if (order.StaffId != null)
            {
                throw new Exception("该订单已经指派！");
            }
            Ydb.BusinessResource.DomainModel.Staff staff = staffService.GetStaff(new Guid(order.BusinessId), utils.CheckGuidID(staffID, "staffID"));
            if (staff == null)
            {
                throw new Exception("在指派订单所属的店铺中不存在该指派的员工！");
            }
            if (!staff.Enable)
            {
                throw new Exception("指派的员工不在职！");
            }
            //if (staff.IsAssigned)
            //{
            //    throw new Exception("指派的员工已经被指派过！");
            //}
            staff.IsAssigned = true;
            oa.Enabled = true;
            DateTime dt = DateTime.Now;
            oa.CreateTime = dt;
            oa.AssignedTime = dt;
            oa.OrderId = order.Id.ToString();
            oa.BusinessId = order.BusinessId;
            oa.AssignedStaffId = staffID;
            order.StaffId = staffID;
            order.LatestOrderUpdated = DateTime.Now;
            //oa.Order.Details[0].Staff.Clear();
            //oa.Order.Details[0].Staff.Add(staff);
            bllOrderAssignment.Save(oa);
            return new string[] { "指派成功！" };
            //bllstaff.Update(staff);
            //oa = bllassign.GetAssignById(oa.Id);
            //if (oa != null && oa.CreateTime == dt)
            //{
            //assignobj = Mapper.Map<Model.OrderAssignment, assignObj>(oa);
            //return assignobj;
            //}
            //else
            //{
            //    throw new Exception("新建失败");
            //}
        }

        /// <summary>
        /// 取消指派
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="staffID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public object DeleteForman(string orderID, string staffID, Customer customer)
        {
            if (string.IsNullOrEmpty(orderID))
            {
                throw new FormatException("取消指派的订单号不能为空！");
            }
            if (string.IsNullOrEmpty(staffID))
            {
                throw new FormatException("取消指派的员工ID不能为空！");
            }
            ServiceOrder order = ibllserviceorder.GetOneOrder(utils.CheckGuidID(orderID, "orderID"), customer.UserID);
            if (order == null)
            {
                throw new Exception("该商户不存在该订单！");
            }
            if (order.StaffId == null)
            {
                throw new Exception("该订单还没有被指派过！");
            }
            if (order.OrderStatus == enum_OrderStatus.Finished || order.OrderStatus == enum_OrderStatus.Appraised)
            {
                throw new Exception("该订单的服务已经完成，无法再取消指派！");
            }
            if (order.StaffId != staffID)
            {
                throw new Exception("该订单指派的不是该员工！");
            }
            OrderAssignment oa = bllOrderAssignment.FindByOrderAndStaff(order, order.StaffId);
            if (oa == null || oa.Enabled == false)
            {
                throw new Exception("该指派不存在或已取消！");
            }
            //staff.IsAssigned = false;
            oa.Enabled = false;
            DateTime dt = DateTime.Now;
            oa.DeAssignedTime = dt;
            order.StaffId = null;
            order.LatestOrderUpdated = DateTime.Now;
            return new string[] { "取消成功！" };
        }
    }
}