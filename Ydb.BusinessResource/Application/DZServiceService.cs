using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common.Specification;

namespace Ydb.BusinessResource.Application
{
    public class DZServiceService : IDZServiceService
    {
        IRepositoryDZService repositoryDZService;
        IRepositoryDZTag repositoryDZTag;
        
        public DZServiceService(IRepositoryDZService repositoryDZService,
        IRepositoryDZTag repositoryDZTag)
        {
            this.repositoryDZService = repositoryDZService;
            this.repositoryDZTag = repositoryDZTag;
        }



        public IList<DZService> GetServiceByBusiness(Guid businessId, int pageindex, int pagesize, out int totalRecords)
        {
            return repositoryDZService.GetList(businessId , pageindex, pagesize, out totalRecords);
        }
        public IList<DZService> GetOtherServiceByBusiness(Guid businessId, Guid serviceId, int pageindex, int pagesize, out int totalRecords)
        {
            return repositoryDZService.GetOtherList(businessId, serviceId, pageindex, pagesize, out totalRecords);
        }

        public virtual   DZService GetOne(Guid serviceId)
        {
            return repositoryDZService.FindById(serviceId);
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
        public void Save(DZService service)
        {
            repositoryDZService.Add(service);
        }
        public void Update(DZService service)
        {
            repositoryDZService.Update(service);
        }
        public void SaveOrUpdate(DZService service,out ValidationResult validationResult)
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
            return repositoryDZService.Find(x => true) ;
        }
        public void Delete(DZService dz)
        {
            repositoryDZService.Delete(dz);
        }
        public IList<DZService> SearchService(string name, decimal priceMin, decimal priceMax, Guid typeId, DateTime datetime,double lng,double lat, int pageIndex, int pagesize, out int total)
        {
            return repositoryDZService.SearchService(name, priceMin, priceMax, typeId, datetime,lng,lat, pageIndex, pagesize, out total);
        }

        /// <summary>
        /// 获取当前服务的标签
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public IList<DZTag> GetServiceTags(DZService service)
        {
            return repositoryDZTag.GetTagsForService(service.Id);
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
        public IList<DZService> GetServices(TraitFilter filter, Guid typeId, string strName, string introduce, decimal startAt, Guid storeID)
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
                where = where.And(x => x.MinPrice==startAt);
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
            return list;
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
        public DZService GetService(Guid storeID, Guid serviceID)
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
            return dzservie;
        }
    }
}
