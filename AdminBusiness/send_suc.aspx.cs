using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Web.Security;
public partial class register : BasePage
{
     
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(Request.Params["id"]);
    }

    
    
    
}