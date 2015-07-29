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
    DZMembershipProvider dzp = new DZMembershipProvider();
    BLLBusinessImage bllBi = new BLLBusinessImage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindChargePersonIdCards();
        }
    }

   protected void change_error(object sender, EventArgs e)
    {
        Exception ex= Server.GetLastError();
        Response.Redirect("~/error.aspx?msg="+ex.Message);
    }

   private void BindChargePersonIdCards()
   {

       rptChargePersonIdCards.DataSource = CurrentBusiness.BusinessChargePersonIdCards;
       rptChargePersonIdCards.DataBind();
   }

   protected void rptChargePersonIdCards_ItemCommand(object source, RepeaterCommandEventArgs e)
   {
       if (e.CommandName.ToLower().Trim() == "delete")
       {
           Guid imageId = new Guid(e.CommandArgument.ToString());
           bllBi.Delete(imageId);
           PHSuit.Notification.Show(Page, "", "删除成功", Request.RawUrl);
           BindChargePersonIdCards();
       }
   }
   protected void btnResendEmailVerify_Click(object sender, EventArgs e)
   { 
       string verifyUrl = "http://" + Request.Url.Authority + "/verify.aspx";
            verifyUrl += "?userId=" + CurrentUser.Id + "&verifyCode=" + CurrentUser.RegisterValidateCode;

            dzp.SendValidationMail(CurrentUser.Email, verifyUrl);
            Response.Redirect("/send_suc.aspx", true);
   }
 

}