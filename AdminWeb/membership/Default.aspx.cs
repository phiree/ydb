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
        long totalRecord;
        int currentPageIndex=1;
        string paramPage=Request.Params["page"];
        if(!string.IsNullOrEmpty(paramPage))
        {
         currentPageIndex=int.Parse(paramPage);
        }
        gvMember.DataSource = dzmp.GetAllDZMembers(currentPageIndex-1, pager.PageSize, out totalRecord);
        pager.RecordCount = (int)totalRecord;
        gvMember.DataBind();
    }
    
}