using PHSuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.BusinessResource.Application;
using Ydb.Finance.Application;
using Ydb.Common.Infrastructure;
using Ydb.Finance.DomainModel;

public partial class servicetype_Import : Page
{
   IServiceTypeService bllServiceType = Bootstrap.Container.Resolve<IServiceTypeService>();
    IServiceTypePointService serviceTypePointService = Bootstrap.Container.Resolve<IServiceTypePointService>();

    IExcelReader excelReader= Bootstrap.Container.Resolve<IExcelReader>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Ydb.ApplicationService.ExcelImporter.ServiceTypeImporter serviceTypeImporter= Bootstrap.Container.Resolve<Ydb.ApplicationService.ExcelImporter.ServiceTypeImporter>();
        serviceTypeImporter.Import(fu.PostedFile.InputStream);

        //IList<VMServiceTypeAndPoint> serviceTypePointList=new ServiceTypeAndPointAdapter().Adapt(excelReader.ReadFromExcel(fu.PostedFile.InputStream));
        //bllServiceType.SaveOrUpdateList(serviceTypePointList.Select(x=>x.ServiceType).ToList());
        //serviceTypePointService.SaveOrUpdateList(serviceTypePointList.Where(x=>x.TypePoint!=null).Select(x=>x.TypePoint).ToList());
        Notification.Show(Page, "", "导入成功", string.Empty);
        lblMsg.Text = "导入完成";
    }
}