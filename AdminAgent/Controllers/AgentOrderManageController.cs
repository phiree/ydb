using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.ApplicationService.ModelDto;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.Common;
using Ydb.BusinessResource.Application;
using Ydb.Common.Domain;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;
namespace AdminAgent.Controllers
{
    public class AgentOrderManageController : AgentBaseController
    {
        IOrdersService ordersService = Bootstrap.Container.Resolve<IOrdersService>();
        IServiceOrderService serviceOrderService = Bootstrap.Container.Resolve<IServiceOrderService>();
        IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
        IComplaintService complaintService = Bootstrap.Container.Resolve<IComplaintService>();
        IClaimsService claimsService = Bootstrap.Container.Resolve<IClaimsService>();
        IClaimsDetailsService claimsDetailsService = Bootstrap.Container.Resolve<IClaimsDetailsService>();
        IChatService chatService= Bootstrap.Container.Resolve<IChatService>();
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="strStatus"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult OrderList(string strStatus, string start, string end)
        {
            ViewBag.UserName = CurrentUser.UserName;
            enum_IsDone isDone = enum_IsDone.None;
            switch (strStatus)
            {
                case "所有":
                    isDone = enum_IsDone.None;
                    break;
                case "已完成":
                    isDone = enum_IsDone.OrderIsDone;
                    break;
                case "未完成":
                    isDone = enum_IsDone.OrderNotDone;
                    break;
            }
            IList<ServiceOrderDto> serviceOrderDtoList = new List<ServiceOrderDto>();
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            {
                serviceOrderDtoList = ordersService.GetOrdersListByAreaAndTime(CurrentUser.AreaIdList, DateTime.Now.AddMonths(-1), DateTime.Now, isDone);
            }
            else
            {
                serviceOrderDtoList = ordersService.GetOrdersListByAreaAndTime(CurrentUser.AreaIdList, StringHelper.CheckDateTime(start, "yyyyMMdd", "查询的开始时间", false), StringHelper.CheckDateTime(end, "yyyyMMdd", "查询的结束时间", true), isDone);
            }
            return View(serviceOrderDtoList);
        }

        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult OrderDetail(string id)
        {
            try
            {
                ViewData["id"] = id;
                //接口
                ViewBag.UserName = CurrentUser.UserName;
                ServiceOrder serviceOrder = serviceOrderService.GetOne(StringHelper.CheckGuidID(id,"订单Id"));
                ViewData["Business"] = businessService.GetOne(StringHelper.CheckGuidID(serviceOrder.BusinessId, "店铺Id"));
                ViewData["ComplaintList"] = complaintService.GetComplaints( new Ydb.Common.Specification.TraitFilter(), serviceOrder.Id,Guid.Empty,Guid.Empty);
                //ViewData["OrderStatusList"] = stateChangeHis.GetOrderHisList(serviceOrder);
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //ViewData["totalOnlineTime"] = MockData.totalOnlineTime;
                //ViewData["totalOrderCount"] = MockData.totalOrderCount;
                //ViewData["totalComplaintCount"] = MockData.totalComplaintCount;
                return View(serviceOrder);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取订单状态列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult OrderStatusList(string id)
        {
            try
            {
                //接口
                IList<StatisticsInfo<ServiceOrderStateChangeHis>> StatusTimeLineList = serviceOrderService.GetOrderStateTimeLine(id);
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //ViewData["totalOnlineTime"] = MockData.totalOnlineTime;
                //ViewData["totalOrderCount"] = MockData.totalOrderCount;
                //ViewData["totalComplaintCount"] = MockData.totalComplaintCount;
                return View(StatusTimeLineList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取订单理赔列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult OrderClaimsList(string id)
        {
            try
            {
                //接口
                //ServiceOrder serviceOrder = serviceOrderService.GetOne(StringHelper.CheckGuidID(id, "订单Id"));
                //Claims claims = claimsService.GetOneByOrder(serviceOrder);
                IList<ClaimsDetails> claimsDetailsList = claimsDetailsService.GetRefundStatus(StringHelper.CheckGuidID(id, "订单Id"), new Ydb.Common.Specification.TraitFilter(), enum_RefundAction.None);
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //ViewData["totalOnlineTime"] = MockData.totalOnlineTime;
                //ViewData["totalOrderCount"] = MockData.totalOrderCount;
                //ViewData["totalComplaintCount"] = MockData.totalComplaintCount;
                return View(claimsDetailsList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取订单聊天记录列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult OrderChatHistory(string id)
        {
            try
            {
                //接口
                //ViewData["ServiceOrder"] = serviceOrderService.GetOne(StringHelper.CheckGuidID(id, "订单Id"));
                IList<StatisticsInfo<ReceptionChatDto>> ChatTimeLineList = chatService.GetChatTimeLine(id);
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //ViewData["totalOnlineTime"] = MockData.totalOnlineTime;
                //ViewData["totalOrderCount"] = MockData.totalOrderCount;
                //ViewData["totalComplaintCount"] = MockData.totalComplaintCount;
                return View(ChatTimeLineList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }
    }
}