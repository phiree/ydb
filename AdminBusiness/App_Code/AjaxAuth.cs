using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class AjaxAuth
{
    public AjaxAuth()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

    }

    public static Boolean authAjaxUser(HttpContext context) {
        bool b = true;

        NHibernateUnitOfWork.UnitOfWork.Start();

        var user = System.Web.Security.Membership.GetUser();
        // 权限判断
        if (user == null)
        {
            FormsAuthentication.SignOut();
            context.Session.Clear();  //从会话状态集合中移除所有的键和值
            context.Session.Abandon(); //取消当前会话

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            context.Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            context.Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();

            b = false;
        }
        NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork (null);

        return b;
    }
}