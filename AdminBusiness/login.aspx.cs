using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using Dianzhu.RequestRestful;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
public partial class login : Dianzhu.Web.Common.BasePage // System.Web.UI.Page
{
   // DZMembershipProvider bllMembership = Bootstrap.Container.Resolve<DZMembershipProvider>();

       IDZMembershipService membershipService = Bootstrap.Container.Resolve<IDZMembershipService>();
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


       
       ValidateResult loginResult= membershipService.Login(tbxUserName.Text, tbxPassword.Text);
        if (loginResult.IsValidated)
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
                    CookieErrorTime.Value = "0";
                    Response.Cookies.Add(CookieErrorTime);
                    Response.Redirect("~/business/", true); 
                }
            }
            else {
                lblMsg.Text = "登录失败，请重试";
                Config.log.Error(res.msg);
                lblMsg.CssClass = "lblMsg lblMsgShow";
            }

        }
        else
        {

            CookieErrorTime.Value = (int.Parse( CookieErrorTime.Value ) + 1).ToString();
            Response.Cookies.Add(CookieErrorTime);

            lblMsg.Text = loginResult.ValidateErrMsg;
            lblMsg.CssClass = "lblMsg lblMsgShow";

            // PHSuit.Notification.Show(Page,"","登录失败",Request.RawUrl);
        }
    }

    public static RequestResponse ReqResponse(string username, string password)
    {
        RequestParams rp = new RequestParams();
        rp.method = "1";
        rp.url = Dianzhu.Config.Config.GetAppSetting("RestApiAuthUrl");
        //rp.url = "http://192.168.1.177:52554/api/v1/authorization";
        rp.content = "{\n\"loginName\":\"" + username + "\",\n\"password\":\"" + password + "\"\n}";
        rp = SetCommon.SetParams("ABc907a34381Cd436eBfed1F90ac8f823b", "2bdKTgh9SiNlGnSajt4E6c4w1ZoZJfb9ATKrzCZ1a3A=", rp);
        IRequestRestful req = new RequestRestful();
        RequestResponse res = req.RequestRestfulApi(rp);
        return res;
        

    }
}