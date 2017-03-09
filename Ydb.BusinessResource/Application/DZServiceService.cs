using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;
using Ydb.BusinessResource.Infrastructure;
using Ydb.Common.Application;
using Ydb.Common.Domain;
using Ydb.BusinessResource.Infrastructure.YdbNHibernate.UnitOfWork;

namespace Ydb.BusinessResource.Application
{
    public class DZServiceService : IDZServiceService
    {
        IRepositoryDZService repositoryDZService;
        IRepositoryDZTag repositoryDZTag;

        //todo:refactor: 应该是valueobject 不需要对应的repo
        IRepositoryServiceOpenTimeForDay repositoryOpenTimeForDay;


        public DZServiceService(IRepositoryDZService repositoryDZService,
        IRepositoryDZTag repositoryDZTag, IRepositoryServiceOpenTimeForDay repositoryOpenTimeForDay)
        {
            this.repositoryDZService = repositoryDZService;
            this.repositoryDZTag = repositoryDZTag;
            this.repositoryOpenTimeForDay = repositoryOpenTimeForDay;
        }


        [UnitOfWork]
        public IList<DZService> GetServiceByBusiness(Guid businessId, int pageindex, int pagesize, out int totalRecords)
        {
            return repositoryDZService.GetList(businessId, pageindex, pagesize, out totalRecords);
        }
        [UnitOfWork]
        public IList<DZService> GetOtherServiceByBusiness(Guid businessId, Guid serviceId, int pageindex, int pagesize, out int totalRecords)
        {
            return repositoryDZService.GetOtherList(businessId, serviceId, pageindex, pagesize, out totalRecords);
        }

        [UnitOfWork]
        public virtual ServiceDto GetOne(Guid serviceId)
        {
            DZService service = repositoryDZService.FindById(serviceId);
            ServiceDto serviceDto = AutoMapper.Mapper.Map<ServiceDto>(service);
            return serviceDto;
        }
        public virtual ServiceOpenTimeForDay GetWorkTime(Guid serviceId, DateTime targetTime)
        {
            DZService service = repositoryDZService.FindById(serviceId);
            string getOpenTimeErrMsg;
            ServiceOpenTime openTime = service.GetWorkDay(targetTime.DayOfWeek, out getOpenTimeErrMsg);
            ServiceOpenTimeForDay openTimeForDay = openTime.GetItem(targetTime.ToString("HH:mm"));

            return openTimeForDay;
            //return new ServiceOpenTimeDto
            //{
            //    Date = targetTime.Date,
            //    MaxOrderForDay = openTime.MaxOrderForDay,
            //    MaxOrderForPeriod = openTimeForDay.MaxOrderForOpenTime
            //,
            //    PeriodBegin = openTimeForDay.TimePeriod.StartTime.TimeValue,
            //    PeriodEnd = openTimeForDay.TimePeriod.EndTime.TimeValue
            //};


        }
        public DZService GetOneByBusAndId(Business business, Guid svcId)
        {
            return repositoryDZService.GetOneByBusAndId(business, svcId);
        }
        public int GetSumByBusiness(Business business)
        {
            return repositoryDZService.GetSumByBusiness(business);
        }
        public virtual IList<ServiceType> GetServiceTypeListByBusiness(Guid businessId)
        {
            int totalRecord;
            IList<DZService> businessServices = repositoryDZService.GetList(businessId, 0, 9999, out totalRecord);
            IList<ServiceType> serviceTypeList = businessServices.Select(x => x.ServiceType.TopType).Distinct().ToList();
            return serviceTypeList;
        }
        [Obsolete("需要重构")]
        [UnitOfWork]
        public void Save(DZService service)
        {
            repositoryDZService.Add(service);
        }
        public void Update(DZService service)
        {
            repositoryDZService.Update(service);
        }
        public void SaveOrUpdate(DZService service, out ValidationResult validationResult)
        {
            ValidatorDZService v = new ValidatorDZService();
            validationResult = v.Validate(service);
            bool isValid = validationResult.IsValid;

            if (!isValid) return;

            if (service.Id == Guid.Empty)
            {
                service.CreatedTime = DateTime.Now;
                repositoryDZService.Add(service);
            }
            else
            {
                service.LastModifiedTime = DateTime.Now;

                repositoryDZService.Update(service);
            }


        }

