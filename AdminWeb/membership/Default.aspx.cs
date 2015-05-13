using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class membership_Default : System.Web.UI.Page
{
    DZMembershipProvider dzmp = new DZMembershipProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    { 
        int totalRecord;
        gvMember.DataSource = dzmp.GetAll();
        gvMember.DataBind();
    }
    
}