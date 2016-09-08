using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ags = agsXMPP.protocol.client;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.IDAL;
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
        IDALMembership dalMembership;
        BLL.Common.SerialNo.ISerialNoBuilder serialNoBuilder;
        Dianzhu.BLL.ReceptionAssigner assigner;
        IDALReceptionStatus dalReceptionStatus;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="im">通讯接口</param>
        public IMNotify(InstantMessage im,IDALMembership dalMembership , IDALReceptionStatus dalReceptionStatus,ReceptionAssigner assigner, BLL.Common.SerialNo.ISerialNoBuilder serialNoBuilder)
        {
            this.serialNoBuilder = serialNoBuilder;
            this.dalMembership = dalMembership;
            this.im = im;
            this.dalReceptionStatus = dalReceptionStatus;
            this.assigner = assigner;
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
        public void SendOrderChangedNotify(ServiceOrder order)
        {
            var extNode = new agsXMPP.Xml.Dom.Element("ext");
            extNode.Namespace = "ihelper:notice:order";
            var extOrderID = new agsXMPP.Xml.Dom.Element("orderID", order.Id.ToString());
            extNode.AddChild(extOrderID);
            var orderObj = new agsXMPP.Xml.Dom.Element("orderObj");

            orderObj.SetAttribute("title", order.Title);
            orderObj.SetAttribute("status", order.OrderStatus.ToString());
            orderObj.SetAttribute("type", order.Service == null ? string.Empty : order.Service.ServiceType.Name);
            extNode.AddChild(orderObj);

            //发送给客户
            ags.Message msg = BuildNotice(
                 order.Customer.Id + "@" + im.Domain + "/" + enum_XmppResource.YDBan_User,
                 "订单状态已变为:" + order.GetStatusTitleFriendly(order.OrderStatus),
                 extNode
                 );
            im.SendMessage(msg.ToString());

            //发送给客服.
            ags.Message msgForCS = BuildNotice(
                  order.CustomerService.Id + "@" + im.Domain + "/" + enum_XmppResource.YDBan_CustomerService,
                  "订单状态已变为:" + order.OrderStatus,
                  extNode
                  );
            im.SendMessage(msgForCS.ToString());

            //发送给商户
            if (order.Business == null)
            {
                return;
            }
            if (order.Business.Owner == null)
            {
                return;
            }
            ags.Message msgToBusiness = BuildNotice(
                 order.Business.Owner.Id + "@" + im.Domain + "/" + enum_XmppResource.YDBan_Store,
                 "订单状态已变为:" + order.GetStatusTitleFriendly(order.OrderStatus),
                 extNode
                 );
            im.SendMessage(msgToBusiness.ToString());

            //发送给指派的员工
            if (order.Staff == null)
            {
                return;
            }
            ags.Message msgToStaff = BuildNotice(
                 order.Staff.Id + "@" + im.Domain + "/" + enum_XmppResource.YDBan_Staff,
                 "订单状态已变为:" + order.GetStatusTitleFriendly(order.OrderStatus),
                 extNode
                 );
            im.SendMessage(msgToStaff.ToString());
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

        public void SendCSLoginMessageToDD(Guid csId)
        {
            DZMembership cs = dalMembership.FindById(csId);
            string notice = assigner.CSLogin(cs);
            if (!string.IsNullOrEmpty(notice))
            {
                im.SendMessage(notice);
            }
        }

        public void SendRessaginMessage(Guid csId)
        {
          
           DZMembership cs = dalMembership.FindById(csId);
            DZMembership imMember = dalMembership.FindById(new Guid( Dianzhu.Config.Config.GetAppSetting("NoticeSenderId")));

            //发送客服离线消息给点点
            string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
            string noticeDraftNew = string.Format(@"<message xmlns = ""jabber:client"" type = ""headline"" id = ""{2}"" to = ""{1}"" from = ""{0}"">
                                                  <active xmlns = ""http://jabber.org/protocol/chatstates""></active><ext xmlns=""ihelper:notice:cer:offline""></ext></message>",
                                              cs.Id , "c64d9dda-4f6e-437b-89d2-a591012d8c65@" + server, Guid.NewGuid());
            im.SendMessage(noticeDraftNew);

            Dictionary<DZMembership, DZMembership> reassignList = assigner.AssignCSLogoff(cs);
            //将新分配的客服发送给客户端.
            foreach (KeyValuePair<DZMembership, DZMembership> r in reassignList)
            {
 
                ServiceOrder order = dalReceptionStatus. GetOrder(r.Key, r.Value).Order;
 
                if (order == null)
                {
                    log.Debug("没有订单");
                    return;
                }
 
                if (order.OrderStatus != enum_OrderStatus.Draft)
                {
                    string serialNoForOrder = serialNoBuilder.GetSerialNo("FW" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    ServiceOrder newOrder = ServiceOrderFactory.CreateDraft(r.Value,r.Key, serialNoForOrder);
 
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

        public void SendCustomLogoffMessage(Guid cId)
        {
             
            ReceptionStatus rs = dalReceptionStatus.GetOneByCustomer(cId);
            if (rs == null)
            {
                log.Debug("ReceptionStatus为空，用户id为：" + cId);
                return;
            }
            DZMembership imMember = dalMembership.FindById(new Guid(Dianzhu.Config.Config.GetAppSetting("NoticeSenderId")));
            //通过 IMServer 给客服发送消息
           
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

        public void SendCustomLoginMessage(Guid cId)
        {
            
            ReceptionStatus rs = dalReceptionStatus.GetOneByCustomer(cId);
            if (rs == null)
            {
                log.Debug("ReceptionStatus为空，用户id为：" + cId);
                return;
            }
            DZMembership imMember = dalMembership.FindById(new Guid(Dianzhu.Config.Config.GetAppSetting("NoticeSenderId")));
            //通过 IMServer 给客服发送消息
        
            ReceptionChat rc = new ReceptionChatUserStatus
            {
                From = imMember,
                ChatType = enum_ChatType.UserStatus,
                To = rs.CustomerService,
                ServiceOrder = rs.Order,
                SendTime = DateTime.Now,
                User = rs.Customer,
                Status = enum_UserStatus.available
            };
            im.SendMessage(rc);
        }
    }
}