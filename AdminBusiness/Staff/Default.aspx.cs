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
    BLLStaff bllStaff = Bootstrap.Container.Resolve<BLLStaff>();

    public string merchantID {
        get {
            return System.Web.Security.Membership.GetUser().ProviderUserKey.ToString();
        }
    }

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
        string strIndex = Request["page"];
        int index = 0;
        if (!string.IsNullOrEmpty(strIndex))
        {
            index = int.Parse(strIndex);
        }
       
        IList<Staff> staffList=bllStaff.GetListByBusiness(b.Id, index, pager.PageSize, out total);
        pager.RecordCount = total;
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