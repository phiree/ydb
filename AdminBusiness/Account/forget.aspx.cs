using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class forget : Dianzhu.Web.Common.BasePage
{
    DZMembershipProvider bllMember = Bootstrap.Container.Resolve<DZMembershipProvider>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.IsLocal)
        {
            lblMsg.Text = "不允许本地网站测试,避免服务器降低系统邮箱的评级";
        }
    }
    protected void btnRecover_Click(object sender, EventArgs e)
    {
        string username = tbxEmail.Text;
        DZMembership member = bllMember.GetUserByName(username);
        if (member == null)
        {

            lblMsg.Text = "不存在此用户";
            return;
        }
        Guid code=Guid.NewGuid();
        member.RecoveryCode = code;
        string recoveryUrl = "http://" + Request.Url.Authority + "/account/recovery.aspx";
        recoveryUrl += "?p=" +Server.UrlEncode(PHSuit.Security.Encrypt(username,false).Replace("+","kewudejiahao"))+ Config.pwssword_recovery_spliter+code;
        if (true||!Request.IsLocal)
        {
            bool sendSuccess = bllMember.SendRecoveryMail(username, recoveryUrl);
            if (sendSuccess)
            {
                lblMsg.Text = "密码重置邮件发送成功,请登录您的邮箱,按照提示操作";
                bllMember.UpdateDZMembership(member);
            }
            else
            {
                lblMsg.Text = "密码重置邮件发送失败,请联系客服";
            }
        }
        

       
        


    }
}