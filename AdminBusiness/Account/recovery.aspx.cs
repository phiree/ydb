using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Web.Security;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common.Application;
public partial class ForgetPassword :Dianzhu.Web.Common.BasePage
{

   
    IDZMembershipService memberService= Bootstrap.Container.Resolve<IDZMembershipService>();
    MemberDto member;
    string recoveryString;
    protected void Page_Load(object sender, EventArgs e)
    {
          recoveryString = Request["p"];
        if (string.IsNullOrEmpty(recoveryString))
        {
            Response.Write("请求参数有误");
            Response.End();
        }
        string[] arr = recoveryString.Split(new string[]{ Config.pwssword_recovery_spliter},  StringSplitOptions.None);
        if (arr.Length != 2)
        {
            Response.Write("请求参数有误");
            Response.End();
        }
        
        

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
      
        string password = tbxPassword.Text;

       ActionResult result=  memberService.RecoveryPassword( recoveryString,  password );

        if (result.IsSuccess)
        {
            Response.Redirect("/account/recover_suc.aspx", true);
        }
        else
        {
            lblMsg.Text = result.ErrMsg;

        }



    }
}