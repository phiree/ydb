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
        
        public DALDZService DALDZService=DALFactory.DALDZService;
        

        public IList<DZService> GetServiceByBusiness(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecords)
        {
            return DALDZService.GetList(businessId, serviceTypeId, pageindex, pagesize, out totalRecords);
        }
        
        public DZService GetOne(Guid serviceId)
        {
            return DALDZService.GetOne(serviceId);
        }
        public void SaveOrUpdate(DZService service)
        {
            if (service.Id == Guid.Empty)
            {
                service.CreatedTime = DateTime.Now;
            }

            service.LastModifiedTime = DateTime.Now;
            DALDZService.SaveOrUpdate(service);
        }

    }
}
