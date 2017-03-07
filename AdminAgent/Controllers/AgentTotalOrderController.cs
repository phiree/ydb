using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ydb.ApplicationService.Application.AgentService;
using Ydb.ApplicationService.ModelDto;


namespace AdminAgent.Controllers
{
    public class AgentTotalOrderController : Controller
    {
        // GET: AgentTotalOrder
        public ActionResult total_order()
        {
            IOrdersService ordersService = Bootstrap.Container.Resolve<IOrdersService>();
            IList<string> areaList = MockData.areaIdList;
            try
            {
                //接口
                //ViewData["NewOrderNumber"] = ordersService.GetCountOfNewOrdersYesterdayByArea(areaList);
                //ViewData["AllOrderNumber"] = ordersService.GetCountOfAllOrdersByArea(areaList,enum_IsDone.None);
                //ViewData["AllDoneOrderNumber"] = ordersService.GetCountOfAllOrdersByArea(areaList, enum_IsDone.OrderIsDone);
                //ViewData["AllNotDoneOrderNumber"] = ordersService.GetCountOfAllOrdersByArea(areaList, enum_IsDone.OrderNotDone);
                //ViewData["YearOrderNumber"] = ordersService.GetStatisticsOrderRatioYearOnYear(areaList);
                //ViewData["MonthOrderNumber"] = ordersService.GetStatisticsOrderRatioMonthOnMonth(areaList);
                
                //模拟数据
                ViewData["NewOrderNumber"] = MockData.NewOrderNumber;
                ViewData["AllOrderNumber"] = MockData.AllOrderNumber;
                ViewData["AllDoneOrderNumber"] = MockData.AllDoneOrderNumber;
                ViewData["AllNotDoneOrderNumber"] = MockData.AllNotDoneOrderNumber;
                ViewData["YearOrderNumber"] = MockData.YearOrderNumber;
                ViewData["MonthOrderNumber"] = MockData.MonthOrderNumber;
                return View();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Content(ex.Message);
            }
        }
    }
}