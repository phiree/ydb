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
        IDZServiceService dzServiceService = Bootstrap.Container.Resolve<IDZServiceService>();
        IStaffService staffService = Bootstrap.Container.Resolve<IStaffService>();
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
        /// 封停/解封商家
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
                ViewData["DoneOrderCount"] = serviceOrderService.GetOrdersCount("done", "", storeId, null, DateTime.MinValue, DateTime.MinValue, Guid.Empty, "", "");
                ViewData["OrderAmountTotal"] = serviceOrderService.GetTotalAmountByBusinessList(new List<string> { id });
                ViewData["totalComplaintCount"] = complaintService.GetComplaintsCount("", id, "");
                ViewData["StaffAverageAppraise"] = appraiseService.GetBusinessAverageAppraise(id,"");
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
        /// 封停/解封店铺
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreDetailLock(string id, bool islock)//string type,
        {
            try
            {
                //接口
                //ViewBag.UserName = CurrentUser.UserName;
                //MemberDto member = dzMembershipService.GetUserById(id);
                //member.IsLocked = islock;
                businessService.EnableBusiness(StringHelper.CheckGuidID(id,"店铺Id"), islock, "违规操作");
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
        public ActionResult BusinessStoreDetailService(string id)
        {
            try
            {
                ViewData["id"] = id;
                //MemberDto member = dzMembershipService.GetUserById(id);
                IList<ServiceDto> serviceDtoList = dzServiceService.GetServices(new Ydb.Common.Specification.TraitFilter(),Guid.Empty,"","",-1, StringHelper.CheckGuidID(id, "店铺Id"));
                //模拟数据
                //IList<ReceptionChatDto> receptionChatDtoList = MockData.receptionChatDtoList;
                return View(serviceDtoList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 获取店铺的员工信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreDetailStaffs(string id)
        {
            try
            {
                ViewData["id"] = id;
                //MemberDto member = dzMembershipService.GetUserById(id);
                IList<Staff> staffList = staffService.GetStaffs(new Ydb.Common.Specification.TraitFilter(), "", "", "", "","","", StringHelper.CheckGuidID(id, "店铺Id"));
                //模拟数据
                //IList<ReceptionChatDto> receptionChatDtoList = MockData.receptionChatDtoList;
                return View(staffList);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 获取服务的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreServiceDetail(string id)
        {
            try
            {
                ViewData["id"] = id;
                //MemberDto member = dzMembershipService.GetUserById(id);
                ServiceDto serviceDto = dzServiceService.GetOne(StringHelper.CheckGuidID(id, "服务Id"));
                //模拟数据
                //IList<ReceptionChatDto> receptionChatDtoList = MockData.receptionChatDtoList;
                return View(serviceDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 封停/解封服务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreServiceDetailLock(string id, bool islock)//string type,
        {
            try
            {
                //接口
                //ViewBag.UserName = CurrentUser.UserName;
                //MemberDto member = dzMembershipService.GetUserById(id);
                //member.IsLocked = islock;
                dzServiceService.EnabledDZService(StringHelper.CheckGuidID(id, "服务Id"), islock, "违规操作");
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
        /// 获取服务的工作时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreServiceOpenTime(string id,int week)
        {
            try
            {
                ViewData["id"] = id;
                //MemberDto member = dzMembershipService.GetUserById(id);
                ServiceOpenTimeDto serviceOpenTimeDto = dzServiceService.GetOpenTimeByWeek(StringHelper.CheckGuidID(id, "服务Id"),(DayOfWeek)week);
                //模拟数据
                //IList<ReceptionChatDto> receptionChatDtoList = MockData.receptionChatDtoList;
                return View(serviceOpenTimeDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取员工的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreStaffDetail(string id)
        {
            try
            {
                ViewData["id"] = id;
                Staff staff = staffService.GetOne(StringHelper.CheckGuidID(id, "员工Id"));
                ViewData["MemberUserName"] = dzMembershipService.GetUserById(staff.UserID).UserName;
                ViewData["OrderCount"] = serviceOrderService.GetOrdersCount("", "", staff.Belongto.Id, staff.Id.ToString(), DateTime.MinValue, DateTime.MinValue, Guid.Empty, "", "");
                ViewData["DoneOrderCount"] = serviceOrderService.GetOrdersCount("done", "", staff.Belongto.Id, staff.Id.ToString(), DateTime.MinValue, DateTime.MinValue, Guid.Empty, "", "");
                ViewData["BusinessAverageAppraise"] = appraiseService.GetBusinessAverageAppraise(staff.Belongto.Id.ToString(),staff.Id.ToString());
                //模拟数据
                //IList<ReceptionChatDto> receptionChatDtoList = MockData.receptionChatDtoList;
                return View(staff);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 封停/解封服务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreStaffDetailLock(string id, bool islock)//string type,
        {
            try
            {
                //接口
                //ViewBag.UserName = CurrentUser.UserName;
                //MemberDto member = dzMembershipService.GetUserById(id);
                //member.IsLocked = islock;
                staffService.EnableStaff(StringHelper.CheckGuidID(id, "用户Id"), islock);
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
        /// 获取员工的订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult BusinessStoreStaffOrders(string id)
        {
            try
            {
                ViewData["id"] = id;
                Staff staff = staffService.GetOne(StringHelper.CheckGuidID(id, "员工Id"));
                //MemberDto member = dzMembershipService.GetUserById(id);
                IList<ServiceOrder> serviceOrderList = serviceOrderService.GetOrders(new Ydb.Common.Specification.TraitFilter(), "", "", staff.Belongto.Id, id, DateTime.MinValue, DateTime.MinValue, Guid.Empty, "", "");
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