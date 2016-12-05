using Dianzhu.RequestRestful;

namespace Dianzhu.DemoClient
{
    public class RestfulApi
    {
        public static RequestResponse GetAssignCS(string username, string password, string apptype)
        {
            //用户认证
            RequestParams rp = new RequestParams();
            rp.method = "1";
            rp.url = Dianzhu.Config.Config.GetAppSetting("RestApiAuthUrl");
            //rp.url = "http://192.168.1.177:52554/api/v1/authorization";
            rp.content = "{\n\"loginName\":\"" + username + "\",\n\"password\":\"" + password + "\"\n}";

            string appName = GetAppNameByAppType(apptype);
            string appKey = GetAppKeyByAppType(apptype);

            rp = SetCommon.SetParams(appName, appKey, rp);
            IRequestRestful req = new Dianzhu.RequestRestful.RequestRestful();
            RequestResponse res = req.RequestRestfulApi(rp);

            if (res.code)
            {
                Newtonsoft.Json.Linq.JObject resObject = Newtonsoft.Json.JsonConvert.DeserializeObject(res.data) as Newtonsoft.Json.Linq.JObject;
                RequestParams rpCS = new RequestParams();
                rpCS.method = "0";
                //rpCS.url = "http://business.dev.ydban.cn:8041/api/v1/customerServices";
                rpCS.url = Dianzhu.Config.Config.GetAppSetting("RestApiSite")+"api/v1/customerServices";
                //rp.url = "http://192.168.1.177:52554/api/v1/authorization";/api/v1/customerServices
                rpCS.content = string.Empty; // "{\n\"loginName\":\"" + username + "\",\n\"password\":\"" + password + "\"\n}";
                rpCS.token = resObject["token"].ToString();
                rpCS = SetCommon.SetParams(appName, appKey, rpCS);
                IRequestRestful reqCS = new Dianzhu.RequestRestful.RequestRestful();
                RequestResponse resp = reqCS.RequestRestfulApi(rpCS);

                return resp;
            }

            return res;
        }

        internal static string GetAppNameByAppType(string appType)
        {
            string name = string.Empty;
            switch (appType.ToLower())
            {
                case "android":
                    name = "UA811Cd5343a1a41e4beB35227868541f8";
                    break;
                case "ios":
                    name = "MI354d5aaa55Ff42fba7716C4e70e015f2";
                    break;
            }

            return name;
        }

        internal static string GetAppKeyByAppType(string appType)
        {
            string key = string.Empty;
            switch (appType.ToLower())
            {
                case "android":
                    key = "WDcajjuVXA6TToFfm1MWhFFgn6bsXTt8VNsGLjcqGMg=";
                    break;
                case "ios":
                    key = "h7lVzFNKU5Nlp7iCSVIyfs2bEgCzA2aFnQsJwia8utE=";
                    break;
            }

            return key;
        }
    }
}
