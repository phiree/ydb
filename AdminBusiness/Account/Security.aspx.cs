using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class Account_Security :BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
    }

   protected void change_error(object sender, EventArgs e)
    {
        Exception ex= Server.GetLastError();
         
        Response.Redirect("~/error.aspx?msg="+ex.Message);
    }
    
     

}