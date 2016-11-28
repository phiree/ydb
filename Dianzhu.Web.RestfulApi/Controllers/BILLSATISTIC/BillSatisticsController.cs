using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.BILLSATISTIC
{
    public class BillSatisticsController : ApiController
    {
        private ApplicationService.BillSatistic.IBillSatisticService ibill = null;
        public BillSatisticsController()
        {
            ibill = Bootstrap.Container.Resolve<ApplicationService.BillSatistic.IBillSatisticService>();
        }

        /// <summary>
        /// 根据日期统计账单结果
        /// </summary>
        /// <param name="billfilter"></param>
        /// <returns></returns>
        [Route("api/v1/billStatement")]
        public IHttpActionResult GetBillSatistics( [FromUri]common_Trait_BillFiltering billfilter)
        {
            try
            {
                if (billfilter == null)
                {
                    billfilter = new common_Trait_BillFiltering();
                }
                return Json(ibill.GetBillSatistics(billfilter, GetRequestHeader.GetTraitHeaders("get/billStatement")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 根据日期统计账单结果
        /// </summary>
        /// <param name="billfilter"></param>
        /// <returns></returns>
        [Route("api/v1/monthBillStatement")]
        public IHttpActionResult GetMonthBillStatement([FromUri]common_Trait_BillFiltering billfilter)
        {
            try
            {
                if (billfilter == null)
                {
                    billfilter = new common_Trait_BillFiltering();
                }
                return Json(ibill.GetMonthBillStatement(billfilter, GetRequestHeader.GetTraitHeaders("get/monthBillStatement")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 根据日期统计账单结果
        /// </summary>
        /// <param name="billfilter"></param>
        /// <returns></returns>
        [Route("api/v1/bills")]
        public IHttpActionResult GetBillList([FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_BillModelFiltering billfilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (billfilter == null)
                {
                    billfilter = new common_Trait_BillModelFiltering();
                }
                return Json(ibill.GetBillList(filter,billfilter, GetRequestHeader.GetTraitHeaders("get/bills/list")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
