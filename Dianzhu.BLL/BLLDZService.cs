using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.Model;
using Dianzhu.DAL;
using Dianzhu.BLL.Validator;
using FluentValidation;
using FluentValidation.Results;
namespace Dianzhu.BLL
{
    public class BLLDZService
    {
        public DALDZService DALDZService = null;
        public BLLDZService() {DALDZService= DALFactory.DALDZService; }
        public BLLDZService(DALDZService dal)
        {
            DALDZService = dal;
        }
        public IList<DZService> GetServiceByBusiness(Guid businessId, int pageindex, int pagesize, out int totalRecords)
        {
            return DALDZService.GetList(businessId , pageindex, pagesize, out totalRecords);
        }

        public virtual   DZService GetOne(Guid serviceId)
        {
            return DALDZService.GetOne(serviceId);
        }
        public virtual IList<ServiceType> GetServiceTypeListByBusiness(Guid businessId)
        {
            int totalRecord;
            IList<DZService> businessServices = DALDZService.GetList(businessId, 0, 9999, out totalRecord);
            IList<ServiceType> serviceTypeList = businessServices.Select(x => x.ServiceType).Distinct().ToList();
            return serviceTypeList;
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
            }

            service.LastModifiedTime = DateTime.Now;
            DALDZService.SaveOrUpdate(service);
        }

        public IList<DZService> GetAll()
        {
            return DALDZService.GetAll<DZService>();
        }
        public void Delete(DZService dz)
        {
            DALDZService.Delete(dz);
        }
        public IList<DZService> Search(string keywords, int pageIndex, int pagesize, out int total)
        {
          return  DALDZService.SearchService(keywords, pageIndex, pagesize, out total);
        }
    }
}
