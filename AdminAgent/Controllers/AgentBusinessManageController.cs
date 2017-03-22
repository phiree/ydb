using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.Application;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.Finance.Application;
using Ydb.Common;

namespace AdminAgent.Controllers
{
    public class AgentBusinessManageController : AgentBaseController
    {
        IBusinessService businessService = Bootstrap.Container.Resolve<IBusinessService>();
        IBusinessOwnerService businessOwnerService = Bootstrap.Container.Resolve<IBusinessOwnerService>();
        IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
        IServiceOrderService serviceOrderService = Bootstrap.Container.Resolve<IServiceOrderService>();
        IBalanceFlowService balanceFlowService = Bootstrap.Container.Resolve<IBalanceFlowService>();
        IComplaintService complaintService = Bootstrap.Container.Resolve<IComplaintService>();
        IServiceOrderAppraiseService appraiseService = Bootstrap.Container.Resolve<IServiceOrderAppraiseService>();
        /// <summary>
        /// 获取活跃商家列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessList()
        {
            try
            {
                ViewBag.UserName = CurrentUser.UserName;
                return View(businessOwnerService.GetBusinessOwnerListByArea(CurrentUser.AreaIdList,false));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取封停商家列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessFrozenList()
        {
            try
            {
                ViewBag.UserName = CurrentUser.UserName;
                return View(businessOwnerService.GetBusinessOwnerListByArea(CurrentUser.AreaIdList, true));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取封停店铺列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessStoreFrozenList()
        {
            try
            {
                return View(businessOwnerService.GetBusinessListByArea(CurrentUser.AreaIdList, false));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 获取封停服务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessServiceFrozenList()
        {
            try
            {
                return View(businessOwnerService.GetServiceListByArea(CurrentUser.AreaIdList, false));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取商家详情
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessDetail(string id)
        {
            try
            {
                ViewBag.UserName = CurrentUser.UserName;
                MemberDto memberDto = dzMembershipService.GetUserById(id);
                IList<Business> businessList = businessService.GetBusinessListByOwner(memberDto.Id);
                ViewData["StoreList"] = businessList;
                IList<string> businessIdList = businessList.Select(x => x.Id.ToString()).ToList();
                IList<ServiceOrder> orderList = serviceOrderService.GetOrdersByBusinessList(businessIdList, DateTime.MinValue, DateTime.MinValue, "");
                ViewData["OrderCount"] = orderList.Count;
                ViewData["OrderAmountTotal"] = serviceOrderService.GetTotalAmountByBusinessList(businessIdList);
                ViewData["FinanceAmountTotal"] = balanceFlowService.GetSumAmountByArea(businessIdList, "OrderShare");
                return View(memberDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 封停/解封账号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessDetailLock(string id, bool islock)//string type,
        {
            try
            {
                //接口
                //ViewBag.UserName = CurrentUser.UserName;
                //MemberDto member = dzMembershipService.GetUserById(id);
                //member.IsLocked = islock;
                dzMembershipService.LockDZMembership(id, islock, "违规操作");
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //member.IsLocked = islock;
                //if (islock)
                //{
                //    member.LockReason = "违规操作";
                //}
                return RedirectToAction("BusinessDetail");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 获取店铺详情
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessStoreDetail(string id)
        {
            try
            {
                ViewBag.UserName = CurrentUser.UserName;
                Guid storeId = StringHelper.CheckGuidID(id, "店铺Id");
                Business business = businessService.GetOne(StringHelper.CheckGuidID(id, "店铺Id"));
                ViewData["BusinessOwner"] = dzMembershipService.GetUserById(business.OwnerId.ToString());
                ViewData["OrderCount"]= serviceOrderService.GetOrdersCount("", "", storeId, null, DateTime.MinValue, DateTime.MinValue, Guid.Empty, "", "");
                ViewData["OrderAmountTotal"] = serviceOrderService.GetTotalAmountByBusinessList(new List<string> { id });
                ViewData["totalComplaintCount"] = complaintService.GetComplaintsCount("", id, "");
                ViewData["BusinessAverageAppraise"] = appraiseService.GetBusinessAverageAppraise(id);
                //string s = string.Join(",", business.ServiceType.Select(x=>x.Name).ToArray());
                return View(business);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取店铺的订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreDetailOrders(string id)
        {
            try
            {
                ViewData["id"] = id;
                //MemberDto member = dzMembershipService.GetUserById(id);
                IList<ServiceOrder> serviceOrderList = serviceOrderService.GetOrders(new Ydb.Common.Specification.TraitFilter(), "", "", StringHelper.CheckGuidID(id,"店铺Id"), null, DateTime.MinValue, DateTime.MinValue, Guid.Empty, "", "");
                //模拟数据
                //IList<ReceptionChatDto> receptionChatDtoList = MockData.receptionChatDtoList;
                return View(serviceOrderList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 获取店铺的服务信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreDetailOrders(string id)
        {
            try
            {
                ViewData["id"] = id;
                //MemberDto member = dzMembershipService.GetUserById(id);
                IList<ServiceOrder> serviceOrderList = serviceOrderService.GetOrders(new Ydb.Common.Specification.TraitFilter(), "", "", StringHelper.CheckGuidID(id, "店铺Id"), null, DateTime.MinValue, DateTime.MinValue, Guid.Empty, "", "");
                //模拟数据
                //IList<ReceptionChatDto> receptionChatDtoList = MockData.receptionChatDtoList;
                return View(serviceOrderList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

    }
}