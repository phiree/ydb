using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Ydb.Finance.Application;
public partial class servicetype_Sharepoint : BasePage
{

    /// <summary>
    /// 服务类别的分成列表
    /// </summary>
    IServiceTypePointService bLLServiceTypePoint = Bootstrap.Container.Resolve<IServiceTypePointService>();
    BLLServiceType bllType = Bootstrap.Container.Resolve<Dianzhu.BLL.BLLServiceType>();
    protected void Page_Load(object sender, EventArgs e)
    {
         
        rptPoints.DataSource = bLLServiceTypePoint.GetAll();
        rptPoints.ItemDataBound += RptPoints_ItemDataBound;
        rptPoints.DataBind();
    }

    private void RptPoints_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            ServiceTypePointDto point = (ServiceTypePointDto)e.Item.DataItem;
            Literal liTypeName = e.Item.FindControl("liTypeName") as Literal;
            Literal liPoint = e.Item.FindControl("liPoint") as Literal;
            liTypeName.Text = point.ServiceTypeId;
            liPoint.Text = point.Point.ToString("0.00");
        }
    }
}
