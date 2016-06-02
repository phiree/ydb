using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Text.RegularExpressions;
public partial class Account_Security :BasePage
{
    DZMembershipProvider dzp = Bootstrap.Container.Resolve<DZMembershipProvider>();
    BLLBusinessImage bllBi =Bootstrap.Container.Resolve<BLLBusinessImage>();
    protected void Page_Load(object sender, EventArgs e)
    {
        NeedBusiness = false;
        if (!IsPostBack)
        {
            
        }
    }

   protected void change_error(object sender, EventArgs e)
    {
        Exception ex= Server.GetLastError();
        Response.Redirect("~/error.aspx?msg="+ex.Message);
    }

 
   protected void btnResendEmailVerify_Click(object sender, EventArgs e)
   { 
       string verifyUrl = "http://" + Request.Url.Authority + "/verify.aspx";
            verifyUrl += "?userId=" + CurrentUser.Id + "&verifyCode=" + CurrentUser.RegisterValidateCode;

          bool sendSuccess=  dzp.SendValidationMail(CurrentUser.Email, verifyUrl);
          if (sendSuccess)
          {
              Response.Redirect("/send_suc.aspx", true);
          }
          else
          {
              Response.Redirect("error.aspx?msg=发送邮件失败,您可以在账户安全页面重新发送验证.");
          }
   }
 

}