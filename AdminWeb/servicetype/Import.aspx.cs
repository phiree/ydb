using PHSuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Finance.Application;

public partial class servicetype_Import : BasePage
{
    Dianzhu.BLL.BLLServiceType bllServiceType = Bootstrap.Container.Resolve<Dianzhu.BLL.BLLServiceType>();
    IServiceTypePointService serviceTypePointService = Bootstrap.Container.Resolve<IServiceTypePointService>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        IList< Dianzhu.Model.Finance.ServiceTypePoint> serviceTypePointList= bllServiceType.Import(fu.PostedFile.InputStream);
        IList<ServiceTypePointDto> serviceTypePointDtoList = new List<ServiceTypePointDto>();
        for (int i = 0; i < serviceTypePointList.Count; i++)
        {
            ServiceTypePointDto serviceTypePointDto = new ServiceTypePointDto();
            serviceTypePointDto.ServiceTypeId = serviceTypePointList[i].ServiceType.Id.ToString();
            serviceTypePointDto.Point = serviceTypePointList[i].Point;
            serviceTypePointDtoList.Add(serviceTypePointDto);
        }
        serviceTypePointService.SaveList(serviceTypePointDtoList);
        Notification.Show(Page, "", "导入成功", string.Empty);
        lblMsg.Text = "导入完成";
    }
}