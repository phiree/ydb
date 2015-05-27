using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class Staff_Default : BasePage
{
    BLLStaff bllStaff = new BLLStaff();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    {
        int total=30;
        Business b = ((BusinessUser)CurrentUser).BelongTo;
<<<<<<< HEAD
       
        //gvStaff.DataSource = bllStaff.GetList(b.Id, Guid.Empty, 1, 10, out total);

        gvStaff.DataSource = bllStaff.GetList(b.Id, Guid.Empty, 0, 10, out total);
        gvStaff.DataBind();

       
=======
 
        gvStaff.DataSource = bllStaff.GetList(b.Id, Guid.Empty, 0, 10, out total);
        gvStaff.DataBind();
 
>>>>>>> origin/master
    }
}