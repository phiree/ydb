using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage : Dianzhu.Web.Common.BasePage
{
    log4net.ILog log = log4net.LogManager.GetLogger("Web.AdminBusiness.BasePage");

    MemberDto currentUser;
    BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
    bool needBusiness = true;
    public bool NeedBusiness { get; set; }
    public MemberDto CurrentUser
    {
        get { return currentUser; }

    }
    Business b = null;
    public Business CurrentBusiness
    {
        get
        {
            if (!needBusiness) { return null; }
            if (b != null) { return b; }

            string strBusinessId = Request["businessId"];

            if (!string.IsNullOrEmpty(strBusinessId))
            {
                Guid bid = new Guid(strBusinessId);
                b = bllBusiness.GetOne(bid);

            }
            else
            {
                log.Error("businessId为空或格式有误，request[\"business\"]=" + strBusinessId);
                Response.Redirect("/");
            }
            return b;
        }
    }


    public BasePage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    IDZMembershipService memebrService = Bootstrap.Container.Resolve<IDZMembershipService>();
    //如果没有登录,则跳转至登录界面
    protected override void OnLoad(EventArgs e)
    {
        if (currentUser == null)
        {
            MembershipUser mu = Membership.GetUser();
            if (mu == null)
            {

                if (Request.RawUrl.Contains("/m/"))
                {
                    Response.Redirect("/m/login.aspx?returnurl=" + HttpUtility.UrlEncode(Request.RawUrl), true);

                }
                else
                {
                    Response.Redirect("/login.aspx?returnurl=" + HttpUtility.UrlEncode(Request.RawUrl), true);
                }
            }
            currentUser  = memebrService.GetUserByName(mu.UserName);

            //if (currentUser == null)
            //{
            //    Response.Redirect("/error.aspx?msg=您不是商户管理员,不能登录", true);

            //}
        }
        base.OnLoad(e);
    }
}