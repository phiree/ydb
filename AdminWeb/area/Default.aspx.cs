using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
/// <summary>
/// 展示行政区域, 可以考虑 用动态创建 reapeater的方式实现.
/// </summary>
public partial class area_Default : System.Web.UI.Page
{
 
    BLLArea bllArea = new BLLArea();
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Request["code"];
        if (string.IsNullOrEmpty(code))
        {

            BindList(bllArea.GetAreaProvince());
        }
        else
        {
            BindList(bllArea.GetSubArea(code));
        }
    }
    private void BindList(IList<Area> dataList)
    {
        rp_province.DataSource = dataList;
       // rp_province.ItemDataBound += new RepeaterItemEventHandler(rp_province_ItemDataBound);
        rp_province.DataBind();
    }

    void rp_province_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater rpt_city = e.Item.FindControl("rp_City") as Repeater;
        string areaCode=((Area)e.Item.DataItem).Code;
        rpt_city.DataSource = bllArea.GetSubArea(areaCode);
        rpt_city.ItemDataBound += new RepeaterItemEventHandler(rpt_city_ItemDataBound);
        rpt_city.DataBind();
    }

    void rpt_city_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater rpt_county = e.Item.FindControl("rpt_County") as Repeater;
        string areaCode = ((Area)e.Item.DataItem).Code;
        rpt_county.DataSource = bllArea.GetSubArea(areaCode);
        rpt_county.DataBind();
    }
}