using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL;
using Dianzhu.Model;
using Dianzhu.DAL;
namespace Dianzhu.BLL
{
    public class BLLDZService
    {
        DALDZService iDALDZService;

        public BLLDZService(DALDZService iDALDZService)
        {
            this.iDALDZService = iDALDZService;
        }
        public BLLDZService()
            : this(new DALDZService())
        { }

        public IList<DZService> GetServiceByBusiness(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecords)
        {
            return iDALDZService.GetList(businessId, serviceTypeId, pageindex, pagesize, out totalRecords);
        }
        
        public DZService GetOne(Guid serviceId)
        {
            return iDALDZService.DalBase.GetOne(serviceId);
        }
        public void SaveOrUpdate(DZService service)
        {
            if (service.Id == Guid.Empty)
            {
                service.CreatedTime = DateTime.Now;
            }

            service.LastModifiedTime = DateTime.Now;
            iDALDZService.DalBase.SaveOrUpdate(service);
        }

    }
}
