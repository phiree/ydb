using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common.Domain;

namespace AdminAgent.Controllers
{
    public class AgentCustomerServiceManageController : Controller
    {
        IDZMembershipService dzMembershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
        IList<Area> areaList = new List<Area> { new Area { Id = 2445 }, new Area { Id = 2446 }, new Area { Id = 2447}, new Area { Id = 2448 }, new Area { Id = 2449}, new Area { Id = 2450 } };
        MemberDto memberAgent = new MemberDto();
        public ActionResult assistant_validate()
        {
            IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto = dzMembershipService.GetVerifiedDZMembershipCustomerServiceByArea(areaList);
            return View(dicDto);
        }
    }
}