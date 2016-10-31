using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using Ydb.Membership.Application;
using Ydb.Common.Application;
public partial class verify : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userId = Request.Params["userId"];
        string verifyCode = Request.Params["verifyCode"];

        IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
        ActionResult result = memberService.VerifyRegisterCode(verifyCode, userId);

     
       if (result.IsSuccess  )
       {
          
           Response.Write("验证成功. <a href='/'>返回首页</a>");
       }
       else
       {
           Response.Write("验证失败,请检查URL是否正确. <a href='/'>返回首页</a>");
       }
       


    }
    private void VerifyCode()
    {
        
    }
}