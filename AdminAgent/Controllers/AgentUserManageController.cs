using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.DomainModel.Enums;
using Ydb.InstantMessage.Application;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
using Ydb.Common.Specification;


namespace AdminAgent.Controllers
{
    public class AgentUserManageController : AgentBaseController
    {
        IIMUserStatusArchieveService imUserStatusArchieveService = Bootstrap.Container.Resolve<IIMUserStatusArchieveService>();
        IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
        IServiceOrderService serviceOrderService = Bootstrap.Container.Resolve<IServiceOrderService>();
        IComplaintService complaintService = Bootstrap.Container.Resolve<IComplaintService>();
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            try
            {
                ViewBag.UserName = CurrentUser.UserName;
                IDictionary<Enum_LockMemberType, IList<MemberDto>> dicDto = dzMembershipService.GetLockDZMembershipByArea(CurrentUser.AreaList,UserType.customer);
                return View(dicDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult UserDetail(string id, string type)//
        {
            try
            {
                ViewData["id"] = id;
                ViewData["type"] = type;
                //接口
                ViewBag.UserName = CurrentUser.UserName;
                MemberDto member = dzMembershipService.GetUserById(id);
                ViewData["totalOnlineTime"] = imUserStatusArchieveService.GetUserTotalOnlineTime(member.Id.ToString());
                int totalOrderCount= serviceOrderService.GetServiceOrderCountWithoutDraft(member.Id, false);
                long totalDoneOrderCount = serviceOrderService.GetOrdersCount("done","",Guid.Empty,null,DateTime.MinValue,DateTime.MinValue,member.Id,member.UserType.ToString(), "");
                ViewData["totalOrderCount"] = totalOrderCount;
                ViewData["totalDoneOrderCount"] = totalDoneOrderCount;
                ViewData["totalNotDoneOrderCount"] = totalOrderCount - totalDoneOrderCount;
                ViewData["totalComplaintCount"] = complaintService.GetComplaintsCountByOperator(member.Id.ToString());
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //ViewData["totalOnlineTime"] = MockData.totalOnlineTime;
                //ViewData["totalOrderCount"] = MockData.totalOrderCount;
                //ViewData["totalComplaintCount"] = MockData.totalComplaintCount;
                return View(member);
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
        public ActionResult UserDetailLock(string id, bool islock)//string type,
        {
            try
            {
                //接口
                ViewBag.UserName = CurrentUser.UserName;
                MemberDto member = dzMembershipService.GetUserById(id);
                member.IsLocked = islock;
                dzMembershipService.LockDZMembership(id, islock, "违规操作");
                //模拟数据
                //DZMembershipCustomerServiceDto member = MockData.GetLockDZMembershipCustomerServiceDtoById(id, type);
                //member.IsLocked = islock;
                //if (islock)
                //{
                //    member.LockReason = "违规操作";
                //}
                return View(member);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }


        /// <summary>
        /// 获取用户的历史订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult UserDetailOrders(string id)
        {
            try
            {
                ViewData["id"] = id;
                MemberDto member = dzMembershipService.GetUserById(id);
                IList<ServiceOrder> serviceOrderList = serviceOrderService.GetOrders(new TraitFilter(), "", "",Guid.Empty,null,DateTime.MinValue,DateTime.MinValue, member.Id, member.UserType.ToString(),"");
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