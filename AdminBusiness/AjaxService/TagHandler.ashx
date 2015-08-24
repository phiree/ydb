<%@ WebHandler Language="C#" Class="TagHandler" %>

using System;
using System.Web;
using Dianzhu.Model;
using Dianzhu.BLL;
public class TagHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";

        string action = context.Request["action"];
        BLLDZTag bllTag = new BLLDZTag();
        switch (action.ToLower())
        {
            case "add":
                string tagText = context.Request["tagText"];
                string serviceId = context.Request["serviceId"];
                string serviceTypeId = context.Request["serviceId"];
                string businessId = context.Request["serviceId"];
                DZTag newTag = bllTag.AddTag(tagText, serviceId, businessId, serviceTypeId);
                context.Response.Write(newTag.Id);
                break;
            case "delete": 
                  
                string tagId = context.Request["tagId"];

                 bllTag.DeleteTag(new Guid(tagId));
                
                break;
        }





    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}