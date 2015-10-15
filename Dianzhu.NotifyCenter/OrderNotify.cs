using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ags = agsXMPP.protocol.client;
using Dianzhu.CSClient.IInstantMessage;
/// <summary>
/// Summary description for PayConfig
/// </summary>
namespace Dianzhu.NotifyCenter { 
public class OrderNotify
{
    private Dianzhu.CSClient.IInstantMessage.InstantMessage im=null;
    
    public OrderNotify(InstantMessage im)
    {
        this.im =  im;
            //
            // TODO: Add constructor logic here
            //

        }
    public   void SendOrderChangedNotify(Dianzhu.Model.ServiceOrder order)
    {

           
        ags.Message msg = new ags.Message();
        msg.SetAttribute("type", "headline");
        msg.Id = Guid.NewGuid().ToString();
        msg.From = "4f088d5c-be94-43bc-9644-a4d1008be129@ydban.cn";
        
        msg.To = order.Customer.Id.ToString();
        msg.Body = "订单状态已发生改变";

        var nodeActive = new agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
        msg.AddChild(nodeActive);
        var extNode = new agsXMPP.Xml.Dom.Element("ext");
        extNode.Namespace = "ihelper:cer:notce";
            var extOrderID = new agsXMPP.Xml.Dom.Element("orderID",order.Id.ToString());
            extNode.AddChild(extOrderID);
            var UserObj = new agsXMPP.Xml.Dom.Element("UserObj");

        UserObj.SetAttribute("UserID", order.CustomerService.Id.ToString());
        UserObj.SetAttribute("alias", order.CustomerService.DisplayName);
        UserObj.SetAttribute("imgUrl", order.CustomerService.AvatarUrl);
        msg.SetAttribute("type", "headline");
        extNode.AddChild(UserObj);
        msg.AddChild(extNode);


        im.SendMessage(msg.ToString());
        msg.To = order.CustomerService.Id.ToString() + "@ydban.cn";
        im.SendMessage(msg.ToString());


    }
}
}