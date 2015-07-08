using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage:System.Web.UI.Page
{
    DZMembership currentUser;
    public DZMembership CurrentUser
    {
        get { return currentUser; }
         
    }
 
    
    DZMembershipProvider mp = new DZMembershipProvider();
	public BasePage()
	{

		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    //如果没有登录,则跳转至登录界面
    protected override void OnLoad(EventArgs e)
    {
        if (currentUser == null)
        {
            MembershipUser mu = Membership.GetUser();
            if (mu == null)
            {

                Response.Redirect("/login.aspx?returnurl=" + HttpUtility.UrlEncode(Request.RawUrl), true);

            }
            else
            {
                currentUser = mp.GetUserById((Guid)mu.ProviderUserKey);
            }

 
        }
        base.OnLoad(e);
    }
}