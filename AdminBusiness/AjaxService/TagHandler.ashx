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
        BLLDZTag bllTag = Bootstrap.Container.Resolve<BLLDZTag>();
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