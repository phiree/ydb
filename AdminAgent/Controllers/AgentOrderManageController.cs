using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.ApplicationService.ModelDto;
using Ydb.Common;
namespace AdminAgent.Controllers
{
    public class AgentOrderManageController : AgentBaseController
    {
        IOrdersService ordersService = Bootstrap.Container.Resolve<IOrdersService>();
        // GET: AgentOrderManagement
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
    }
}