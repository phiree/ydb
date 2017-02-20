using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Newtonsoft.Json;
using Dianzhu.BLL;
using Dianzhu.RequestRestful;
using Ydb.Membership.Application;

public partial class login : Dianzhu.Web.Common.BasePage // System.Web.UI.Page
{
    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();

    protected void Page_Load(object sender, EventArgs e)
    {
        //PHSuit.Logging.GetLog(Dianzhu.Config.Config.GetAppSetting("LoggerName")).Debug("login page");
        Config.log.Debug("loging");
        BrowserCheck.CheckVersion();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        HttpCookie CookieErrorTime = Request.Cookies["errorTime"];
        HttpCookie CookieApiToken = Request.Cookies["api_token"];

        if (CookieErrorTime == null)
        {
            HttpCookie errorTime = new HttpCookie("errorTime");
            errorTime.Value = "0";
            errorTime.Expires = DateTime.Now.AddDays(3);
            Response.Cookies.Add(errorTime);
        }


        string errorMsg;
        var isValid = memberService.ValidateUser(tbxUserName.Text, tbxPassword.Text, true);
        if (isValid.IsValidated)
        {
            bool rememberMe = savePass.Checked;
            RequestResponse res = ReqResponse(tbxUserName.Text, tbxPassword.Text);
        
            FormsAuthentication.SetAuthCookie(tbxUserName.Text, rememberMe);

            if (CookieApiToken == null)
            {
                CookieApiToken = new HttpCookie("api_token");
                CookieApiToken.Expires = DateTime.Now.AddDays(3);
            }

            if (res.code)
            {
                RequestToken resObj = JsonConvert.DeserializeObject<RequestToken>(res.data);
                CookieApiToken.Value = resObj.token;
                Response.Cookies.Add(CookieApiToken);

                if (Request.RawUrl.Contains("/m/"))
                {
                    Response.Redirect("~/m/account/", true);
                }
                else
                {
                    HttpCookie CookieErrorTimeInit = new HttpCookie("errorTime");
                    CookieErrorTimeInit.Value = "0";
                    Response.Cookies.Add(CookieErrorTimeInit);
                    Response.Redirect("~/business/", true);
                }
            }
            else {
                lblMsg.Text = "登录失败，请重试";
                lblMsg.CssClass = "lblMsg lblMsgShow";
            }

        }
        else
        {
            if (CookieErrorTime == null) { CookieErrorTime = new HttpCookie("errorTime"); CookieErrorTime.Value = "0"; }
            CookieErrorTime.Value = (int.Parse( CookieErrorTime.Value ) + 1).ToString();
            Response.Cookies.Add(CookieErrorTime);

            lblMsg.Text = isValid.ValidateErrMsg;// "用户名或密码错误";
            lblMsg.CssClass = "lblMsg lblMsgShow";

            // PHSuit.Notification.Show(Page,"","登录失败",Request.RawUrl);
        }
    }

   
    public delegate void ParameterizedThreadStart(string username, string password);

    public static RequestResponse ReqResponse(string username, string password)
    {
        RequestParams rp = new RequestParams();
        rp.method = "1";
        rp.url = Dianzhu.Config.Config.GetAppSetting("RestApiAuthUrl");
        //rp.url = "http://192.168.1.177:52554/api/v1/authorization";
        rp.content = "{\n\"loginName\":\"" + username + "\",\n\"password\":\"" + password + "\"\n}";
        rp = SetCommon.SetParams("ABc907a34381Cd436eBfed1F90ac8f823b", "2bdKTgh9SiNlGnSajt4E6c4w1ZoZJfb9ATKrzCZ1a3A=", rp);
        IRequestRestful req = new RequestRestful();

        RequestResponse res = new RequestResponse();

        //测试userToken重复保存问题
        //System .Threading.Thread t1 = new System.Threading.Thread(() => ReqResponse1(username, password));
        //System.Threading.Thread t2 = new System.Threading.Thread(() => ReqResponse1(username, password));
        //t1.Start();
        //t2.Start();
        res = req.RequestRestfulApi(rp);
        return res;
        

    }

    //测试userToken重复保存问题
    //private static void ReqResponse1(object username, object password)
    //{
    //    RequestParams rp = new RequestParams();
    //    rp.method = "1";
    //    rp.url = Dianzhu.Config.Config.GetAppSetting("RestApiAuthUrl");
    //    //rp.url = "http://192.168.1.177:52554/api/v1/authorization";
    //    rp.content = "{\n\"loginName\":\"" + username + "\",\n\"password\":\"" + password + "\"\n}";
    //    rp = SetCommon.SetParams("ABc907a34381Cd436eBfed1F90ac8f823b", "2bdKTgh9SiNlGnSajt4E6c4w1ZoZJfb9ATKrzCZ1a3A=", rp);
    //    IRequestRestful req = new RequestRestful();
    //    RequestResponse res = new RequestResponse();
    //    res = req.RequestRestfulApi(rp);
    //}


}