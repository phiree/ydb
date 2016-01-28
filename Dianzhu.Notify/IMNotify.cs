using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ags = agsXMPP.protocol.client;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
/// <summary>
/// 推送模块
/// </summary>
namespace Dianzhu.NotifyCenter
{
    /// <summary>
    /// 基于即时通讯 的推送.
    /// </summary>
    public class IMNotify
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Notify");
        private Dianzhu.CSClient.IInstantMessage.InstantMessage im = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="im">通讯接口</param>
        public IMNotify(InstantMessage im)
        {
            this.im = im;
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// 系统广播
        /// </summary>
        /// <param name="message">通知内容</param>
        /// <param name="scope">发送范围 all, 或者 某个具体的人物.</param>
        public void SendSysNotification(string message, string scope)
        {
           
            var extNode = new agsXMPP.Xml.Dom.Element("ext");
            extNode.Namespace = "ihelper:notice:system";

            ags.Message msg = BuildNotice(scope + "@broadcast." + im.Domain, message, extNode);
            im.SendMessage(msg.ToString());
        }
        public void SendSysNoitification(string message)
        {
            SendSysNotification(message, "all");
        }
        /// <summary>
        /// 订单消息
        /// </summary>
        /// <param name="order"></param>
        public void SendOrderChangedNotify(Dianzhu.Model.ServiceOrder order)
        {
            var extNode = new agsXMPP.Xml.Dom.Element("ext");
            extNode.Namespace = "ihelper:notice:order";
            var extOrderID = new agsXMPP.Xml.Dom.Element("orderID", order.Id.ToString());
            extNode.AddChild(extOrderID);
            var orderObj = new agsXMPP.Xml.Dom.Element("orderObj");

            orderObj.SetAttribute("title", order.ServiceName);
            orderObj.SetAttribute("status", order.OrderStatus.ToString());
            orderObj.SetAttribute("type", order.Service == null ? string.Empty : order.Service.ServiceType.Name);
            extNode.AddChild(orderObj);

            ags.Message msg= BuildNotice(
                 order.Customer.Id + "@" + im.Domain,
                 "订单状态已变为:" + order.OrderStatus,
                 extNode
                 );
            //发送给客户
            
            im.SendMessage(msg.ToString());

            ags.Message msgForCS = BuildNotice(
                  order.CustomerService.Id + "@" + im.Domain,
                  "订单状态已变为:" + order.OrderStatus,
                  extNode
                  );
            //发送给客服.
            im.SendMessage(msgForCS.ToString());
        }
        /// <summary>
        /// 促销消息
        /// </summary>
        /// <param name="promoteUrl"></param>
        public void SendPromote(string promoteUrl)
        {


            var extNode = new agsXMPP.Xml.Dom.Element("ext");
            extNode.Namespace = "ihelper:notice:system";
            var urlNode= new agsXMPP.Xml.Dom.Element("url");
            urlNode.Value = promoteUrl;
            ags.Message msg = BuildNotice("all@broadcast.ydban.cn", promoteUrl, extNode);
            im.SendMessage(msg.ToString());
        }

        private ags.Message BuildNotice(string to,
            string body,
            agsXMPP.Xml.Dom.Element extNode)
        {
            ags.Message msg = new ags.Message();
            msg.SetAttribute("type", "headline");
            msg.Id = Guid.NewGuid().ToString();
            msg.To = to;// 广播,分组.
            msg.Body = body;
            var nodeActive = new agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
            msg.AddChild(nodeActive);
            msg.AddChild(extNode);
            return msg;

        }

        public void SendRessaginMessage(Guid csId)
        {
            DZMembershipProvider bllDZMembership = new DZMembershipProvider();
            BLLReceptionStatus bllReceptionStatus = new BLLReceptionStatus();
            DZMembership cs = bllDZMembership.GetUserById(csId);
            DZMembership imMember = bllDZMembership.GetUserById(new Guid( Dianzhu.Config.Config.GetAppSetting("NoticeSenderId")));
            //通过 IMServer 给客服发送消息
            IIMSession imSession = new IMSessionsDB();
            ReceptionAssigner assigner = new ReceptionAssigner(imSession);
            Dictionary<DZMembership, DZMembership> reassignList = assigner.AssignCSLogoff(cs);
            //将新分配的客服发送给客户端.
            foreach (KeyValuePair<DZMembership, DZMembership> r in reassignList)
            {
                ServiceOrder order = bllReceptionStatus.GetOrder(r.Key, r.Value).Order;
                if (order.OrderStatus != enum_OrderStatus.Draft)
                {
                    ServiceOrder newOrder = new ServiceOrder {
                        Customer=r.Key,CustomerService=r.Value
                    };  
                    order = newOrder;
                }
                ReceptionChat rc = new ReceptionChatReAssign
                {
                    From = imMember,
                    ChatType = enum_ChatType.ReAssign,
                    ReAssignedCustomerService = r.Value,
                    To = r.Key,
                    ServiceOrder = order,
                    SendTime = DateTime.Now
                };
                im.SendMessage(rc);
            }
        }

        public void SendCustomLogoffMessage(Guid csId)
        {
            DZMembershipProvider bllDZMembership = new DZMembershipProvider();
            BLLReceptionStatus bllReceptionStatus = new BLLReceptionStatus();
            ReceptionStatus rs = bllReceptionStatus.GetOneByCustomer(csId);
            DZMembership imMember = bllDZMembership.GetUserById(new Guid(Dianzhu.Config.Config.GetAppSetting("NoticeSenderId")));
            //通过 IMServer 给客服发送消息
            IIMSession imSession = new IMSessionsDB();

            ReceptionChat rc = new ReceptionChatUserStatus
            {
                From = imMember,
                ChatType = enum_ChatType.UserStatus,
                To = rs.CustomerService,
                ServiceOrder = rs.Order,
                SendTime = DateTime.Now,
                User=rs.Customer,
                Status = enum_UserStatus.unavailable
            };
            im.SendMessage(rc);
        }
    }
}