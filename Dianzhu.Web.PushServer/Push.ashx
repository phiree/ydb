<%@ WebHandler Language="C#" Class="Push" %>

using System;
using System.Web;
using Dianzhu.Push;
public class Push : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string response = string.Empty;
        string appType = context.Request["client"];// ios, android, wp ...
        if (string.IsNullOrEmpty(appType))
        {
            response = "错误：未传入client参数";
        }
        else
        {
            switch (appType.ToLower())
            {
                case "ios":
                    string deviceToken = context.Request["deviceToken"];
                    string pushNum = context.Request["pushNum"];
                    string notificaitonSound = context.Request["notificaitonSound"];
                    string message = context.Request["message"];
                    IPush push = new PushIOS(deviceToken, Convert.ToInt16(pushNum), notificaitonSound);
                    push.Push(message);
                    break;
                case "android": break;
                case "wp": break;
                default:
                    response = "error:Unknow cliens.";
                    break;
            }
        }
        context.Response.Write(response);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}