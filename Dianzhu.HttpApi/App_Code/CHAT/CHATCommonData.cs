using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
/// <summary>
 ///orm接口 公用的类
/// </summary>

public class RespDataCHAT_chatObj
{
    public string id { get; set; }
    public string to { get; set; }
    public string from { get; set; }
    public string orderID { get; set; }
    public string body { get; set; }
    public string type { get; set; }
    public string date { get; set; }
    public RespDataCHAT_chatObj Adapt(ReceptionChat chat)
    {
        this.id = chat.Id.ToString();
        this.to = chat.To.Id.ToString();
        this.from = chat.From.Id.ToString();
        this.orderID = chat.ServiceOrder == null ? string.Empty : chat.ServiceOrder.Id.ToString();
        this.type = "chat";
        this.date = chat.SavedTime.ToString("yyyyMMddhhmmss");
        if (chat is ReceptionChatMedia)
        {
            this.body = ((ReceptionChatMedia)chat).MedialUrl;
        }
        else if (chat is ReceptionChatReAssign)
        {
            this.body = "(Reassign to)" + ((ReceptionChatReAssign)chat).ReAssignedCustomerService.DisplayName;
        }
        else {
            this.body = chat.MessageBody;
        }
        return this;

    }
}


