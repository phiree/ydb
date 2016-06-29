﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.STAFF
{
    [HMACAuthentication]
    public class StaffsController : ApiController
    {
        private ApplicationService.Staff.IStaffService istaff = null;
        public StaffsController()
        {
            istaff = Bootstrap.Container.Resolve<ApplicationService.Staff.IStaffService>();
        }

        /// <summary>
        /// 新建员工 店铺的员工
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffobj"></param>
        /// <returns></returns>
        [Route("api/stores/{storeID}/staffs")]
        public IHttpActionResult PostStaff(string storeID, [FromBody]staffObj staffobj)
        {
            try
            {
                if (staffobj == null)
                {
                    staffobj = new staffObj();
                }
                return Json(istaff.PostStaff(storeID, staffobj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 条件读取员工
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="filter"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        [Route("api/stores/{storeID}/staffs")]
        public IHttpActionResult GetStaffs(string storeID, [FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_StaffFiltering stafffilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (stafffilter == null)
                {
                    stafffilter = new common_Trait_StaffFiltering();
                }
                return Json(istaff.GetStaffs(storeID, filter, stafffilter));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计服务员工的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="stafffilter"></param>
        /// <returns></returns>
        [Route("api/stores/{storeID}/staffs/count")]
        public IHttpActionResult GetStaffsCount(string storeID, [FromUri]common_Trait_StaffFiltering stafffilter)
        {
            try
            {
                if (stafffilter == null)
                {
                    stafffilter = new common_Trait_StaffFiltering();
                }
                return Json(istaff.GetStaffsCount(storeID, stafffilter));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 读取员工 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        [Route("api/stores/{storeID}/staffs/{staffID}")]
        public IHttpActionResult GetStaff(string storeID, string staffID)
        {
            try
            {
                return Json(istaff.GetStaff(storeID, staffID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <param name="staffobj"></param>
        /// <returns></returns>
        [Route("api/stores/{storeID}/staffs/{staffID}")]
        public IHttpActionResult PatchStaff(string storeID, string staffID, [FromBody]staffObj staffobj)
        {
            try
            {
                if (staffobj == null)
                {
                    staffobj = new staffObj();
                }
                return Json(istaff.PatchStaff(storeID, staffID, staffobj));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 删除员工 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="staffID"></param>
        /// <returns></returns>
        [Route("api/stores/{storeID}/staffs/{staffID}")]
        public IHttpActionResult DeleteStaff(string storeID, string staffID)
        {
            try
            {
                return Json(istaff.DeleteStaff(storeID, staffID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}