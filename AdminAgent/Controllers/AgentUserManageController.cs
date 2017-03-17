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


namespace AdminAgent.Controllers
{
    public class AgentUserManageController : AgentBaseController
    {
        IIMUserStatusArchieveService imUserStatusArchieveService = Bootstrap.Container.Resolve<IIMUserStatusArchieveService>();
        IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
        IServiceOrderService serviceOrderService = Bootstrap.Container.Resolve<IServiceOrderService>();
        IComplaintService complaintService = Bootstrap.Container.Resolve<IComplaintService>();
        // GET: AgentUserManage
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
        /// 获取助理详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult assistant_detail(string id, string type)//
        {
            try
            {
                ViewData["id"] = id;
                ViewData["type"] = type;
                //接口
                ViewBag.UserName = CurrentUser.UserName;
                MemberDto member = dzMembershipService.GetUserById(id);
                ViewData["totalOnlineTime"] = imUserStatusArchieveService.GetUserTotalOnlineTime(member.Id.ToString());
                int totalOrderCount= serviceOrderService.GetServiceOrderCountWithoutDraft(CurrentUser.UserId, false);
                long totalDoneOrderCount = serviceOrderService.GetServiceOrderCountWithoutDraft(CurrentUser.UserId, false);
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
    }
}