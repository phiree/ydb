using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class register : System.Web.UI.Page
{
    DZMembershipProvider bllMember = new DZMembershipProvider();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        System.Web.Security.MembershipCreateStatus createStatus;
        bllMember.CreateUser(tbxUserName.Text, string.Empty, string.Empty, tbxPwd.Text,
            out createStatus, (Dianzhu.Model.Enums.enum_UserType)(Convert.ToInt16(rblUserType.SelectedValue)));
        PHSuit.Notification.Show(this, "注册成功", "注册成功", string.Empty);
    }
}