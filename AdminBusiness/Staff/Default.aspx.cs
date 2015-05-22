using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class Staff_Default : System.Web.UI.Page
{
    BLLStaff bllStaff = new BLLStaff();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void BindList()
    { 
      // gv.DataSource=bllStaff.
    }
}