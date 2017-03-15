<%@ WebHandler Language="C#" Class="TagHandler" %>

using System;
using System.Web;
 
using System.Web.Security;
    using Ydb.BusinessResource.Application;
    using Ydb.BusinessResource.DomainModel;
public class TagHandler : IHttpHandler,System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        //权限判断
        if (!AjaxAuth.authAjaxUser(context)){
            context.Response.StatusCode = 400;
            context.Response.Clear();
            context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"unlogin\"}");
            return;
        }

        

        context.Response.ContentType = "text/plain";

        string action = context.Request["action"];
        IDZTagService bllTag = Bootstrap.Container.Resolve<IDZTagService>();
        switch (action.ToLower())
        {
            case "add":
                string tagText = context.Request["tagText"];
                string serviceId = context.Request["serviceId"];
                string serviceTypeId = context.Request["serviceId"];
                string businessId = context.Request["serviceId"];
                string[] tags = tagText.Split(' ');
                string resultJson = "[";
                foreach(string tag in tags)
                { if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag)) { continue; }
                    DZTag newTag = bllTag.AddTag(tag, serviceId, businessId, serviceTypeId);
                    resultJson += "{\"tagId\":\""+newTag.Id+"\",\"tagText\":\""+newTag.Text+"\"},";
                }
                resultJson=resultJson.TrimEnd(',');
                resultJson += "]";
                //  context.Response.ContentType = "application/json";
                context.Response.Write(resultJson);
                break;
            case "delete":

                string tagId = context.Request["tagId"];

                bllTag.DeleteTag(new Guid(tagId));

                context.Response.Write("{\"result\":\""+false+"\",\"msg\":\"unlogin\"}");

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