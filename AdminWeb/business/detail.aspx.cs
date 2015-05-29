using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class business_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        string paramId = Request.Params["id"];
        if (!string.IsNullOrEmpty(paramId))
        {
            BLLBusiness bllBusiness = new BLLBusiness();

            DetailsView1.DataSource = new List<Business> { bllBusiness.GetOne(new Guid(paramId)) };
            DetailsView1.DataBind();
        }
        else { 
            
        }

    }
}