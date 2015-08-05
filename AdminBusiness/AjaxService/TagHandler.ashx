<%@ WebHandler Language="C#" Class="TagHandler" %>

using System;
using System.Web;
using Dianzhu.Model;
using Dianzhu.BLL;
public class TagHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";
        
        string tagText = context.Request["tagText"];
        string serviceId = context.Request["serviceId"];
        string serviceTypeId = context.Request["serviceId"];
        string businessId = context.Request["serviceId"];
        BLLDZTag bllTag = new BLLDZTag();
      DZTag newTag=  bllTag.AddTag(tagText, serviceId, businessId, serviceTypeId);
      context.Response.Write(newTag.Id);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}