        public IList<DZService> GetAll()
        {
            return repositoryDZService.Find(x => true);
        }
        public void Delete(DZService dz)
        {
            repositoryDZService.Delete(dz);
        }
        public IList<DZService> SearchService(string name, decimal priceMin, decimal priceMax, Guid typeId, DateTime datetime, double lng, double lat, int pageIndex, int pagesize, out int total)
        {
            return repositoryDZService.SearchService(name, priceMin, priceMax, typeId, datetime, lng, lat, pageIndex, pagesize, out total);
        }

        /// <summary>
        /// 获取当前服务的标签
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public IList<DZTag> GetServiceTags(Guid serviceId)
        {
            return repositoryDZTag.GetTagsForService(serviceId);
        }

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="typeId"></param>
        /// <param name="strName"></param>
        /// <param name="introduce"></param>
        /// <param name="startAt"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public IList<ServiceDto> GetServices(TraitFilter filter, Guid typeId, string strName, string introduce, decimal startAt, Guid storeID)
        {
            var where = PredicateBuilder.True<DZService>();
            where = where.And(x => x.IsDeleted == false);
            if (typeId != Guid.Empty)
            {
                where = where.And(x => x.ServiceType.Id == typeId);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Business.Id == storeID);
            }
            if (strName != null && strName != "")
            {
                where = where.And(x => x.Name == strName);
            }
            if (introduce != null && introduce != "")
            {
                where = where.And(x => x.Description.Contains(introduce));
            }
            if (startAt != -1)
            {
                where = where.And(x => x.MinPrice == startAt);
            }

            DZService baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repositoryDZService.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? repositoryDZService.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList()
                : repositoryDZService.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();


            return AutoMapper.Mapper.Map<IList<ServiceDto>>(list);
        }

