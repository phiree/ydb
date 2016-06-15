using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class verify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userId = Request.Params["userId"];
        string verifyCode = Request.Params["verifyCode"];
        DZMembershipProvider dz = Bootstrap.Container.Resolve<DZMembershipProvider>();
       DZMembership member=  dz.GetUserById(new Guid(userId));
       if (member == null)
       { Response.Write("请求参数有误,请确认URL地址是否有误."); }

       if (member.IsRegisterValidated == true)
       {
           Response.Write("您已经通过了邮箱验证,无须再次验证.");
       }
       if (verifyCode == member.RegisterValidateCode)
       {
           member.IsRegisterValidated = true;
           dz.UpdateDZMembership(member);
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