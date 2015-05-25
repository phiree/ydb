using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class DZService_SelectType : System.Web.UI.Page
{
    BLLServiceType bllServiceType = new BLLServiceType();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    {
        IList<ServiceType> topType = bllServiceType.GetTopList();
        rptServiceType.DataSource = topType;
        rptServiceType.ItemDataBound += new RepeaterItemEventHandler(rptServiceType_ItemDataBound);
        rptServiceType.DataBind();

        
    }
    ServiceType currentType = null;
    void rptServiceType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ServiceType st = e.Item.DataItem as ServiceType;
        currentType = st;
        BindChildren(st,e.Item);
        
    }
   
    private void BindChildren(ServiceType st,RepeaterItem ri)
    {
         
        HyperLink hlServiceType = new HyperLink();
        hlServiceType.Font.Size = 20 - currentType.DeepLevel;
        hlServiceType.Style["display"] = "block";
        hlServiceType.Text = currentType.Name;
        hlServiceType.NavigateUrl = "edit.aspx?typeid=" + currentType.Id;
        
        ri.Controls.Add(hlServiceType);
        Repeater rpt = new Repeater();
        ri.Controls.Add(rpt);
        
        rpt.DataSource = currentType.Children;
        rpt.ItemDataBound += new RepeaterItemEventHandler(rpt_ItemDataBound);
        rpt.DataBind();
        
    }

    void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        currentType = e.Item.DataItem as ServiceType;
        BindChildren(currentType, e.Item);
    }
}