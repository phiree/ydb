﻿<%@ WebHandler Language="C#" Class="Push" %>

using System;
using System.Web;
using Dianzhu.Push;
public class Push : IHttpHandler
{
    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.PushServer");
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string response = string.Empty;
        string appType = context.Request["client"];// ios, android, wp ...
        log.Debug("接受推送请求,来自"+appType);
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
                    log.Debug(string.Format("推送参数为:devicetoken_{0},pushnum{1},sound_{2},message_{3}", deviceToken, pushNum, notificaitonSound, message));
                    IPush push = new PushIOS(deviceToken, Convert.ToInt16(pushNum), notificaitonSound);
                    push.Push(message);
                    log.Debug("推送完成");
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