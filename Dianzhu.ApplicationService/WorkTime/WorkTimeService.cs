using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Application;
namespace Dianzhu.ApplicationService.WorkTime
{
    public class WorkTimeService : IWorkTimeService
    {
        IDZServiceService dzServiceService;



        public WorkTimeService(IDZServiceService dzServiceService)
        {
            this.dzServiceService = dzServiceService;

        }

        /// <summary>
        /// 新建工作时间
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="worktimeobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public workTimeObj PostWorkTime(string storeID, string serviceID, workTimeObj worktimeobj, Customer customer)
        {


            DayOfWeek dayOfWeek = utils.CheckWeek(worktimeobj.week.ToString());
            ActionResult<ServiceOpenTimeForDay> result = dzServiceService.AddWorkTime(storeID, serviceID, dayOfWeek, worktimeobj.startTime, worktimeobj.endTime, Convert.ToInt32(worktimeobj.maxCountOrder), worktimeobj.tag);

            if (!result.IsSuccess)
            {
                throw new Exception(result.ErrMsg);
            }
            return Mapper.Map<workTimeObj>(result.ResultObject);
        }

        /// <summary>
        /// 条件读取工作时间
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="worktime"></param>
        /// <returns></returns>
        public IList<workTimeObj> GetWorkTimes(string storeID, string serviceID, common_Trait_WorkTimeFiltering worktime)
        {
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("所属店铺不能为空！");
            }
            if (string.IsNullOrEmpty(serviceID))
            {
                throw new FormatException("所属服务不能为空！");
            }
            List<workTimeObj> worktimeobjs = new List<workTimeObj>();
            DayOfWeek? dayOfWeek;
            if (string.IsNullOrEmpty(worktime.week))
            { dayOfWeek = null; }
            else
            {
                dayOfWeek = utils.CheckWeek(worktime.week);
            }

            IList<ServiceOpenTimeForDay> list = dzServiceService.GetWorkTimes(storeID, serviceID, dayOfWeek, worktime.startTime, worktime.endTime);



            return Mapper.Map<IList<workTimeObj>>(list);// worktimeobjs;
        }


        /// <summary>
        /// 统计工作时间的数量
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="worktime"></param>
        /// <returns></returns>
        public countObj GetWorkTimesCount(string storeID, string serviceID, common_Trait_WorkTimeFiltering worktime)
        {
            int count = GetWorkTimes(storeID, serviceID, worktime).Count;
            countObj countObj = new countObj { count = count.ToString() };

            return countObj;
        }

        /// <summary>
        /// 读取工作时间
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public workTimeObj GetWorkTime(string storeID, string serviceID, string workTimeID, Customer customer)
        {
            var serviceOpenTimeForDay = dzServiceService.GetWorkitem(storeID, serviceID, workTimeID);

            var worktimeobj = Mapper.Map<workTimeObj>(serviceOpenTimeForDay);

            return worktimeobj;
        }

        /// <summary>
        /// 更新工作时间信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <param name="worktimeobj"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public workTimeObj PatchWorkTime(string storeID, string serviceID, string workTimeID, workTimeObj worktimeobj, Customer customer)
        {

            DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), worktimeobj.week);
            
            ActionResult<ServiceOpenTimeForDay> modifyResult =
                   dzServiceService.ModifyWorkTimeDay(serviceID, dayOfWeek, workTimeID, worktimeobj.startTime,
                   worktimeobj.endTime, Convert.ToInt32(worktimeobj.maxCountOrder),worktimeobj.bOpen,worktimeobj.tag);
            if (modifyResult.IsSuccess)
            {
                worktimeobj = Mapper.Map<workTimeObj>(modifyResult.ResultObject);

                return worktimeobj;
            }
            else
            {
                throw new Exception(modifyResult.ErrMsg);
            }
        }

        /// <summary>
        /// 删除服务 工作时间信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="workTimeID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public object DeleteWorkTime(string storeID, string serviceID, string workTimeID, Customer customer)
        {
            //todo: refactor: 没用到 暂时注释
            //throw new NotImplementedException();
           
            if (string.IsNullOrEmpty(storeID))
            {
                throw new FormatException("所属店铺不能为空！");
            }
            if (string.IsNullOrEmpty(serviceID))
            {
                throw new FormatException("所属服务不能为空！");
            }
            if (string.IsNullOrEmpty(workTimeID))
            {
                throw new FormatException("删除的工作时间ID不能为空！");
            }
            workTimeObj worktimeobj = new workTimeObj();
           ServiceDto service = dzServiceService.GetOne(utils.CheckGuidID(serviceID, "serviceID"));
            if (service == null)
            {
                throw new Exception("不存在该服务！");
            }
            if (service.ServiceBusinessId != storeID)
            {
                throw new Exception("该服务不属于该店铺！");
            }
            if (service.ServiceBusinessOwnerId != customer.UserID)
            {
                throw new Exception("这不是你店铺的服务！");
            }
            // Guid guidId = utils.CheckGuidID(workTimeID, "workTimeID");
            dzServiceService.DeleteWorkTime(serviceID, workTimeID);
            //int c = 0;
            //foreach (Model.ServiceOpenTime sotObj in service.OpenTimes)
            //{
            //    foreach (Model.ServiceOpenTimeForDay sotdayObj in sotObj.OpenTimeForDay)
            //    {
            //        if (guidId == sotdayObj.Id)
            //        {
            //            sotObj.OpenTimeForDay.Remove(sotdayObj);
            //            //   blltimeforday.Delete(sotdayObj);
            //            c++;
            //            return "删除成功！";
            //        }
            //    }
            //}
            //if (c == 0)
            //{
            //    throw new Exception("该服务时段不存在！");
            //}
            return worktimeobj;
           
        }

    }
}
