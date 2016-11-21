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
            ActionResult<ServiceOpenTimeForDay> result = dzServiceService.AddWorkTimeDay(storeID, serviceID, dayOfWeek, worktimeobj.startTime, worktimeobj.endTime, Convert.ToInt32(worktimeobj.maxCountOrder), worktimeobj.tag);

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
                throw new FormatException("修改的工作时间ID不能为空！");
            }
            if (!string.IsNullOrEmpty(worktimeobj.startTime))
            {
                try
                {
                    DateTime dtStart = Convert.ToDateTime("2016-06-20 " + worktimeobj.startTime.Trim());
                    worktimeobj.startTime = dtStart.ToString("HH:mm");
                }
                catch
                {
                    throw new FormatException("服务开始时间格式不正确！");
                }
            }
            if (!string.IsNullOrEmpty(worktimeobj.endTime))
            {
                try
                {
                    DateTime dtEnd = Convert.ToDateTime("2016-06-20 " + worktimeobj.endTime.Trim());
                    worktimeobj.endTime = dtEnd.ToString("HH:mm");
                }
                catch
                {
                    throw new FormatException("服务结束时间格式不正确！");
                }
            }
            int intCount = 0;
            if (!string.IsNullOrEmpty(worktimeobj.maxCountOrder))
            {
                if (!int.TryParse(worktimeobj.maxCountOrder, out intCount))
                {
                    throw new FormatException("该时间段的最大接单量必须为整数！");
                }
            }
            //DayOfWeek week1 = utils.CheckWeek(worktimeobj.week.ToString());
           ServiceDto service = dzServiceService.GetOne(utils.CheckGuidID(serviceID, "serviceID"));
            if (service == null)
            {
                throw new Exception("不存在该服务！");
            }

            if (service.Business.Id != utils.CheckGuidID(storeID, "storeID"))
            {
                throw new Exception("该服务不属于该店铺！");
            }
            if (service.Business.OwnerId.ToString() != customer.UserID)
            {
                throw new Exception("这不是你店铺的服务！");
            }
            Model.ServiceOpenTimeForDay sotDay = Mapper.Map<workTimeObj, Model.ServiceOpenTimeForDay>(worktimeobj);
            Model.ServiceOpenTimeForDay sotDay1 = new Model.ServiceOpenTimeForDay();
            Guid guidId = utils.CheckGuidID(workTimeID, "workTimeID");
            //sotDay.Id = guidId;
            int c = 0;
            string week1 = "";
            foreach (Model.ServiceOpenTime sotObj in service.OpenTimes)
            {
                //if (sotObj.DayOfWeek == week1)
                //{
                bool isConflict = false;
                foreach (Model.ServiceOpenTimeForDay d in sotObj.OpenTimeForDay)
                {
                    if (d.Id == guidId)
                    {
                        week1 = sotObj.DayOfWeek.ToString();
                        //d.CopyTo(sotDay1);
                        //if (sotDay.Tag != null && sotDay.Tag != sotDay1.Tag)
                        //{
                        //    sotDay1.Tag = sotDay.Tag;
                        //}
                        //if (sotDay.TimeStart != null && sotDay.TimeStart != sotDay1.TimeStart)
                        //{
                        //    sotDay1.TimeStart = sotDay.TimeStart;
                        //}
                        //if (sotDay.TimeEnd != null && sotDay.TimeEnd != sotDay1.TimeEnd)
                        //{
                        //    sotDay1.TimeEnd = sotDay.TimeEnd;
                        //}
                        //if (sotDay.MaxOrderForOpenTime != 0 && sotDay.MaxOrderForOpenTime != sotDay1.MaxOrderForOpenTime)
                        //{
                        //    sotDay1.MaxOrderForOpenTime = sotDay.MaxOrderForOpenTime;
                        //}
                        //if (sotDay.Enabled != sotDay1.Enabled)
                        //{
                        //    sotDay1.Enabled = sotDay.Enabled;
                        //}
                        if (string.IsNullOrEmpty(worktimeobj.tag) == false && worktimeobj.tag != d.Tag)
                        {
                            d.Tag = worktimeobj.tag;
                        }
                        if (worktimeobj.startTime != "" && worktimeobj.startTime != d.TimeStart)
                        {
                            d.TimeStart = worktimeobj.startTime;
                        }
                        if (worktimeobj.endTime != "" && worktimeobj.endTime != d.TimeEnd)
                        {
                            d.TimeEnd = worktimeobj.endTime;
                        }
                        if (d.TimeStart.CompareTo(d.TimeEnd) >= 0)
                        {
                            throw new FormatException("修改后服务开始时间大于等于结束时间！");
                        }
                        if (intCount != 0 && intCount != d.MaxOrderForOpenTime)
                        {
                            d.MaxOrderForOpenTime = intCount;
                        }
                        if (worktimeobj.bOpen != d.Enabled)
                        {
                            d.Enabled = worktimeobj.bOpen;
                        }
                        d.CopyTo(sotDay1);
                        c++;
                        break;
                    }
                    //else
                    //{
                    //    if (!(sotDay.PeriodStart >= d.PeriodEnd || sotDay.PeriodEnd < d.PeriodStart))
                    //    {
                    //        isConflict = true;
                    //    }
                    //}
                }
                if (c == 1)
                {
                    foreach (Model.ServiceOpenTimeForDay d in sotObj.OpenTimeForDay)
                    {
                        if (d.Id != guidId && !(sotDay.PeriodStart >= d.PeriodEnd || sotDay.PeriodEnd < d.PeriodStart))
                        {
                            isConflict = true;
                        }
                    }
                    if (isConflict)
                    {
                        throw new Exception("timeRepeat:服务时间段不能重合.ID=" + sotObj.Id + ";重合时间：" + sotDay.TimeStart + "-" + sotDay.TimeEnd);
                    }
                    break;
                }

                //}
            }
            if (c == 0)
            {
                throw new Exception("该服务时段不存在!");
            }
            else
            {
                //blltimeforday.Update(sotDay1);
            }
            worktimeobj = Mapper.Map<Model.ServiceOpenTimeForDay, workTimeObj>(sotDay1);
            worktimeobj.week = week1;
            return worktimeobj;
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
            Model.DZService service = dzServiceService.GetOne(utils.CheckGuidID(serviceID, "serviceID"));
            if (service == null)
            {
                throw new Exception("不存在该服务！");
            }
            if (service.Business.Id != utils.CheckGuidID(storeID, "storeID"))
            {
                throw new Exception("该服务不属于该店铺！");
            }
            if (service.Business.OwnerId.ToString() != customer.UserID)
            {
                throw new Exception("这不是你店铺的服务！");
            }
            Guid guidId = utils.CheckGuidID(workTimeID, "workTimeID");
            int c = 0;
            foreach (Model.ServiceOpenTime sotObj in service.OpenTimes)
            {
                foreach (Model.ServiceOpenTimeForDay sotdayObj in sotObj.OpenTimeForDay)
                {
                    if (guidId == sotdayObj.Id)
                    {
                        sotObj.OpenTimeForDay.Remove(sotdayObj);
                        //   blltimeforday.Delete(sotdayObj);
                        c++;
                        return "删除成功！";
                    }
                }
            }
            if (c == 0)
            {
                throw new Exception("该服务时段不存在！");
            }
            return worktimeobj;
        }

    }
}
