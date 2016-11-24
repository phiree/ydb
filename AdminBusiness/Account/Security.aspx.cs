using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Text.RegularExpressions;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common.Application;
using Ydb.BusinessResource.Application;
public partial class Account_Security :BasePage
{
 //   DZMembershipProvider dzp = Bootstrap.Container.Resolve<DZMembershipProvider>();
    IDZMembershipService memberService= Bootstrap.Container.Resolve<IDZMembershipService>();
 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        NeedBusiness = false;
        
    }
    protected void change_error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Response.Redirect("~/error.aspx?msg=" + ex.Message);
    }


    protected void btnResendEmailVerify_Click(object sender, EventArgs e)
   { 
       
         ActionResult result=  memberService.ResendVerifyEmail(CurrentUser.UserName,Request.Url.Scheme+"://"+Request.Url.Authority);
          if (result.IsSuccess)
          {
              Response.Redirect("/send_suc.aspx", true);
          }
          else
          {
              Response.Redirect("error.aspx?msg="+result.ErrMsg);
          }
   }
 

}