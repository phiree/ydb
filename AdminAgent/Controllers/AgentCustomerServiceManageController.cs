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
        IList<Area> areaList = MockData.areaList;
        public ActionResult assistant_validate()
        {
            try
            {
                //模拟数据
                IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto = MockData.dicDto;
                //接口
                //IDictionary<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> dicDto = dzMembershipService.GetVerifiedDZMembershipCustomerServiceByArea(areaList);
                foreach (KeyValuePair<Enum_ValiedateCustomerServiceType, IList<DZMembershipCustomerServiceDto>> d in dicDto)
                {
                    TempData[d.Key.ToString()] = d.Value.Select(x => x.Id.ToString()).ToList();
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
                //接口
                //DZMembershipCustomerServiceDto member = dzMembershipService.GetDZMembershipCustomerServiceById(id);
                //模拟数据
                DZMembershipCustomerServiceDto member = MockData.GetDZMembershipCustomerServiceDtoById(id,type);
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
            var list =(List<string>)TempData[type];
            TempData[type] = list;
            int index = list.IndexOf(id);
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