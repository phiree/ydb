using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.BusinessResource.Application;
using Ydb.Common.Infrastructure;
using Ydb.Finance.Application;

namespace Ydb.ApplicationService.ExcelImporter
{
    /// <summary>
    /// excel导入ServiceType(属于BusinessResource领域和 ServiceTypePoint(属于Finance领域)
    /// </summary>
    public class ServiceTypeImporter
    {
        private readonly IExcelReader excelReader;
        private IServiceTypeService typeService;
        private IServiceTypePointService pointService;
        public ServiceTypeImporter(IExcelReader excelReader,IServiceTypeService typeService,IServiceTypePointService pointService)
        {
            this.excelReader = excelReader;
            this.typeService = typeService;
            this.pointService = pointService;
        }

        public void Import(Stream excelFileStream)
        {
            var datatable = excelReader.ReadFromExcel(excelFileStream);


            IList<VMServiceTypeAndPoint> serviceTypePointList = new ServiceTypeAndPointAdapter().Adapt(datatable);
             
            typeService.SaveOrUpdateList(serviceTypePointList.Select(x => x.ServiceType).ToList());
            pointService.SaveOrUpdateList(serviceTypePointList.Where(x => x.TypePoint != null).Select(x => x.TypePoint).ToList());


        }
    }
   
}