        /// <summary>
        /// 统计服务的数量
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="strName"></param>
        /// <param name="introduce"></param>
        /// <param name="startAt"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public long GetServicesCount(Guid typeId, string strName, string introduce, decimal startAt, Guid storeID)
        {
            var where = PredicateBuilder.True<DZService>();
            where = where.And(x => x.IsDeleted == false);
            if (typeId != Guid.Empty)
            {
                where = where.And(x => x.ServiceType.Id == typeId);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Business.Id == storeID);
            }
            if (strName != null && strName != "")
            {
                where = where.And(x => x.Name == strName);
            }
            if (introduce != null && introduce != "")
            {
                where = where.And(x => x.Description.Contains(introduce));
            }
            if (startAt != -1)
            {
                where = where.And(x => x.MinPrice == startAt);
            }
            long count = repositoryDZService.GetRowCount(where);
            return count;
        }

        /// <summary>
        ///  读取服务 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public ServiceDto GetService(Guid storeID, Guid serviceID)
        {
            var where = PredicateBuilder.True<DZService>();
            where = where.And(x => x.IsDeleted == false);
            if (serviceID != Guid.Empty)
            {
                where = where.And(x => x.Id == serviceID);
            }
            if (storeID != Guid.Empty)
            {
                where = where.And(x => x.Business.Id == storeID);
            }
            DZService dzservie = repositoryDZService.FindOne(where);
            return AutoMapper.Mapper.Map<ServiceDto>(dzservie);
        }

        /// <summary>
        /// 添加一个工作时间段
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="weekday"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <param name="maxOrder"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [UnitOfWork]
        public ActionResult<ServiceOpenTimeForDay> AddWorkTime(string storeId, string serviceId, DayOfWeek weekday, string timeBegin, string timeEnd, int maxOrder, string tag)
        {
            var result = new ActionResult<ServiceOpenTimeForDay>();
            DZService service = repositoryDZService.FindById(new Guid(serviceId));
            if (service == null)
            {
                throw new Exception("不存在该服务！");
            }
            if (service.Business.Id.ToString() != storeId)
            {
                throw new Exception("该服务不属于该店铺！");
            }
            try
            {

              ServiceOpenTimeForDay openTimeForDay=  service.AddWorkTime(weekday, timeBegin, timeEnd, maxOrder, tag);
                result.ResultObject = openTimeForDay;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMsg = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 修改一个工作时间段
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="workTimeId"></param>
        /// <param name="timeBegin"></param>
        /// <param name="endtime"></param>
        /// <param name="maxOrder"></param>
        /// <returns></returns>
        [Obsolete("WorkTimeId参数应该取消,因为工作时间是vo")]
        [UnitOfWork]
        public ActionResult<ServiceOpenTimeForDay> ModifyWorkTimeDay(string serviceId,DayOfWeek dayOfWeek, string workTimeId,string timeBegin,string endtime,int? maxOrder,bool? isOpen,string tag)
        {
            var workTime = repositoryOpenTimeForDay.FindById(new Guid(workTimeId));
            dayOfWeek = workTime.ServiceOpenTime.DayOfWeek;
            ActionResult<ServiceOpenTimeForDay> result = new ActionResult<ServiceOpenTimeForDay>();
            string errMsg;
            DZService service = repositoryDZService.FindById(new Guid(serviceId));
 
          
           
            try
            {
              if(timeBegin!=null&&endtime!=null)
                { 
                service.ModifyWorkTimePeriod(dayOfWeek,  workTime.TimePeriod,
                    new TimePeriod(new Time(timeBegin), new Time(endtime)),   out errMsg);
                    workTime.TimePeriod = new TimePeriod(new Time(timeBegin), new Time(endtime));
                }
                if (isOpen != null)
                {
                    service.ModifyWorkTimeEnable(dayOfWeek,workTime.TimePeriod, isOpen.Value);
                }
                if (tag != null)
                {
                    service.ModifyWorkTimeTag(dayOfWeek, workTime.TimePeriod, tag);
                }
                if (maxOrder != null)
                {
                    service.ModifyWorkTimeMaxOrder(dayOfWeek, workTime.TimePeriod, maxOrder.Value);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMsg = ex.Message;

            }
 
            return result;
        }
       
        /// <summary>
        /// 查询工作时间项
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="serviceID"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <returns></returns>
        public IList<ServiceOpenTimeForDay> GetWorkTimes(string storeID, string serviceID, DayOfWeek? dayOfWeek, string timeBegin, string timeEnd)
        {
            IList<ServiceOpenTimeForDay> list = new List<ServiceOpenTimeForDay>();
            DZService service = repositoryDZService.FindById(new Guid(serviceID));
            if (service == null)
            {
                throw new Exception("不存在该服务！");
            }
            if (service.Business.Id.ToString() != storeID)
            {
                throw new Exception("该服务不属于该店铺！");
            }
            foreach (var openTime in service.OpenTimes)
            {
                if (dayOfWeek == null || openTime.DayOfWeek == dayOfWeek)
                {
                    foreach (var openTimeForDay in openTime.OpenTimeForDay)
                    {
                        if (!string.IsNullOrEmpty(timeBegin) && timeBegin != openTimeForDay.TimePeriod.StartTime.ToString())
                        {
                            continue;
                        }
                        if (!string.IsNullOrEmpty(timeEnd) && timeEnd != openTimeForDay.TimePeriod.EndTime.ToString())
                        {
                            continue;
                        }
                        list.Add(openTimeForDay);
                    }
                }
            }
            return list;
        }

        public ServiceOpenTimeForDay GetWorkitem(string storeID, string serviceID, string workTimeID)
        {
            DZService service = repositoryDZService.FindById(new Guid(serviceID));
            if (service == null)
            {
                throw new Exception("不存在该服务！");
            }
            if (service.Business.Id.ToString() != storeID)
            {
                throw new Exception("该服务不属于该店铺！");
            }
            ServiceOpenTimeForDay openTimeForDay=  repositoryOpenTimeForDay.FindById(new Guid(workTimeID));
            if (openTimeForDay == null)
            {
                throw new Exception("该工作时间不存在！");
            }
            foreach (var openTime in service.OpenTimes)
            {
                if (openTimeForDay.ServiceOpenTime.Id == openTime.Id)
                {
                    openTimeForDay.ServiceOpenTime = openTime;
                }
            }
            return openTimeForDay;
        }

        [Obsolete]
        [UnitOfWork]
        public void DeleteWorkTime(string serviceId, string workTimeId)
        {
           var workTime= repositoryOpenTimeForDay.FindById(new Guid(workTimeId));
            var service = repositoryDZService.FindById(new Guid(serviceId));
            service.DeleteWorkTime(workTime.ServiceOpenTime.DayOfWeek,workTime.TimePeriod);
        }
        public ServiceOpenTimeForDay GetWorkitem(  string workTimeID)
        {
            ServiceOpenTimeForDay openTimeForDay = repositoryOpenTimeForDay.FindById(new Guid(workTimeID));

            return openTimeForDay;
        }
        public DZService GetOne2(Guid serviceId)
        {
          return  repositoryDZService.FindById(serviceId);
        }
    }
}
