using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class membership_roles_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadList();
        }
    }
    Dianzhu.BLL.IdentityAccess.RoleService roleService = new Dianzhu.BLL.IdentityAccess.RoleService();
    private void LoadList()
    {
        gvRoleList.DataSource = roleService.GetAllRolesDto();
        gvRoleList.DataBind();
    }

    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {

    }
}