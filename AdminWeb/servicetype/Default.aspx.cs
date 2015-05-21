using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class servicetype_Default : System.Web.UI.Page
{
    BLLServiceType bllServiceType = new BLLServiceType();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }
    private void Bind()
    {
        gvServiceType.DataSource = bllServiceType.GetAll();
        gvServiceType.DataBind();
    }
}