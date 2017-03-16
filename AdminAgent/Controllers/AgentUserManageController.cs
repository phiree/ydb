using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.DomainModel.Enums;


namespace AdminAgent.Controllers
{
    public class AgentUserManageController : AgentBaseController
    {
        IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
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
    }
}