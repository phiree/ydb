using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.Common.Application;
public partial class forget : Dianzhu.Web.Common.BasePage
{


    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }
    protected void btnRecover_Click(object sender, EventArgs e)
    {
        string username = tbxEmail.Text;

        ActionResult applyResult = memberService.ApplyRecovery(username,Request.Url.Scheme+"://"+ Request.Url.Authority);

        MemberDto member = memberService.GetUserByName(username);
        if (member == null)
        {

            lblMsg.Text = "不存在此用户";
            return;
        }

        if (applyResult.IsSuccess)
        {



            lblMsg.Text = "密码重置邮件发送成功,请登录您的邮箱,按照提示操作";
        }

        else
        {
            lblMsg.Text = "密码重置邮件发送失败,请联系客服";
        }
 
 
    }
}