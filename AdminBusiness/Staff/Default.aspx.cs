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
        Business b = CurrentBusiness; 
        IList<Staff> staffList=bllStaff.GetList(b.Id, Guid.Empty, 0, 10, out total);
        rptStaff.DataSource = staffList;
        rptStaff.DataBind();

        
    }
    protected void rptStaff_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            Guid id = new Guid(e.CommandArgument.ToString());
            Staff staff=bllStaff.GetOne(id);
            bllStaff.Delete(staff);
            BindList();
        }
    }
}