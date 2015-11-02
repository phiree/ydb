using System;
using System.Collections.Generic;
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
    BLLBusiness bllBusiness = new BLLBusiness();
    bool needBusiness = true;
    public bool NeedBusiness { get; set; }
    public DZMembership CurrentUser
    {
        get { return currentUser; }
         
    }
    Business b = null;
    public Business CurrentBusiness
    {
        get {
            if (!needBusiness) { return null; }
            if (b != null) { return b; }

            string strBusinessId = Request["businessId"];
            
            if (!string.IsNullOrEmpty(strBusinessId))
            {
                Guid bid = new Guid(strBusinessId);
                b = bllBusiness.GetOne(bid);

            }
            else {
                throw new Exception("没有这个店铺");
            }
            return b;
        }
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
                if(Request.RawUrl.Contains("/m/"))
                {
                    Response.Redirect("/m/login.aspx?returnurl=" + HttpUtility.UrlEncode(Request.RawUrl), true);
             
                }
                else{
                Response.Redirect("/login.aspx?returnurl="+HttpUtility.UrlEncode(Request.RawUrl), true);
                }
            }
            currentUser = mp.GetUserById((Guid)mu.ProviderUserKey);
            //if (currentUser == null)
            //{
            //    Response.Redirect("/error.aspx?msg=您不是商户管理员,不能登录", true);
               
            //}
        }
        base.OnLoad(e);
    }
}