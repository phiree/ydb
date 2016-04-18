using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
public partial class membership_roles_Edit : System.Web.UI.Page
{
    
    private bool isNew;
  
    string id;
    Dianzhu.BLL.IdentityAccess.RoleService roleService = new Dianzhu.BLL.IdentityAccess.RoleService();

    protected void Page_Load(object sender, EventArgs e)
    {
          id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            isNew = true;
            
        }
        else
        {
            
            if (!IsPostBack)
            {
                LoadForm();
            }
        }
        
    }
    private void LoadForm()
    {
        tbxRoleName.Text = roleService.GetRoleName(id);
    }
     
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        Dianzhu.BLL.IdentityAccess.RoleService roleService = new Dianzhu.BLL.IdentityAccess.RoleService();
        roleService.SaveOrUpdate(id, tbxRoleName.Text);
        lblMsg.Text = isNew ? "新建成功" : "修改成功";
    }
}