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
        IList<Area> areaList = new List<Area> { new Area { Id = 2445 }, new Area { Id = 2446 }, new Area { Id = 2447 }, new Area { Id = 2448 }, new Area { Id = 2449 }, new Area { Id = 2450 } };
        MemberDto memberAgent = new MemberDto();
        public ActionResult assistant_validate()
        {
            try
            {
                IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto = dzMembershipService.GetVerifiedDZMembershipCustomerServiceByArea(areaList);
                for (int i = 1; i < 15; i++)
                {
                    DZMembershipCustomerServiceDto member = new DZMembershipCustomerServiceDto()
                    {
                        Id = Guid.NewGuid(),
                        DisplayName = "DisplayName" + i.ToString(),
                        Phone = "1363769130" + i.ToString(),
                        QQNumber = "50264711" + i.ToString(),
                        TimeCreated = DateTime.Now.AddDays(-12).AddHours(i),
                        ApplyTime = DateTime.Now.AddDays(-1).AddHours(i)
                    };
                    dicDto[Enum_ValiedateCustomerServiceType.NotVerifiedCustomerService].Add(member);
                }
                foreach (KeyValuePair<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> d in dicDto)
                {
                    TempData[d.Key.ToString()] = d.Value.Select(x => x.Id).ToString();
                }
                return View(dicDto);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult assistant_validate_info(string id,string type)
        {
            try
            {
                TempData["assistant_validate_info_id"] = id;
                TempData["assistant_validate_info_type"] = type;
                DZMembershipCustomerServiceDto member = dzMembershipService.GetDZMembershipCustomerServiceById(id);
                return View(member);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }

        public ActionResult assistant_validate_info_agree(string id)
        {
            Ydb.Common.Application.ActionResult result = dzMembershipService.VerifyDZMembershipCustomerService(id, true, "");
            if (result.IsSuccess)
            {
                return Content("Success");
            }
            else
            {
                Response.StatusCode = 400;
                return Content(result.ErrMsg);
            }
        }

        public ActionResult assistant_validate_info_refuse(string id,string memo)
        {
            Ydb.Common.Application.ActionResult result = dzMembershipService.VerifyDZMembershipCustomerService(id, false, memo);
            if (result.IsSuccess)
            {
                return Content("Success");
            }
            else
            {
                Response.StatusCode = 400;
                return Content(result.ErrMsg);
            }
        }

        public ActionResult assistant_validate_info_next()
        {
            string id=TempData["assistant_validate_info_id"].ToString();
            string type=TempData["assistant_validate_info_type"].ToString();
            var list =(List<Guid>)TempData[type];
            TempData[type] = list;
            int index = list.IndexOf(new Guid(id));
            if (index == list.Count - 1)
            {
                Response.StatusCode = 400;
                return Content("这已经是最后一个了！");
            }
            else
            {
                return RedirectToAction("./ assistant_validate_info?type="+type+"&id="+list[index+1].ToString());
            }

        }
    }
}