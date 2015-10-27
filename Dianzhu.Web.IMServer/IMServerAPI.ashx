<%@ WebHandler Language="C#" Class="IMServerAPI" %>

using System;
using System.Web;

public class IMServerAPI : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string type = context.Request["type"];
        Dianzhu.CSClient.IInstantMessage.InstantMessage im
        =( Dianzhu.CSClient.IInstantMessage.InstantMessage) context.Application["im"];
        Dianzhu.NotifyCenter.IMNotify imNotify = new Dianzhu.NotifyCenter.IMNotify(im);
        switch (type.ToLower())
        {
            case "systemnotice":
                string sysMsg = context.Request["body"];
                imNotify.SendSysNoitification(sysMsg);
                break;
        }

        //get cslist, using xmpp

        //get order or create new order
        // return cs,and order 

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}